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

"""Print head and foot banner"""

import atexit
import logging

from . import __version__

head_banner = f"""
########### Welcome! ##############
You're using Pyplitudes version {__version__}
###################################
"""

foot_banner = f"""
############### Goodbye! #################
Thank you for using Pyplitudes version {__version__}
##########################################
"""

# logging example
logging.basicConfig(format="%(message)s", level=logging.INFO)

# print banners
logging.info(head_banner)
atexit.register(logging.info, foot_banner)
