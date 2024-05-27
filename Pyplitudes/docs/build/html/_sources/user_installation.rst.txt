Package installation
=======================
.. toctree::
    :hidden:

    user_amplitudes

.. Warning::

    Link for download may not work at the moment

Linux Ubuntu 20.04 LTS
######################

Download Pyplitudes
+++++++++++++++++++

You can download the latest version here:

    * `Pyplitudes 0.3 <https://gitlab.com/Emil-Sujkowski/Pyplitudes>`_

    * In case download link is not working, you can clone the repository:
        - **git@gitlab.com:Emil-Sujkowski/Pyplitudes.git**

Dependencies for Pyplitudes package
+++++++++++++++++++++++++++++++++++

To compile, run and test Pyplitudes, you need the following packages:

- ``gcc`` (>=9.4.0)

    .. code-block::

        $ sudo  apt-get install gcc g++
        $ gcc --version
        $ g++ --version
- ``python`` (>=3.8.10)

    .. code-block::

        $ sudo apt-get install python3
- ``git`` (>=2.25.1)

    .. code-block::

        $ sudo apt-get install git
        $ git --version
- ``pip3`` (>=20.0.2)

    .. code-block::

        $ sudo apt-get install python3-pip
        $ pip3 --version
- ``pybind11`` (>=2.5.0)

    .. code-block::

        $ sudo pip3 install pybind11==2.5.0
        $ python3
        $ import pybind11
        $ pybind11.__version__
        $ exit()
- ``numpy`` (>=1.12.3)

    .. code-block::

        $ sudo pip3 install numpy
        $ python3
        $ import numpy
        $ numpy.__version__
        $ exit()
- ``pytest`` (>=6.2.5)

    .. code-block::

        $ sudo pip3 install pytest
        $ python3
        $ import pytest
        $ pytest.__version__
        $ exit()
- ``h5py`` (>=3.0.0)

    .. code-block::

        $ sudo pip3 install --no-binary=h5py h5py
        $ python3
        $ import h5py
        $ h5py.__version__
        $ exit()
- ``setuptools`` (>=61.0.0)

    .. code-block::

        $ sudo pip3 install setuptools
        $ python3
        $ import setuptools
        $ setuptools.__version__
        $ exit()
