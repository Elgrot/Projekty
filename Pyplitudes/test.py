from Pyplitudes import open_h5, Solver

g_ints, f_i, f_a = open_h5("Pyplitudes/data/data.h5")
obj = Solver()
for select in ["c++", "for"]:
    result = obj(g_ints, f_i, f_a, select=select)