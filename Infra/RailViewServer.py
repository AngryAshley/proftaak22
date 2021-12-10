import sys
import time
import datetime
import socket, ssl
import mysql.connector

ts = time.time()
timestamp = datetime.datetime.fromtimestamp(ts).strftime('%Y-%m-%d %H:%M:%S') # get the current timestamp

context = ssl.SSLContext(ssl.PROTOCOL_TLS)
context.load_cert_chain(certfile="cert.pem", keyfile="cert.pem")

s = socket.socket()
buffer_size = 100

sslsoc = context.wrap_socket(s)
sslsoc.bind(('192.168.161.205', 6023))
sslsoc.listen(5)

doOnce = 0

mydb = mysql.connector.connect(
  host='192.168.161.205',
  user='admin',
  password='TopMaster99',
  database='RailView'
)

railviewdata = mydb.cursor()

railviewdata.execute("SELECT LAST_INSERT_ID() FROM alerts")
result = railviewdata.fetchall()

def updateDatabase(type): # function to update database on type of alert
  updateSqlTrain = "UPDATE alerts SET alert=%s, location_x=%s, location_y=%s, route=%s, times=%s, alert_checked=%s WHERE cam_id=1"
  valuesTrain = (type, 51.4531, 5.5680, "Helmond naar Eindhoven, Intercity", timestamp, 0)
  railviewdata.execute(updateSqlTrain, valuesTrain)
  mydb.commit()

while True:
  newsocket, fromaddr = sslsoc.accept()
  request = sslsoc.read()
  print(request)

  if doOnce == 0:
    print('Got connection from ', fromaddr)
    doOnce = 1

  data = newsocket.recv(buffer_size).decode('ascii')

  if data == "PERSON":
    # select the id where the location = alert location send by the PI
    # counter = counter + 1
    print(data)
    updateDatabase("person")
    
    #c.send(b'RECIEVED MESSAGE') #send data back to PI

  if data == "All clear":
    print(data)
    updateDatabase("other")

  if data == "TRAIN":
    print(data)
    updateDatabase("train")
    
  if data == "!":
    print('Closing...', fromaddr)
    doOnce = 0
    newsocket.close()
    #break #close the script after recieving the closing message
