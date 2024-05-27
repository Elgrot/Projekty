# -*- coding: utf-8 -*-
#  <one line to give the program's name and a brief idea of what it does.>
#  Copyright (C) 2023  Emil Sujkowski

#  This program is free software: you can redistribute it and/or modify
#  it under the terms of the GNU General Public License as published by
#  the Free Software Foundation, either version 3 of the License, or
#  (at your option) any later version.

#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU General Public License for more details.

#  You should have received a copy of the GNU General Public License
#  along with this program.  If not, see <https://www.gnu.org/licenses/>.

import Pyplitudes
from Pyplitudes import open_h5, Solver

g_ints, f_i, f_a = open_h5("data.h5")
obj = Solver()
for select in ["for", "c++"]:
    result = obj(g_ints, f_i, f_a, select=select)
