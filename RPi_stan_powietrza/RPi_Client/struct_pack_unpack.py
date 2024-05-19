from struct import pack, unpack

co2 = 34
tvoc = 3.14
temp = 2.72

mess = pack("iff", co2, tvoc, temp)

print(mess)
print(unpack("iff",mess))

