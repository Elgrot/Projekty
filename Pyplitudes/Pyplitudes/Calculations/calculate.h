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

#ifndef COURSE_CALCULATE_H
#define COURSE_CALCULATE_H

#include <pybind11/numpy.h>
#include <pybind11/pybind11.h>
namespace py = pybind11;

void calculate(
    py::array_t<double, py::array::c_style | py::array::forcecast> &inp,
    py::array_t<double, py::array::c_style | py::array::forcecast> &f_i,
    py::array_t<double, py::array::c_style | py::array::forcecast> &f_a,
    py::array_t<double, py::array::c_style | py::array::forcecast> &out);

#endif
