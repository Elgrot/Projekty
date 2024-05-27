# -*- coding: utf-8 -*-
# Pyplitudes: a python package developed for the python 3 course at NCU
# Copyright (C) 2022  Emil Sujkowski
#
# This file is a part of Pyplitudes
#
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program.  If not, see <https://www.gnu.org/licenses/>.


def calc_for(g_ints, f_i, f_a, output):
    """Obtain cluster amplitudes using flavor for loops"""
    for i in range(f_i.shape[0]):
        for j in range(f_i.shape[0]):
            for a in range(f_a.shape[0]):
                for b in range(f_a.shape[0]):
                    output[i, j, a, b] = g_ints[i, j, a, b] / (
                        f_i[i] + f_i[j] - f_a[a] - f_a[b]
                    )
    return output


def calc_cpp(g_ints, f_i, f_a, output):
    """Obtain cluster amplitudes using flavor C++"""
    from Pyplitudes.Calculations.pycalculate import calc_cpp as calc_cpp

    calc_cpp(g_ints, f_i, f_a, output)
    return output


def calculate(g_ints, f_i, f_a, output, select="for"):
    """Obtain cluster amplitudes using flavor select
       The equation we solve is included in documentation,
       basically a division of g_ints over a sum of f_*
    **Arguments:**

        g_ints
            4-dim array

        f_*
            1-dim array

        output
            The resulting matrix"""

    if select == "for":
        return calc_for(g_ints, f_i, f_a, output)

    if select == "c++":
        return calc_cpp(g_ints, f_i, f_a, output)

    raise NotImplementedError
