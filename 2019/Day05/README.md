original source: [https://adventofcode.com/2019/day/5](https://adventofcode.com/2019/day/5)
## --- Day 5: Sunny with a Chance of Asteroids ---
You're starting to sweat as the ship makes its way toward Mercury.  The Elves suggest that you get the air conditioner working by upgrading your ship computer to support the Thermal Environment Supervision Terminal.

The Thermal Environment Supervision Terminal (TEST) starts by running a <em>diagnostic program</em> (your puzzle input).  The TEST diagnostic program will run on [your existing Intcode computer](2) after a few modifications:

<em>First</em>, you'll need to add <em>two new instructions</em>:


 - Opcode <code>3</code> takes a single integer as <em>input</em> and saves it to the position given by its only parameter. For example, the instruction <code>3,50</code> would take an input value and store it at address <code>50</code>.
 - Opcode <code>4</code> <em>outputs</em> the value of its only parameter. For example, the instruction <code>4,50</code> would output the value at address <code>50</code>.

Programs that use these instructions will come with documentation that explains what should be connected to the input and output. The program <code>3,0,4,0,99</code> outputs whatever it gets as input, then halts.

<em>Second</em>, you'll need to add support for <em>parameter modes</em>:

Each parameter of an instruction is handled based on its parameter mode.  Right now, your ship computer already understands parameter mode <code>0</code>, <em>position mode</em>, which causes the parameter to be interpreted as a <em>position</em> - if the parameter is <code>50</code>, its value is <em>the value stored at address <code>50</code> in memory</em>. Until now, all parameters have been in position mode.

Now, your ship computer will also need to handle parameters in mode <code>1</code>, <em>immediate mode</em>. In immediate mode, a parameter is interpreted as a <em>value</em> - if the parameter is <code>50</code>, its value is simply <em><code>50</code></em>.

Parameter modes are stored in the same value as the instruction's opcode.  The opcode is a two-digit number based only on the ones and tens digit of the value, that is, the opcode is the rightmost two digits of the first value in an instruction. Parameter modes are single digits, one per parameter, read right-to-left from the opcode: the first parameter's mode is in the hundreds digit, the second parameter's mode is in the thousands digit, the third parameter's mode is in the ten-thousands digit, and so on. Any missing modes are <code>0</code>.

For example, consider the program <code>1002,4,3,4,33</code>.

The first instruction, <code>1002,4,3,4</code>, is a <em>multiply</em> instruction - the rightmost two digits of the first value, <code>02</code>, indicate opcode <code>2</code>, multiplication.  Then, going right to left, the parameter modes are <code>0</code> (hundreds digit), <code>1</code> (thousands digit), and <code>0</code> (ten-thousands digit, not present and therefore zero):

<pre>
<code>ABCDE
 1002

DE - two-digit opcode,      02 == opcode 2
 C - mode of 1st parameter,  0 == position mode
 B - mode of 2nd parameter,  1 == immediate mode
 A - mode of 3rd parameter,  0 == position mode,
                                  omitted due to being a leading zero
</code>
</pre>

This instruction multiplies its first two parameters.  The first parameter, <code>4</code> in position mode, works like it did before - its value is the value stored at address <code>4</code> (<code>33</code>). The second parameter, <code>3</code> in immediate mode, simply has value <code>3</code>. The result of this operation, <code>33 * 3 = 99</code>, is written according to the third parameter, <code>4</code> in position mode, which also works like it did before - <code>99</code> is written to address <code>4</code>.

Parameters that an instruction writes to will <em>never be in immediate mode</em>.

<em>Finally</em>, some notes:


 - It is important to remember that the instruction pointer should increase by <em>the number of values in the instruction</em> after the instruction finishes. Because of the new instructions, this amount is no longer always <code>4</code>.
 - Integers can be negative: <code>1101,100,-1,4,0</code> is a valid program (find <code>100 + -1</code>, store the result in position <code>4</code>).

The TEST diagnostic program will start by requesting from the user the ID of the system to test by running an <em>input</em> instruction - provide it <code>1</code>, the ID for the ship's air conditioner unit.

It will then perform a series of diagnostic tests confirming that various parts of the Intcode computer, like parameter modes, function correctly. For each test, it will run an <em>output</em> instruction indicating how far the result of the test was from the expected value, where <code>0</code> means the test was successful.  Non-zero outputs mean that a function is not working correctly; check the instructions that were run before the output instruction to see which one failed.

Finally, the program will output a <em>diagnostic code</em> and immediately halt. This final output isn't an error; an output followed immediately by a halt means the program finished.  If all outputs were zero except the diagnostic code, the diagnostic program ran successfully.

After providing <code>1</code> to the only input instruction and passing all the tests, <em>what diagnostic code does the program produce?</em>


## --- Part Two ---
The air conditioner comes online! Its cold air feels good for a while, but then the TEST alarms start to go off. Since the air conditioner can't vent its heat anywhere but back into the spacecraft, it's actually making the air inside the ship <em>warmer</em>.

Instead, you'll need to use the TEST to extend the [thermal radiators](https://en.wikipedia.org/wiki/Spacecraft_thermal_control). Fortunately, the diagnostic program (your puzzle input) is already equipped for this.  Unfortunately, your Intcode computer is not.

Your computer is only missing a few opcodes:


 - Opcode <code>5</code> is <em>jump-if-true</em>: if the first parameter is <em>non-zero</em>, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
 - Opcode <code>6</code> is <em>jump-if-false</em>: if the first parameter <em>is zero</em>, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
 - Opcode <code>7</code> is <em>less than</em>: if the first parameter is <em>less than</em> the second parameter, it stores <code>1</code> in the position given by the third parameter.  Otherwise, it stores <code>0</code>.
 - Opcode <code>8</code> is <em>equals</em>: if the first parameter is <em>equal to</em> the second parameter, it stores <code>1</code> in the position given by the third parameter.  Otherwise, it stores <code>0</code>.

Like all instructions, these instructions need to support <em>parameter modes</em> as described above.

Normally, after an instruction is finished, the instruction pointer increases by the number of values in that instruction. <em>However</em>, if the instruction modifies the instruction pointer, that value is used and the instruction pointer is <em>not automatically increased</em>.

For example, here are several programs that take one input, compare it to the value <code>8</code>, and then produce one output:


 - <code>3,9,8,9,10,9,4,9,99,-1,8</code> - Using <em>position mode</em>, consider whether the input is <em>equal to</em> <code>8</code>; output <code>1</code> (if it is) or <code>0</code> (if it is not).
 - <code>3,9,7,9,10,9,4,9,99,-1,8</code> - Using <em>position mode</em>, consider whether the input is <em>less than</em> <code>8</code>; output <code>1</code> (if it is) or <code>0</code> (if it is not).
 - <code>3,3,1108,-1,8,3,4,3,99</code> - Using <em>immediate mode</em>, consider whether the input is <em>equal to</em> <code>8</code>; output <code>1</code> (if it is) or <code>0</code> (if it is not).
 - <code>3,3,1107,-1,8,3,4,3,99</code> - Using <em>immediate mode</em>, consider whether the input is <em>less than </em><code>8</code>; output <code>1</code> (if it is) or <code>0</code> (if it is not).

Here are some jump tests that take an input, then output <code>0</code> if the input was zero or <code>1</code> if the input was non-zero:


 - <code>3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9</code> (using <em>position mode</em>)
 - <code>3,3,1105,-1,9,1101,0,0,12,4,12,99,1</code> (using <em>immediate mode</em>)

Here's a larger example:

<pre>
<code>3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99
</code>
</pre>

The above example program uses an input instruction to ask for a single number.  The program will then output <code>999</code> if the input value is below <code>8</code>, output <code>1000</code> if the input value is equal to <code>8</code>, or output <code>1001</code> if the input value is greater than <code>8</code>.

This time, when the TEST diagnostic program runs its input instruction to get the ID of the system to test, <em>provide it <code>5</code></em>, the ID for the ship's thermal radiator controller. This diagnostic test suite only outputs one number, the <em>diagnostic code</em>.

<em>What is the diagnostic code for system ID <code>5</code>?</em>


