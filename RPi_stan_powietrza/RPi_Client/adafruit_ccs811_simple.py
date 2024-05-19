#!/usr/bin/python3

from struct import pack
import socket
from time import sleep
from adafruit_extended_bus import ExtendedI2C as I2C
import adafruit_ccs811


def ReadAndSend(times, sleepTime, address):
    """Reads data from sensor and send it to server
        Arguments:
         times - how many readings will be taken
         nap - time between reads
         server_address - IP address and port of the server
        This function also skips any co2 or tvoc reading that is over 30 000 or below 0"""
        
    try:
        while times>0:
            co2 = ccs.eco2
            tvoc = ccs.tvoc
            temp = ccs.temperature
            print(f"CO2: {co2}({type(co2)}), TVOC: {tvoc}({type(tvoc)}), Temperature: {temp}({type(temp)})") 
            if co2 > 30000 or co2 < 0 or tvoc > 30000 or tvoc < 0:
                pass
            Send(co2, tvoc, temp, address)
            print(sleepTime,type(sleepTime))
            sleep(sleepTime)
            times -= 1
            print(f"{times} reading(s) left")
            
    except Exception:
        print(f"Sensor error! {repr(Exception)}")

def Send(co2, tvoc, temp, address):
    """Pack data and send it to the server"""
    
    message = pack("iif", co2, tvoc, temp)
    try:
        sock.sendto(message, address)
        print(f"Data sent! Length: {len(message)}")
    except Exception:
        print(f"Data not sent! {Exception}")
        
        
if __name__ == "__main__":
    
    #i2c = board.I2C() # uses board.SCL and board.SDA
    i2c = I2C(1) # This is slowing down I2C clock to match the sensor's clock
    ccs = adafruit_ccs811.CCS811(i2c) # instantiate the sensor
    
    # Create UDP socket
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    
    # host - adres IP serwera
    host, port = '192.168.137.1', 65000

    server_address = (host,port)
    
    # Send hello message
    sock.sendto(b'H', server_address)
    data = sock.recv(2)
    print(data,len(data))
    readings, sleepTime = data
    print(readings,type(readings),sleepTime,type(sleepTime))
    
    # Wait for the sensor to be ready  
    while not ccs.data_ready:
        pass
    ReadAndSend(readings, sleepTime, server_address)
    #ReadAndSend(5,1,server_address)
    
    sock.close()
