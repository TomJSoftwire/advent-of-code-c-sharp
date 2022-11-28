original source: [https://adventofcode.com/2019/day/3](https://adventofcode.com/2019/day/3)
## --- Day 3: Crossed Wires ---
The gravity assist was successful, and you're well on your way to the Venus refuelling station.  During the rush back on Earth, the fuel management system wasn't completely installed, so that's next on the priority list.

Opening the front panel reveals a jumble of wires. Specifically, <em>two wires</em> are connected to a central port and extend outward on a grid.  You trace the path each wire takes as it leaves the central port, one wire per line of text (your puzzle input).

The wires twist and turn, but the two wires occasionally cross paths. To fix the circuit, you need to <em>find the intersection point closest to the central port</em>. Because the wires are on a grid, use the [Manhattan distance](https://en.wikipedia.org/wiki/Taxicab_geometry) for this measurement. While the wires do technically cross right at the central port where they both start, this point does not count, nor does a wire count as crossing with itself.

For example, if the first wire's path is <code>R8,U5,L5,D3</code>, then starting from the central port (<code>o</code>), it goes right <code>8</code>, up <code>5</code>, left <code>5</code>, and finally down <code>3</code>:

<pre>
<code>...........
...........
...........
....+----+.
....|....|.
....|....|.
....|....|.
.........|.
.o-------+.
...........
</code>
</pre>

Then, if the second wire's path is <code>U7,R6,D4,L4</code>, it goes up <code>7</code>, right <code>6</code>, down <code>4</code>, and left <code>4</code>:

<pre>
<code>...........
.+-----+...
.|.....|...
.|..+--X-+.
.|..|..|.|.
.|.-<em>X</em>--+.|.
.|..|....|.
.|.......|.
.o-------+.
...........
</code>
</pre>

These wires cross at two locations (marked <code>X</code>), but the lower-left one is closer to the central port: its distance is <code>3 + 3 = 6</code>.

Here are a few more examples:


 - <code>R75,D30,R83,U83,L12,D49,R71,U7,L72
U62,R66,U55,R34,D71,R55,D58,R83</code> = distance <code>159</code>
 - <code>R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
U98,R91,D20,R16,D67,R40,U7,R15,U6,R7</code> = distance <code>135</code>

<em>What is the Manhattan distance</em> from the central port to the closest intersection?


## --- Part Two ---
It turns out that this circuit is very timing-sensitive; you actually need to <em>minimize the signal delay</em>.

To do this, calculate the <em>number of steps</em> each wire takes to reach each intersection; choose the intersection where the <em>sum of both wires' steps</em> is lowest. If a wire visits a position on the grid multiple times, use the steps value from the <em>first</em> time it visits that position when calculating the total value of a specific intersection.

The number of steps a wire takes is the total number of grid squares the wire has entered to get to that location, including the intersection being considered. Again consider the example from above:

<pre>
<code>...........
.+-----+...
.|.....|...
.|..+--X-+.
.|..|..|.|.
.|.-X--+.|.
.|..|....|.
.|.......|.
.o-------+.
...........
</code>
</pre>

In the above example, the intersection closest to the central port is reached after <code>8+5+5+2 = <em>20</em></code> steps by the first wire and <code>7+6+4+3 = <em>20</em></code> steps by the second wire for a total of <code>20+20 = <em>40</em></code> steps.

However, the top-right intersection is better: the first wire takes only <code>8+5+2 = <em>15</em></code> and the second wire takes only <code>7+6+2 = <em>15</em></code>, a total of <code>15+15 = <em>30</em></code> steps.

Here are the best steps for the extra examples from above:


 - <code>R75,D30,R83,U83,L12,D49,R71,U7,L72
U62,R66,U55,R34,D71,R55,D58,R83</code> = <code>610</code> steps
 - <code>R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
U98,R91,D20,R16,D67,R40,U7,R15,U6,R7</code> = <code>410</code> steps

<em>What is the fewest combined steps the wires must take to reach an intersection?</em>


