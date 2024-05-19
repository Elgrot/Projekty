#!/usr/bin/python3

from random import random
from struct import pack, unpack
import socket
from time import sleep

# Create UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

host, port = '192.168.100.3', 65000
server_address = (host,port)

# Generate random values
x, y, z = random(), random(), random()

# Send messages

sock.sendto(b'Start', server_address)
for i in range(10):
    # Pack three 32-bit floats into message
    message = pack('3f', x, y, z)
    print(unpack("3f", message))
    
    sock.sendto(message, server_address)
    
    sleep(1)
    x += 2
    y += 2
    z += 2

sock.sendto(b'Stop', server_address)