import paho.mqtt.client as mqtt
import time
import datetime
import mysql.connector

hostname = "192.168.161.205"
username = "admin"
passwd = "TopMaster99"
ts = time.time()
timestamp = datetime.datetime.fromtimestamp(ts).strftime('%Y-%m-%d %H:%M:%S') # get the current timestamp

mydb = mysql.connector.connect(
  host=hostname,
  user=username,
  password=passwd,
  database='RailView'
)

railviewdata = mydb.cursor()

def updateDatabase(type): # function to update database on type of alert
    updateSqlTrain = "UPDATE alerts SET alert=%s, location_x=%s, location_y=%s, route=%s, times=%s, alert_checked=%s WHERE cam_id=1"
    valuesTrain = (type, 51.4531, 5.5680, "Helmond naar Eindhoven, Intercity", timestamp, 0)
    railviewdata.execute(updateSqlTrain, valuesTrain)
    mydb.commit()

def on_connect(client, userdata, flags, rc):
    if rc == 0:
        print("Connected to MQTT Broker!")
    else:
        print("Failed to connect, return code %d\n", rc)

def on_message(client, userdata, message):
    recievedMessage = str(message.payload.decode("utf-8"))

    print("received message: ", timestamp, " ", recievedMessage)

    if (recievedMessage == "TRAIN"):
        updateDatabase("TRAIN")

    elif (recievedMessage == "PERSON"):
        updateDatabase("PERSON")

    else:
        updateDatabase("other")
        print("All clear or something else...")

mqttBroker = hostname

client = mqtt.Client()
client.username_pw_set(username, passwd)
client.connect(mqttBroker, 8883, 60)

client.loop_start()
client.on_connect=on_connect

client.subscribe("RailView")
client.on_message=on_message 

time.sleep(604800) #timeout after 7 days
client.loop_stop()