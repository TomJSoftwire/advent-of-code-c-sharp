
using System;

namespace AdventOfCode.Y2022;

class SplashScreenImpl : SplashScreen {

    public void Show() {

        var color = Console.ForegroundColor;
        Write(0xcc00, false, "           ▄█▄ ▄▄█ ▄ ▄ ▄▄▄ ▄▄ ▄█▄  ▄▄▄ ▄█  ▄▄ ▄▄▄ ▄▄█ ▄▄▄\n           █▄█ █ █ █ █ █▄█ █ █ █   █ █ █▄ ");
            Write(0xcc00, false, " █  █ █ █ █ █▄█\n           █ █ █▄█ ▀▄▀ █▄▄ █ █ █▄  █▄█ █   █▄ █▄█ █▄█ █▄▄  int y = 2022;\n           ");
            Write(0xcc00, false, " \n           ");
            Write(0x333333, false, "@@@#@##@@@@@@##@@#@##@##@@@@@@@##@##@@##@#@@#@@@@  ");
            Write(0x666666, false, "25\n           ");
            Write(0x333333, false, "#@##@@@#@@@@@@##@@@#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  ");
            Write(0x666666, false, "24\n           ");
            Write(0x333333, false, "@@@@#@@@@@@##@@@#@@@@@@@@@#@@##@#@@@@#@@@#@##@@@@  ");
            Write(0x666666, false, "23\n           ");
            Write(0x333333, false, "@##@##@@@@@##@@@@#@@@@#@@####@@@##@@#@@@@@#@@@@@#  ");
            Write(0x666666, false, "22\n           ");
            Write(0x333333, false, "@@@@@@@@@#@#@@@@@@@@#@@@@@@@@@@@#@@@#@@@@#@@@@@@@  ");
            Write(0x666666, false, "21\n           ");
            Write(0x333333, false, "@#@#@#@#@@@@@#@@@@@#@@@@@#@#@@@@@@@@@@@@@#@@#@@@@  ");
            Write(0x666666, false, "20\n           ");
            Write(0x333333, false, "##@@@@#@@@##@@@@#@@##@@@@@@@@@@@@@@#@@@@#@@###@@@  ");
            Write(0x666666, false, "19\n           ");
            Write(0x333333, false, "#@@@##@@#@@#@@##@@#@@@@@#@@#@@#@@@@##@##@@#@#@@@#  ");
            Write(0x666666, false, "18\n           ");
            Write(0x333333, false, "@@##@@#@##@@@#@@@@###@#@#@@@@@#@@##@@#@##@@#@#@@@  ");
            Write(0x666666, false, "17\n           ");
            Write(0x333333, false, "@@@@@##@##@@###@@@#@@@@@@@@@@@#@@@@##@@#@#@#@@@@@  ");
            Write(0x666666, false, "16\n           ");
            Write(0x333333, false, "#@#@#@@##@@@@@#@@@@@@@@@@#@@@#@@@@@@@@@@@@@@#@@@#  ");
            Write(0x666666, false, "15\n           ");
            Write(0x333333, false, "@@@@@@###@@@@@@#@#@####@#@@@#@@@@@#@@@@##@#@@#@@@  ");
            Write(0x666666, false, "14\n           ");
            Write(0x333333, false, "##@@@@@@@#@@@#@@@@##@@@@##@@@#@@#@#@###@#@@@@@@#@  ");
            Write(0x666666, false, "13\n           ");
            Write(0x333333, false, "@@#@##@@@#@@#@@##@##@#@@@@#@@@@@@@#@##@@##@#@@#@@  ");
            Write(0x666666, false, "12\n           ");
            Write(0x333333, false, "@@@@@@@@#@@#@#@@@@#@@##@@@@@#@@@@#@@#@#@@@#@@@@@#  ");
            Write(0x666666, false, "11\n           ");
            Write(0x333333, false, "@#@@#@@@#@#@@@@@@@@@@@@@#|@@@@@#@@#@@@#@##@@#####  ");
            Write(0x666666, false, "10\n           ");
            Write(0x333333, false, "@@##@@@#@@@###@@@@@#@@@@@@#@@@#@@#@@@##@@@#@@@@@@  ");
            Write(0x666666, false, " 9\n           ");
            Write(0x333333, false, "@##@@@@@@#@@@#@#@#@@@@@#@@@@#@#@@#@#@@###@#@#@@##  ");
            Write(0x666666, false, " 8\n           ");
            Write(0x333333, false, "@@@@@@#@#@@@@@@@#@#@@@@@@@@@@@#@@#@@@@@@@#@#@@#@@  ");
            Write(0x666666, false, " 7\n           ");
            Write(0x333333, false, "#@@#@@#@#@@@@@@@@#@@#@#@@@@#@@@#@###@@##@##@#@@@@  ");
            Write(0x666666, false, " 6\n           ");
            Write(0x333333, false, "#@@@#@@@@@@@@###@#@##@#@@@@##@@#@@@@@#@@#@#@@@@@@  ");
            Write(0x666666, false, " 5\n           ");
            Write(0x333333, false, "@#@@@@@@@@#@#@@@#@@@@@@@#@@#@@@@@@#@@@@@##@@#@#@@  ");
            Write(0x666666, false, " 4\n           ");
            Write(0x333333, false, "@@@#@@@@@@###@#@@#@@@@@@#@@@@###@#####@#@@@#@@@@#  ");
            Write(0x666666, false, " 3\n           -~------'    ~    ~ '--~-----~-~----___________--  ");
            Write(0xcccccc, false, " 2 ");
            Write(0xffff66, false, "*");
            Write(0x666666, false, "*\n           ");
            Write(0x5eabb4, false, "  ~    ~  ~      ~     ~ ~   ~     ~  ~  ~   ~     ");
            Write(0xcccccc, false, " 1 ");
            Write(0xffff66, false, "**\n           \n");
            
        Console.ForegroundColor = color;
        Console.WriteLine();
    }

   private static void Write(int rgb, bool bold, string text){
       Console.Write($"\u001b[38;2;{(rgb>>16)&255};{(rgb>>8)&255};{rgb&255}{(bold ? ";1" : "")}m{text}");
   }
}
