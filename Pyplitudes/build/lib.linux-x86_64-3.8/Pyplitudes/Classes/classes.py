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

import logging
import numpy as np
from abc import abstractmethod, ABC
from Pyplitudes.Calculations.calculate import calculate


class Base(ABC):
    def __init__(self):
        self._output = None

    @property
    def output(self):
        """Cluster amplitudes"""
        return self._output

    @output.setter
    def output(self, new):
        if new is None:
            raise ValueError("Wrong value.")
        self._output = new

    def __call__(self, g_ints, f_i, f_a, **kwargs):
        """Provide information about the problem and return the result"""
        self.info(g_ints, **kwargs)

        self.solve(g_ints, f_i, f_a, **kwargs)

        self.solution(**kwargs)

    def info(self, g_ints, **kwargs):
        """Print dimension of the problem and flavor"""
        logging.info("####################################")
        logging.info(f"Flavor:      {kwargs.get('select', 'for')}")
        logging.info("Dimension of the problem:")
        logging.info(f"     i:      {g_ints.shape[0]}")
        logging.info(f"     j:      {g_ints.shape[1]}")
        logging.info(f"     a:      {g_ints.shape[2]}")
        logging.info(f"     b:      {g_ints.shape[3]}")

    def solution(self, **kwargs):
        """Print output"""
        logging.info("")
        logging.info("####################################")
        logging.info(f"Matrix norm:    {np.linalg.norm(self.output)}")
        logging.info("")
        logging.info("Exiting...")

    @abstractmethod
    def solve(self, g_ints, f_i, f_a, **kwargs):
        """Solve problem and return cluster amplitudes"""


class Solver(Base):
    def solve(self, g_ints, f_i, f_a, **kwargs):

        out = np.zeros_like(g_ints)
        select = kwargs.get("select", "for")
        calculate(g_ints, f_i, f_a, out, select=select)
        self.output = out
