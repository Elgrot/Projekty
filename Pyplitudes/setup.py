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

import pybind11
from setuptools import setup, find_packages, Extension

with open("requirements.txt") as f:
    required = f.read().splitlines()

setup(
    name="Pyplitudes",
    version="0.2",
    author="Emil Sujkowski",
    description="Pyplitudes: Python package to obtain cluster amplitudes",
    package_dir={"Pyplidutes": "Pyplitudes"},
    packages=find_packages(include=["Pyplitudes", "Pyplitudes.*"]),
    include_package_data=True,
    package_data={
        "Pyplitudes": ["data/examples/*.py"],
    },
    install_requires=required,
    python_requires=">=3.7",
    classifiers=[
        "Environment :: Console",
        "License :: OSI Approved :: GNU General Public License (GPL)",
        "Operating System :: POSIX :: Linux",
        "Programming Language :: Python :: 3",
        "Programming Language :: C++",
    ],
    headers=["Pyplitudes/Calculations/calculate.h"],
    ext_modules=[
        Extension(
            "Pyplitudes.Calculations.pycalculate",
            sources=[
                "Pyplitudes/Calculations/py_calculate.cpp",
                "Pyplitudes/Calculations/calculate.cpp",
            ],
            language="c++",
            # Include header files to find pybind11/numpy.h
            include_dirs=[pybind11.get_include()],
        ),
    ],
)
