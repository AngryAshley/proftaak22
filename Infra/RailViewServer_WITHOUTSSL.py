import sys
import time
import datetime
import socket, ssl
import mysql.connector

ts = time.time()
timestamp = datetime.datetime.fromtimestamp(ts).strftime('%Y-%m-%d %H:%M:%S') # get the current timestamp

buffer_size = 100
doOnce = 0

peterjan = 0

mydb = mysql.connector.connect(
  host='192.168.161.205',
  user='admin',
  password='TopMaster99',
  database='RailViewv2'
)

railviewdata = mydb.cursor()

def updateDatabase(type): # function to update database on type of alert
  updateSqlTrain = "UPDATE Accident SET Accident_Type=%s, Accident_Date=%s WHERE Accident_ID=1"
  valuesTrain = (type, timestamp)
  railviewdata.execute(updateSqlTrain, valuesTrain)
  mydb.commit()

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.bind(('192.168.161.205', 6023))
sock.listen(5)
conn, addr = sock.accept()

while (True):
  if doOnce == 0:
    print('Got connection from ', addr)
    doOnce = 1

  data = conn.recv(buffer_size).decode('ascii')
  if not data: print("REMOTE CONNECTION ERROR")
  print(data)

  if data == "PERSON":
    print(data)
    # select the id where the location = alert location send by the PI
    # counter = counter + 1
    updateDatabase("person")
    #c.send(b'RECIEVED MESSAGE') #send data back to PI

  if data == "All clear":
    print(data)
    updateDatabase("other")

  if data == "TRAIN":
    print(data)
    updateDatabase("train")
    
  if data == "!":
    print('Closing...', addr)
    doOnce = 0
    sock.close()
    #break #close the script after recieving the closing message