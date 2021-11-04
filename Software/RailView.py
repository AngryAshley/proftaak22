import numpy as np
import cv2;
#print(cv2.__version__);
print("Starting RailView System...")

cap = cv2.VideoCapture(0)
NSVIRMupperRange = np.array([35,255,255],dtype=np.uint8)
NSVIRMlowerRange = np.array([25,155,20],dtype=np.uint8)

GEVAARupperRange = np.array([179,255,255],dtype=np.uint8)
GEVAARlowerRange = np.array([161,155,20],dtype=np.uint8)
   

lastFrameDetected=False
cameraRunning=True


print("OK")
while(True):



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
         print("Train detected") 

      if cv2.countNonZero(cv2.cvtColor(GEVAARoutput,cv2.COLOR_BGR2GRAY)) > 20:
         thisFrameDetected=True
         print("PERSON DETECTED") 

      if(lastFrameDetected == True and thisFrameDetected == False):
         print("All clear")

      lastFrameDetected=thisFrameDetected

   k = cv2.waitKey(1)
   if(k==27):
      break

cap.release()
cv2.destroyAllWindows()
