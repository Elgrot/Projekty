// -*- coding: utf-8 -*-
// Pyplitudes: a python package developed for the python 3 course at NCU
// Copyright (C) 2022  Emil Sujkowski
//
// This file is part of Pyplitudes
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

#include "calculate.h"

void calculate(
    py::array_t<double, py::array::c_style | py::array::forcecast> &inp,
    py::array_t<double, py::array::c_style | py::array::forcecast> &f_i,
    py::array_t<double, py::array::c_style | py::array::forcecast> &f_a,
    py::array_t<double, py::array::c_style | py::array::forcecast> &out) {
  // Create buffer pointer to input and output
  py::buffer_info bufinp0 = f_i.request();
  py::buffer_info bufinp1 = f_a.request();
  py::buffer_info bufinp2 = inp.request();
  double *ptrf_i = static_cast<double *>(bufinp0.ptr);
  double *ptrf_a = static_cast<double *>(bufinp1.ptr);
  double *ptrinp = static_cast<double *>(bufinp2.ptr);
  py::buffer_info bufout = out.request();
  double *ptrout = static_cast<double *>(bufout.ptr);

  int dim_i = bufout.shape[0];
  int dim_a = bufout.shape[2];

  for (int i = 0; i < dim_i; i++)
    for (int j = 0; j < dim_i; j++)
      for (int a = 0; a < dim_a; a++)
        for (int b = 0; b < dim_a; b++) {
          // g_ints[i,j,a,b] / (f_i[i]+f_i[j]-f_a[a]-f_a[b])
          int ind = (((i * dim_i + j) * dim_i + a) * dim_a + b);
          ptrout[ind] +=
              ptrinp[ind] / (ptrf_i[i] + ptrf_i[j] - ptrf_a[a] - ptrf_a[b]);
        }
}
