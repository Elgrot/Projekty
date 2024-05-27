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

import numpy as np
import pytest

from Pyplitudes.Calculations.calculate import calculate


def generate_guess(dim_1, dim_2):
    """Generate arrays for testing"""
    np.random.seed(101)
    f_i = np.random.rand(dim_1)
    f_a = np.random.rand(dim_2)
    g_ints = np.random.rand(dim_1, dim_1, dim_2, dim_2)

    return g_ints, f_i, f_a


@pytest.fixture(params=[(1, 1), (2, 3), (5, 5), (6, 8), (5, 15)])
def arrays(request):
    """fixture for generating the input data"""
    dim_1 = request.param[0]
    dim_2 = request.param[1]
    return generate_guess(dim_1, dim_2)


data_flavor = ["for", "c++"]


@pytest.mark.slow
@pytest.mark.parametrize("select", data_flavor)
def test_calculate_fixture(arrays, select):
    """Test for all flavors for calculate"""
    g_ints, f_i, f_a = arrays

    ref = g_ints.shape

    out = np.zeros_like(g_ints)
    out1 = calculate(g_ints, f_i, f_a, out, select)

    assert (out == out1).all()
    assert out.shape == ref, "wrong shape"


data_select = ["fro", "foor", "or", "cpp", "cp", "c+", "c"]


@pytest.mark.parametrize("select", data_select)
def test_calculate_select(select):
    """Test if provided select argument is correct"""
    with pytest.raises(NotImplementedError):
        calculate(None, None, None, None, select)
