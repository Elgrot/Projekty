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

import pytest


def pytest_addoption(parser):
    """pytest hook for adding custom run options"""

    parser.addoption(
        "--slow",
        action="store_true",
        default=False,
        help="run ONLY slow tests",
    )
    parser.addoption(
        "--all", action="store_true", default=False, help="run ALL tests"
    )


def pytest_collection_modifyitems(config, items):
    """modify collection to honor custom options"""

    if config.getoption("--all"):
        return

    # run only slow tests
    if config.getoption("--slow"):
        skip_fast = pytest.mark.skip
        for item in items:
            if "slow" not in item.keywords:
                item.add_marker(skip_fast)
        return

    # run only fast tests
    skip_slow = pytest.mark.skip
    for item in items:
        if "slow" in item.keywords:
            item.add_marker(skip_slow)
