import socket
from struct import unpack

# Create UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Bind the socket to the port
host, port = '0.0.0.0', 65000
server_address = (host,port)

print(f'Starting UDP server on {host} port {port}')
sock.bind(server_address)

while True:
    # Wait for the message
    message, address = sock.recvfrom(4096)

    print(f'Received {len(message)} bytes:')
    x, y, z = unpack("3f", message)
    print(x,y,z)