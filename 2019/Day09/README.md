original source: [https://adventofcode.com/2019/day/9](https://adventofcode.com/2019/day/9)
## --- Day 9: Sensor Boost ---
You've just said goodbye to the rebooted rover and left Mars when you receive a faint distress signal coming from the asteroid belt.  It must be the Ceres monitoring station!

In order to lock on to the signal, you'll need to boost your sensors. The Elves send up the latest <em>BOOST</em> program - Basic Operation Of System Test.

While BOOST (your puzzle input) is capable of boosting your sensors, for tenuous safety reasons, it refuses to do so until the computer it runs on passes some checks to demonstrate it is a <em>complete Intcode computer</em>.

[Your existing Intcode computer](5) is missing one key feature: it needs support for parameters in <em>relative mode</em>.

Parameters in mode <code>2</code>, <em>relative mode</em>, behave very similarly to parameters in <em>position mode</em>: the parameter is interpreted as a position.  Like position mode, parameters in relative mode can be read from or written to.

The important difference is that relative mode parameters don't count from address <code>0</code>.  Instead, they count from a value called the <em>relative base</em>. The <em>relative base</em> starts at <code>0</code>.

The address a relative mode parameter refers to is itself <em>plus</em> the current <em>relative base</em>. When the relative base is <code>0</code>, relative mode parameters and position mode parameters with the same value refer to the same address.

For example, given a relative base of <code>50</code>, a relative mode parameter of <code>-7</code> refers to memory address <code>50 + -7 = <em>43</em></code>.

The relative base is modified with the <em>relative base offset</em> instruction:


 - Opcode <code>9</code> <em>adjusts the relative base</em> by the value of its only parameter. The relative base increases (or decreases, if the value is negative) by the value of the parameter.

For example, if the relative base is <code>2000</code>, then after the instruction <code>109,19</code>, the relative base would be <code>2019</code>. If the next instruction were <code>204,-34</code>, then the value at address <code>1985</code> would be output.

Your Intcode computer will also need a few other capabilities:


 - The computer's available memory should be much larger than the initial program. Memory beyond the initial program starts with the value <code>0</code> and can be read or written like any other memory. (It is invalid to try to access memory at a negative address, though.)
 - The computer should have support for large numbers. Some instructions near the beginning of the BOOST program will verify this capability.

Here are some example programs that use these features:


 - <code>109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99</code> takes no input and produces a [copy of itself](https://en.wikipedia.org/wiki/Quine_(computing)) as output.
 - <code>1102,34915192,34915192,7,4,7,99,0</code> should output a 16-digit number.
 - <code>104,1125899906842624,99</code> should output the large number in the middle.

The BOOST program will ask for a single input; run it in test mode by providing it the value <code>1</code>. It will perform a series of checks on each opcode, output any opcodes (and the associated parameter modes) that seem to be functioning incorrectly, and finally output a BOOST keycode.

Once your Intcode computer is fully functional, the BOOST program should report no malfunctioning opcodes when run in test mode; it should only output a single value, the BOOST keycode. <em>What BOOST keycode does it produce?</em>


