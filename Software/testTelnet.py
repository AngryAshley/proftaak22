import sys
import time
import datetime
import socket
import mysql.connector

ts = time.time()
timestamp = datetime.datetime.fromtimestamp(ts).strftime('%Y-%m-%d %H:%M:%S') # get the current timestamp

s = socket.socket()
host = '192.168.161.205'
buffer_size = 100
port = 6023
s.bind((host, port))

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

#counter = len(result)

s.listen(5)
c, addr = s.accept()
while True:

  if doOnce == 0:
    print('Got connection from ', addr)
    doOnce = 1

  data = c.recv(buffer_size).decode('ascii')

  if data == "PERSON DETECTED":
    # select the id where the location = alert location send by the PI
    # counter = counter + 1
    print(data)
    
    updateSql = "UPDATE alerts SET alert=%s, location_x=%s, location_y=%s, route=%s, times=%s, alert_checked=%s WHERE cam_id=1"
    values = ("person", 51.4531, 5.5680, "Helmond naar Eindhoven, Intercity", timestamp, 0)
    railviewdata.execute(updateSql, values)
    mydb.commit()
    
    #print(railviewdata.rowcount, "record(s) affected")
    
    c.send(b'RECIEVED MESSAGE') #send data back to PI

  if data == "All clear":
    print(data)
    updateSql = "UPDATE alerts SET alert=%s, location_x=%s, location_y=%s, route=%s, times=%s, alert_checked=%s WHERE cam_id=1"
    values = ("person", 51.4531, 5.5680, "Helmond naar Eindhoven, Intercity", timestamp, 0)
    railviewdata.execute(updateSql, values)
    mydb.commit()

  if data == "Train detected":
    print(data)
    updateSql = "UPDATE alerts SET alert=%s, location_x=%s, location_y=%s, route=%s, times=%s, alert_checked=%s WHERE cam_id=1"
    values = ("person", 51.4531, 5.5680, "Helmond naar Eindhoven, Intercity", timestamp, 0)
    railviewdata.execute(updateSql, values)
    mydb.commit()

  if data == "!":
    print('Closing...', addr)
    doOnce = 0
    c.close()
    #break #close the script after recieving the closing message
