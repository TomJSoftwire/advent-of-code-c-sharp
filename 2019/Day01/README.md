original source: [https://adventofcode.com/2019/day/1](https://adventofcode.com/2019/day/1)
## --- Day 1: The Tyranny of the Rocket Equation ---
Santa has become stranded at the edge of the Solar System while delivering presents to other planets! To accurately calculate his position in space, safely align his warp drive, and return to Earth in time to save Christmas, he needs you to bring him measurements from <em>fifty stars</em>.

Collect stars by solving puzzles.  Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first.  Each puzzle grants <em>one star</em>. Good luck!

The Elves quickly load you into a spacecraft and prepare to launch.

At the first Go / No Go poll, every Elf is Go until the Fuel Counter-Upper.  They haven't determined the amount of fuel required yet.

Fuel required to launch a given <em>module</em> is based on its <em>mass</em>.  Specifically, to find the fuel required for a module, take its mass, divide by three, round down, and subtract 2.

For example:


 - For a mass of <code>12</code>, divide by 3 and round down to get <code>4</code>, then subtract 2 to get <code>2</code>.
 - For a mass of <code>14</code>, dividing by 3 and rounding down still yields <code>4</code>, so the fuel required is also <code>2</code>.
 - For a mass of <code>1969</code>, the fuel required is <code>654</code>.
 - For a mass of <code>100756</code>, the fuel required is <code>33583</code>.

The Fuel Counter-Upper needs to know the total fuel requirement.  To find it, individually calculate the fuel needed for the mass of each module (your puzzle input), then add together all the fuel values.

<em>What is the sum of the fuel requirements</em> for all of the modules on your spacecraft?


