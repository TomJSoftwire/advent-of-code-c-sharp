original source: [https://adventofcode.com/2019/day/2](https://adventofcode.com/2019/day/2)
## --- Day 2: 1202 Program Alarm ---
On the way to your [gravity assist](https://en.wikipedia.org/wiki/Gravity_assist) around the Moon, your ship computer beeps angrily about a "[1202 program alarm](https://www.hq.nasa.gov/alsj/a11/a11.landing.html#1023832)". On the radio, an Elf is already explaining how to handle the situation: "Don't worry, that's perfectly norma--" The ship computer [bursts into flames](https://en.wikipedia.org/wiki/Halt_and_Catch_Fire).

You notify the Elves that the computer's [magic smoke](https://en.wikipedia.org/wiki/Magic_smoke) seems to have escaped. "That computer ran <em>Intcode</em> programs like the gravity assist program it was working on; surely there are enough spare parts up there to build a new Intcode computer!"

An Intcode program is a list of [integers](https://en.wikipedia.org/wiki/Integer) separated by commas (like <code>1,0,0,3,99</code>).  To run one, start by looking at the first integer (called position <code>0</code>). Here, you will find an <em>opcode</em> - either <code>1</code>, <code>2</code>, or <code>99</code>. The opcode indicates what to do; for example, <code>99</code> means that the program is finished and should immediately halt. Encountering an unknown opcode means something went wrong.

Opcode <code>1</code> <em>adds</em> together numbers read from two positions and stores the result in a third position. The three integers <em>immediately after</em> the opcode tell you these three positions - the first two indicate the <em>positions</em> from which you should read the input values, and the third indicates the <em>position</em> at which the output should be stored.

For example, if your Intcode computer encounters <code>1,10,20,30</code>, it should read the values at positions <code>10</code> and <code>20</code>, add those values, and then overwrite the value at position <code>30</code> with their sum.

Opcode <code>2</code> works exactly like opcode <code>1</code>, except it <em>multiplies</em> the two inputs instead of adding them. Again, the three integers after the opcode indicate <em>where</em> the inputs and outputs are, not their values.

Once you're done processing an opcode, <em>move to the next one</em> by stepping forward <code>4</code> positions.

For example, suppose you have the following program:

<pre>
<code>1,9,10,3,2,3,11,0,99,30,40,50</code>
</pre>

For the purposes of illustration, here is the same program split into multiple lines:

<pre>
<code>1,9,10,3,
2,3,11,0,
99,
30,40,50
</code>
</pre>

The first four integers, <code>1,9,10,3</code>, are at positions <code>0</code>, <code>1</code>, <code>2</code>, and <code>3</code>. Together, they represent the first opcode (<code>1</code>, addition), the positions of the two inputs (<code>9</code> and <code>10</code>), and the position of the output (<code>3</code>).  To handle this opcode, you first need to get the values at the input positions: position <code>9</code> contains <code>30</code>, and position <code>10</code> contains <code>40</code>.  <em>Add</em> these numbers together to get <code>70</code>.  Then, store this value at the output position; here, the output position (<code>3</code>) is <em>at</em> position <code>3</code>, so it overwrites itself.  Afterward, the program looks like this:

<pre>
<code>1,9,10,<em>70</em>,
2,3,11,0,
99,
30,40,50
</code>
</pre>

Step forward <code>4</code> positions to reach the next opcode, <code>2</code>. This opcode works just like the previous, but it multiplies instead of adding.  The inputs are at positions <code>3</code> and <code>11</code>; these positions contain <code>70</code> and <code>50</code> respectively. Multiplying these produces <code>3500</code>; this is stored at position <code>0</code>:

<pre>
<code><em>3500</em>,9,10,70,
2,3,11,0,
99,
30,40,50
</code>
</pre>

Stepping forward <code>4</code> more positions arrives at opcode <code>99</code>, halting the program.

Here are the initial and final states of a few more small programs:


 - <code>1,0,0,0,99</code> becomes <code><em>2</em>,0,0,0,99</code> (<code>1 + 1 = 2</code>).
 - <code>2,3,0,3,99</code> becomes <code>2,3,0,<em>6</em>,99</code> (<code>3 * 2 = 6</code>).
 - <code>2,4,4,5,99,0</code> becomes <code>2,4,4,5,99,<em>9801</em></code> (<code>99 * 99 = 9801</code>).
 - <code>1,1,1,4,99,5,6,0,99</code> becomes <code><em>30</em>,1,1,4,<em>2</em>,5,6,0,99</code>.

Once you have a working computer, the first step is to restore the gravity assist program (your puzzle input) to the "1202 program alarm" state it had just before the last computer caught fire. To do this, <em>before running the program</em>, replace position <code>1</code> with the value <code>12</code> and replace position <code>2</code> with the value <code>2</code>. <em>What value is left at position <code>0</code></em> after the program halts?


## --- Part Two ---
"Good, the new computer seems to be working correctly!  <em>Keep it nearby</em> during this mission - you'll probably use it again. Real Intcode computers support many more features than your new one, but we'll let you know what they are as you need them."

"However, your current priority should be to complete your gravity assist around the Moon. For this mission to succeed, we should settle on some terminology for the parts you've already built."

Intcode programs are given as a list of integers; these values are used as the initial state for the computer's <em>memory</em>. When you run an Intcode program, make sure to start by initializing memory to the program's values. A position in memory is called an <em>address</em> (for example, the first value in memory is at "address 0").

Opcodes (like <code>1</code>, <code>2</code>, or <code>99</code>) mark the beginning of an <em>instruction</em>.  The values used immediately after an opcode, if any, are called the instruction's <em>parameters</em>.  For example, in the instruction <code>1,2,3,4</code>, <code>1</code> is the opcode; <code>2</code>, <code>3</code>, and <code>4</code> are the parameters. The instruction <code>99</code> contains only an opcode and has no parameters.

The address of the current instruction is called the <em>instruction pointer</em>; it starts at <code>0</code>.  After an instruction finishes, the instruction pointer increases by <em>the number of values in the instruction</em>; until you add more instructions to the computer, this is always <code>4</code> (<code>1</code> opcode + <code>3</code> parameters) for the add and multiply instructions. (The halt instruction would increase the instruction pointer by <code>1</code>, but it halts the program instead.)

"With terminology out of the way, we're ready to proceed. To complete the gravity assist, you need to <em>determine what pair of inputs produces the output <code>19690720</code></em>."

The inputs should still be provided to the program by replacing the values at addresses <code>1</code> and <code>2</code>, just like before.  In this program, the value placed in address <code>1</code> is called the <em>noun</em>, and the value placed in address <code>2</code> is called the <em>verb</em>.   Each of the two input values will be between <code>0</code> and <code>99</code>, inclusive.

Once the program has halted, its output is available at address <code>0</code>, also just like before. Each time you try a pair of inputs, make sure you first <em>reset the computer's memory to the values in the program</em> (your puzzle input) - in other words, don't reuse memory from a previous attempt.

Find the input <em>noun</em> and <em>verb</em> that cause the program to produce the output <code>19690720</code>. <em>What is <code>100 * noun + verb</code>?</em> (For example, if <code>noun=12</code> and <code>verb=2</code>, the answer would be <code>1202</code>.)


