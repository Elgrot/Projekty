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

#include <pybind11/pybind11.h>

#include "calculate.h"

namespace py = pybind11;

PYBIND11_MODULE(pycalculate, m) {
  // Module docstring
  m.doc() = "pybind11 module PyCalculate";

  // Functions
  m.def("calc_cpp", &calculate,
        "Solve a set of equations (provided in docs) to obtain some cluster "
        "amplitudes",
        py::return_value_policy::take_ownership);
}
