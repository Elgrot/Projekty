What and how are we solving?
############################

Calculations module
+++++++++++++++++++
We are solving:

    .. math::

        t_{ij}^{ab} = \frac{g_{ijab}}{f_{i} + f_{j} - f_{a} - f_{b}}


How are we solving
++++++++++++++++++

In order to run a calulation, create an _instance_ of the Solve class and perform a function call.

.. code-block:: python

    cluster = Solve()
    cluster(g_ints, f_i, f_a, select="c++")

Arguments:

+------------+------------------+
| Argument   | Type             |
+============+==================+
| g_ints     | 4-dim np.array   |
+------------+------------------+
| f_i        | 1-dim np.array   |
+------------+------------------+
| f_a        | 1-dim np.array   |
+------------+------------------+

keyword arguments:

    - select: (str) choose one:
        * ``for`` (default)
        * ``c++``

The inputs can be read from file using the :py:func:`~Pyplitudes.IO.iodata.open_h5`
function::

    # Read input
    input = open_h5("data.h5")

Example
+++++++

.. code-block:: python

   import Pyplitudes
   from Pyplitudes import open_h5, Solver

   g_ints, f_i, f_a = open_h5("data.h5")

   obj = Solver()
   for select in ["for", "c++"]:
       result = obj(g_ints, f_i, f_a, select=select)
