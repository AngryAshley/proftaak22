import numpy as np
import asyncio, telnetlib3
from datetime import datetime
import cv2;
#print(cv2.__version__);

status = "clear" # clear or alert
ver = 0x01
rev = 0x00
serialIn = ""

print("Starting RailView System...")



cap = cv2.VideoCapture(0)
NSVIRMupperRange = np.array([35,255,255],dtype=np.uint8)
NSVIRMlowerRange = np.array([25,155,20],dtype=np.uint8)

GEVAARupperRange = np.array([179,255,255],dtype=np.uint8)
GEVAARlowerRange = np.array([161,155,20],dtype=np.uint8)
   

lastFrameDetected=False



print(" - OpenCV initialized")
print(" - Initializing Telnet client...",end='')

@asyncio.coroutine
def shell(reader, writer):
    while True:
        # read stream until '?' mark is found
        outp = yield from reader.read(1024)
        if not outp:
            # End of File
            break
        else:
            # reply all questions with 'y'.
            print('y')

        # display all server output
        print(outp, flush=True)

    # EOF
    print()



async def receive(reader):
   while True:
      serialIn = reader.read(1024)
      await asyncio.sleep(1)



print(" OK ")

reader = None
writer = None
loop=None
coro=None
receiver = None

while True: 

   print(" - Connecting to RailView server...",end='')

   try:
      loop = asyncio.get_event_loop()
      coro = telnetlib3.open_connection('192.168.161.205', 6023, shell=shell)
      reader, writer = loop.run_until_complete(coro)
      #loop.run_until_complete(writer.protocol.waiter_closed)

   except OSError:
      print("FAIL")

   if reader is not None:
      print(" OK ")
      
      break





print("System booted successfully!")
cameraRunning=True

async def main(): #program loop
   receiver = asyncio.create_task(receive(reader))
   
   serialIn=""
   lastFrameDetected=False
   
   while True:
      await asyncio.sleep(1)
      if(serialIn!=""):   
         print(serialIn)
         serialIn=""

      if(cameraRunning==True):
         thisFrameDetected=False
         ret, frame = cap.read()
         #frame = cv2.flip(frame,-1)

         NSVIRMmask = cv2.inRange(cv2.cvtColor(frame,cv2.COLOR_BGR2HSV), NSVIRMlowerRange, NSVIRMupperRange)
         NSVIRMoutput = cv2.bitwise_and(frame,frame,mask=NSVIRMmask)   
         GEVAARmask = cv2.inRange(cv2.cvtColor(frame,cv2.COLOR_BGR2HSV), GEVAARlowerRange, GEVAARupperRange)
         GEVAARoutput = cv2.bitwise_and(frame,frame,mask=GEVAARmask)  
   
         output= cv2.bitwise_or(NSVIRMoutput, GEVAARoutput)
         cv2.imshow('RailView | Cam 1',np.hstack([frame,output]))
   
         if cv2.countNonZero(cv2.cvtColor(NSVIRMoutput,cv2.COLOR_BGR2GRAY)) > 20:
            thisFrameDetected=True
            print(datetime.today(),">Train detected") 

         if cv2.countNonZero(cv2.cvtColor(GEVAARoutput,cv2.COLOR_BGR2GRAY)) > 20:
            thisFrameDetected=True
            print(datetime.today(),">PERSON DETECTED")
            writer.write("PERSON DETECTED")
            status = "alert"

         if(lastFrameDetected == True and thisFrameDetected == False):
            print(datetime.today(),">All clear")
            status = "clear"

         lastFrameDetected=thisFrameDetected

      k = cv2.waitKey(1)
      if(k==27):
         break


loop.run_until_complete(main()) # start the main loop async.

if writer is not None:
   writer.write("!")
cap.release()
cv2.destroyAllWindows()
print("Goodbye!")