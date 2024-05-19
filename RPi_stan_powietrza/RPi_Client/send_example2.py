#!/usr/bin/python3

from struct import pack, unpack
import socket
from time import sleep

# Create UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

host, port = '192.168.137.1', 65000
#host, port = '192.168.100.3', 65000
server_address = (host,port)

co2 = 34
tvoc = 3.14
temp = 2.72

message = pack("iff", co2, tvoc, temp)
# Send messages

print(unpack("iff", message), len(message))
sock.sendto(message, server_address)