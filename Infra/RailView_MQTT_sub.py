import paho.mqtt.client as mqtt
import time
import datetime
import mysql.connector

ts = time.time()
timestamp = datetime.datetime.fromtimestamp(ts).strftime('%Y-%m-%d %H:%M:%S') # get the current timestamp

mydb = mysql.connector.connect(
  host='192.168.161.205',
  user='admin',
  password='TopMaster99',
  database='RailView'
)

railviewdata = mydb.cursor()

def on_connect(client, userdata, flags, rc):
    if rc == 0:
        print("Connected to MQTT Broker!")
    else:
        print("Failed to connect, return code %d\n", rc)

def on_message(client, userdata, message):
    recievedMessage = str(message.payload.decode("utf-8"))

    print("received message: ", timestamp, " ", recievedMessage)

    if (recievedMessage == "TRAIN"):
        updateSqlTrain = "UPDATE alerts SET alert=%s, location_x=%s, location_y=%s, route=%s, times=%s, alert_checked=%s WHERE cam_id=1"
        valuesTrain = ("train", 51.4531, 5.5680, "Helmond naar Eindhoven, Intercity", timestamp, 0)
        railviewdata.execute(updateSqlTrain, valuesTrain)
        mydb.commit()

    elif (recievedMessage == "PERSON"):
        updateSqlPerson = "UPDATE alerts SET alert=%s, location_x=%s, location_y=%s, route=%s, times=%s, alert_checked=%s WHERE cam_id=1"
        valuesPerson = ("person", 51.4531, 5.5680, "Helmond naar Eindhoven, Intercity", timestamp, 0)
        railviewdata.execute(updateSqlPerson, valuesPerson)
        mydb.commit()

    else:
        updateSqlOther = "UPDATE alerts SET alert=%s, location_x=%s, location_y=%s, route=%s, times=%s, alert_checked=%s WHERE cam_id=1"
        valuesOther = ("other", 51.4531, 5.5680, "Helmond naar Eindhoven, Intercity", timestamp, 0)
        railviewdata.execute(updateSqlOther, valuesOther)
        mydb.commit()
        print("All clear or something else...")

mqttBroker ="192.168.161.205"

client = mqtt.Client()
client.username_pw_set("admin", "TopMaster99")
client.connect(mqttBroker, 1883, 60)

client.loop_start()
client.on_connect=on_connect

client.subscribe("RailView")
client.on_message=on_message 

time.sleep(604800) #timeout after 7 days
client.loop_stop()