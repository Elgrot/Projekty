# <div align="center"> ![Pyplitudes logo](http://fizyka.umk.pl/~313059/Pyplitudes1.png) </div>

Pyplitudes is a python package developed for the python 3 course at Nicolaus Copernicus University (NCU).

The package solves equation:
![equation](http://fizyka.umk.pl/~313059/equation.png)

## Usage/Examples

```python
import Pyplitudes
from Pyplitudes import open_h5, Solver

# Read arrays from data.h5 <- provide path to your data file
g_ints, f_i, f_a = open_h5("data.h5")

# Initialiaze Solver class
obj = Solver()

# Calling our class generates output and solves the equation
# select is one of the following flavor: "for" , "c++"
obj(g_ints, f_i, f_a, select=select)
```

## Installation and documentation

We use groundworks sphinx theme.

To download this theme run the following command:

``` pip install groundworks-sphinx-theme ```

For installation see:
[Installation](http://fizyka.umk.pl/~313059/)

## License

[GNU GPLv3](https://choosealicense.com/licenses/gpl-3.0/)
