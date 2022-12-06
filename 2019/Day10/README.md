original source: [https://adventofcode.com/2019/day/10](https://adventofcode.com/2019/day/10)
## --- Day 10: Monitoring Station ---
You fly into the asteroid belt and reach the Ceres monitoring station.  The Elves here have an emergency: they're having trouble tracking all of the asteroids and can't be sure they're safe.

The Elves would like to build a new monitoring station in a nearby area of space; they hand you a map of all of the asteroids in that region (your puzzle input).

The map indicates whether each position is empty (<code>.</code>) or contains an asteroid (<code>#</code>).  The asteroids are much smaller than they appear on the map, and every asteroid is exactly in the center of its marked position.  The asteroids can be described with <code>X,Y</code> coordinates where <code>X</code> is the distance from the left edge and <code>Y</code> is the distance from the top edge (so the top-left corner is <code>0,0</code> and the position immediately to its right is <code>1,0</code>).

Your job is to figure out which asteroid would be the best place to build a <em>new monitoring station</em>. A monitoring station can <em>detect</em> any asteroid to which it has <em>direct line of sight</em> - that is, there cannot be another asteroid <em>exactly</em> between them. This line of sight can be at any angle, not just lines aligned to the grid or diagonally. The <em>best</em> location is the asteroid that can <em>detect</em> the largest number of other asteroids.

For example, consider the following map:

<pre>
<code>.#..#
.....
#####
....#
...<em>#</em>#
</code>
</pre>

The best location for a new monitoring station on this map is the highlighted asteroid at <code>3,4</code> because it can detect <code>8</code> asteroids, more than any other location. (The only asteroid it cannot detect is the one at <code>1,0</code>; its view of this asteroid is blocked by the asteroid at <code>2,2</code>.) All other asteroids are worse locations; they can detect <code>7</code> or fewer other asteroids. Here is the number of other asteroids a monitoring station on each asteroid could detect:

<pre>
<code>.7..7
.....
67775
....7
...87
</code>
</pre>

Here is an asteroid (<code>#</code>) and some examples of the ways its line of sight might be blocked. If there were another asteroid at the location of a capital letter, the locations marked with the corresponding lowercase letter would be blocked and could not be detected:

<pre>
<code>#.........
...A......
...B..a...
.EDCG....a
..F.c.b...
.....c....
..efd.c.gb
.......c..
....f...c.
...e..d..c
</code>
</pre>

Here are some larger examples:


 - Best is <code>5,8</code> with <code>33</code> other asteroids detected:

<pre>
<code>......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...<em>#</em>..#.
.#....####
</code>
</pre>

 - Best is <code>1,2</code> with <code>35</code> other asteroids detected:

<pre>
<code>#.#...#.#.
.###....#.
.<em>#</em>....#...
##.#.#.#.#
....#.#.#.
.##..###.#
..#...##..
..##....##
......#...
.####.###.
</code>
</pre>

 - Best is <code>6,3</code> with <code>41</code> other asteroids detected:

<pre>
<code>.#..#..###
####.###.#
....###.#.
..###.<em>#</em>#.#
##.##.#.#.
....###..#
..#.#..#.#
#..#.#.###
.##...##.#
.....#.#..
</code>
</pre>

 - Best is <code>11,13</code> with <code>210</code> other asteroids detected:

<pre>
<code>.#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.####<em>#</em>#####...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##
</code>
</pre>


Find the best location for a new monitoring station.  <em>How many other asteroids can be detected from that location?</em>


