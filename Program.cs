using System;
using System.Drawing;
using System.Threading;
using System.IO;
 
namespace BrainFuck
{
    class BrainFuckInterpreter
    {
        private static string VER = "0.0.0.1";
        private static readonly int BUFSIZE = 65535;
        private int[] buf = new int[BUFSIZE];
        private int ptr { get; set; }
        private bool echo { get; set; }
        public static bool censored { get; set; } = true;
 
        public BrainFuckInterpreter()
        {
            this.ptr = 0;
            this.Reset();
        }
 
        public static void PrintHelp()
        {
            Console.WriteLine("BrainFuck Interpreter " + VER);
            Console.WriteLine("Parameter: -h: Print Help");
            Console.WriteLine("Parameter: -e: Enable Echo Input Text");
            Console.WriteLine("Parameter: -E: use Example.txt as an example");
            Console.WriteLine("Parameter: -c: Enable text Censoring(does not work when inputed from brainfuck)");
            Console.WriteLine("Parameter: -d: Disable Echo Input Text and censoring");
            Console.WriteLine("Parameter: -p: Enable Keyboard Input");
            Console.WriteLine("Parameter: -v: Print Version");
            Console.WriteLine("Parameter: FileName");
            Console.WriteLine(censored);
        }
 
        public void Reset()
        {
            Array.Clear(this.buf, 0, this.buf.Length);
        }
 
        public void Interpret(string s)
        {
            int i = 0;
            int right = s.Length;
            while (i < right)
            {
                switch (s[i])
                {
                    case '>':
                        {
                            this.ptr++;
                            if (this.ptr >= BUFSIZE)
                            {
                                this.ptr = 0;
                            }
                            break;
                        }
                    case '<':
                        {
                            this.ptr--;
                            if (this.ptr < 0)
                            {
                                this.ptr = BUFSIZE - 1;
                            }
                            break;
                        }
                    case '.':
                        {
                            Console.Write((char)this.buf[this.ptr]);
                            break;
                        }
                    case '+':
                        {
                            this.buf[this.ptr]++;
                            break;
                        }
                    case '-':
                        {
                            this.buf[this.ptr]--;
                            break;
                        }
                    case '[':
                        {
                            if (this.buf[this.ptr] == 0)
                            {
                                int loop = 1;
                                while (loop > 0)
                                {
                                    i ++;
                                    char c = s[i];
                                    if (c == '[')
                                    {
                                        loop ++;
                                    }
                                    else
                                    if (c == ']')
                                    {
                                        loop --;
                                    }
                                }
                            }
                            break;
                        }
                    case ']':
                        {
                            int loop = 1;
                            while (loop > 0)
                            {
                                i --;
                                char c = s[i];
                                if (c == '[')
                                {
                                    loop --;
                                }
                                else
                                if (c == ']')
                                {
                                    loop ++;
                                }
                            }
                            i --;
                            break;
                        }
                    case ',':
                        {
                            // read a key
                            ConsoleKeyInfo key = Console.ReadKey(this.echo);
                            this.buf[this.ptr] = (int)key.KeyChar;
                            break;
                        }
                }
                i++; 
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Console.Title = "BrainFuck Interpreter";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Green;
            
            BrainFuckInterpreter bf2 = new BrainFuckInterpreter();
            if (args.Length == 0)
            {
                Console.Clear();
                
                Console.Write(@"-.`..................................................................................--.....-.......
-       `````````````````````...............................................`.-----.---.-...:-....`-
-`..........................------------------------------------------------......................`:
-`-..............................................................................................-`:
-`-`````````````````````````````````````````````````````````````````````````````````````````````.-`:
-`-                                                                                             `-`:
-`-                                                                               ``````````    `-`:
-`-  .osssssssssssssssooooooooooooooooooooooooooooo-     ````````````````````    ``````````.`   `-`:
-`-  .ssssssssssssssssssssssssssosssooooooooooooooo:`    ````````````````````    `.```````.-.`  `-`:
-`-  .ossssssssssssssssssoooooooooooooooooooooooooo:`    ````````````````````     +oooooooo/.`  `-`:
-`-  .ossssoooooooooooooooooooooooooooooooooooooooo:`    :/:-.``                  +ooo+++++/.`  `- :
-`-  .ooooooooooooooooooooooooooooooooooooo++++++oo:`    /ysossso+//:-`           +ooo+++++/.`  `- :
-`-  .ooooooooooooooooooooooooooooooooooooooooo+oo+:`    :y+ ``.-:/+osss/.       `+++++++++/.` ``- :
-`-  .oooooooooooooooooooooooooooooo+++++++++++++++-`    /y+``````````./ss-``    `+++++++++/.`  `- :
-`-  .+oooooooooooooooooo+++ooooooooooooooooooo++++:`    /y+````````````/y+``    `/++++++++/.` ``- :
:--  .+ooooo+++++++++oooooooooosooooooooooooooooooo:`    /yo````````````/yo``    `/+++/+++//.````-.:
:--  .++++++++++oooosssssssssoooooooooooooooooooooo:`    :so````````````oy+``    `/+++/++++/.````:-:
:--  .++++++oossssssssssssooooooooooooooooooooooooo:`    .ss.   ```````/ss.``    `//+///////.````:-:
:--  .+++oosssssssssssssooooooooooooooooooooooooooo:`    `sy-        `+ys-```    `/+++///+//.````:-:
:--  .+ossssssssssssosooooooooooooooooooooooooooooo:.    `sy:```````:ss+.`````   `//////////.````--:
:--  .ossssssssssssoooooooooooooooooooooooooooooooo:.   ``+y/`````-oyo-```````  ``//////////.````--:
:--  .osssssssssooooooooooooooooooooooooooooooooooo:.   ``/yo```:oso:`````````` ``/////////:.````--:
:--  .ossssssoooooooooooooooooooooooooooooooooooooo:.   ``/yo``+so:```````````````//////////.````--:
:--  .osssosooooooooooooooooooooooooooooooooooooooo:.     /yo``+ss/```````````````//////////.````--:
:--  .ooooooooooooooooooooooooooooooooooooooooooooo:.`    /yo`  ./ss/.````````````-:::::::::.````--:
:-- `.ooooooooooooooooooooooooooooooooooooooooooooo:.`  ``/yo``````/ss+-`````````````.......`````:-:
---` .oooooooooooooooooooooooooooooooooooooooooooo+:.`````/yo.``````.:ss+-```````````````````````--:
---``.oooooooooooooooooooooooooooooooooooooooooooo+:.`````/yo.````````./ss/``````````````````````--:
---``.+ooooooooooooooooooooooooooooooooooooooooooo+:.`````/yo```````````-oyo.````````````````````:-:
---``.+oooooooooooooooooooooooooooooooooooooooo++++:.`````/yo.```````````.ss:````````````````````:-:
:--``.+ooooooooooooooooooooooooooooooooo+++oo++++++:.`````:ys.```````````.sy:````````````````````:-:
:--``.+ooooooooooooooooooooooooooooooo+++++++++++++:.`````-yy-``......-:/oso.````````````````````:-:
:--``.+ooooooooooooooooooooooooooooo+++++++++++++++:.`````.syooosssssysso+:..````````````````````:-:
:--``.+oooooooooooooooooooooooooooooooo++++++++++++:.``````.::::---....``````````````````````````:-:
:--``.oooooooooooooooooooooooooo+++++++++++++++++++:.````````````````````````````````````````````:-:
:--``.+ooo+++++++++++++++++++++++++++++++++++++++++:.````````````````````````````````````````````:-:
:--``.:////////////////////////////////////////////:.````````````````````````````````````````````:-:
:--````.............................................`````````````````````````````````````````````:-:
:--``````````````````````````````````````````````````````````````````````````````````````````````:-:
:.-------------------------------------------------------------------------------------------------:
---------------------------------------------------------------------------------------------------:
");Console.WriteLine("\n=====================================================================================================");
                if(BrainFuckInterpreter.censored == true)
                {
                    Console.WriteLine("BrainF*** Interpreter " + BrainFuckInterpreter.VER);
                    Console.Write("No argument? Then the -p parameter goes active by deafult(no changing!), Now type your BrainFuck code. \n");
                }
                else
                {
                    Console.WriteLine("BrainFuck Interpreter " + BrainFuckInterpreter.VER);
                    Console.Write("No argument? Then the -p parameter goes active, Now type your BrainFuck code. \n");
                }
                Thread.Sleep(1000);
                Console.Write("If you need help? Use -h \n");
                Console.Write("Type exit to exit, type -h to get help \n");
                Console.Write("Here is an example: ++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++. \n");
                Console.Write("=========================================================================================================================================\n");
                string src = Console.ReadLine();
                if(src =="exit")
                {
                    System.Environment.Exit(0);
                }
                else if(src =="-h")
                {
                    PrintHelp();
                    Console.ReadLine();
                }
                else{
                    try{
                        bf2.Interpret(src);
                    }
                    catch(System.Exception e)
                    {
                        Console.WriteLine("Err, Exceptions. Something went wrong in the interpretation of the code: " + src + ". Error: " + e.Message);
                    }
                    
                }
                
            }
            else
            {
                Console.Write(@"-.`..................................................................................--.....-.......
-       `````````````````````...............................................`.-----.---.-...:-....`-
-`..........................------------------------------------------------......................`:
-`-..............................................................................................-`:
-`-`````````````````````````````````````````````````````````````````````````````````````````````.-`:
-`-                                                                                             `-`:
-`-                                                                               ``````````    `-`:
-`-  .osssssssssssssssooooooooooooooooooooooooooooo-     ````````````````````    ``````````.`   `-`:
-`-  .ssssssssssssssssssssssssssosssooooooooooooooo:`    ````````````````````    `.```````.-.`  `-`:
-`-  .ossssssssssssssssssoooooooooooooooooooooooooo:`    ````````````````````     +oooooooo/.`  `-`:
-`-  .ossssoooooooooooooooooooooooooooooooooooooooo:`    :/:-.``                  +ooo+++++/.`  `- :
-`-  .ooooooooooooooooooooooooooooooooooooo++++++oo:`    /ysossso+//:-`           +ooo+++++/.`  `- :
-`-  .ooooooooooooooooooooooooooooooooooooooooo+oo+:`    :y+ ``.-:/+osss/.       `+++++++++/.` ``- :
-`-  .oooooooooooooooooooooooooooooo+++++++++++++++-`    /y+``````````./ss-``    `+++++++++/.`  `- :
-`-  .+oooooooooooooooooo+++ooooooooooooooooooo++++:`    /y+````````````/y+``    `/++++++++/.` ``- :
:--  .+ooooo+++++++++oooooooooosooooooooooooooooooo:`    /yo````````````/yo``    `/+++/+++//.````-.:
:--  .++++++++++oooosssssssssoooooooooooooooooooooo:`    :so````````````oy+``    `/+++/++++/.````:-:
:--  .++++++oossssssssssssooooooooooooooooooooooooo:`    .ss.   ```````/ss.``    `//+///////.````:-:
:--  .+++oosssssssssssssooooooooooooooooooooooooooo:`    `sy-        `+ys-```    `/+++///+//.````:-:
:--  .+ossssssssssssosooooooooooooooooooooooooooooo:.    `sy:```````:ss+.`````   `//////////.````--:
:--  .ossssssssssssoooooooooooooooooooooooooooooooo:.   ``+y/`````-oyo-```````  ``//////////.````--:
:--  .osssssssssooooooooooooooooooooooooooooooooooo:.   ``/yo```:oso:`````````` ``/////////:.````--:
:--  .ossssssoooooooooooooooooooooooooooooooooooooo:.   ``/yo``+so:```````````````//////////.````--:
:--  .osssosooooooooooooooooooooooooooooooooooooooo:.     /yo``+ss/```````````````//////////.````--:
:--  .ooooooooooooooooooooooooooooooooooooooooooooo:.`    /yo`  ./ss/.````````````-:::::::::.````--:
:-- `.ooooooooooooooooooooooooooooooooooooooooooooo:.`  ``/yo``````/ss+-`````````````.......`````:-:
---` .oooooooooooooooooooooooooooooooooooooooooooo+:.`````/yo.``````.:ss+-```````````````````````--:
---``.oooooooooooooooooooooooooooooooooooooooooooo+:.`````/yo.````````./ss/``````````````````````--:
---``.+ooooooooooooooooooooooooooooooooooooooooooo+:.`````/yo```````````-oyo.````````````````````:-:
---``.+oooooooooooooooooooooooooooooooooooooooo++++:.`````/yo.```````````.ss:````````````````````:-:
:--``.+ooooooooooooooooooooooooooooooooo+++oo++++++:.`````:ys.```````````.sy:````````````````````:-:
:--``.+ooooooooooooooooooooooooooooooo+++++++++++++:.`````-yy-``......-:/oso.````````````````````:-:
:--``.+ooooooooooooooooooooooooooooo+++++++++++++++:.`````.syooosssssysso+:..````````````````````:-:
:--``.+oooooooooooooooooooooooooooooooo++++++++++++:.``````.::::---....``````````````````````````:-:
:--``.oooooooooooooooooooooooooo+++++++++++++++++++:.````````````````````````````````````````````:-:
:--``.+ooo+++++++++++++++++++++++++++++++++++++++++:.````````````````````````````````````````````:-:
:--``.:////////////////////////////////////////////:.````````````````````````````````````````````:-:
:--````.............................................`````````````````````````````````````````````:-:
:--``````````````````````````````````````````````````````````````````````````````````````````````:-:
:.-------------------------------------------------------------------------------------------------:
---------------------------------------------------------------------------------------------------:
");
Console.WriteLine("\n=====================================================================================================");
Console.WriteLine("BrainF*** Interpreter " + BrainFuckInterpreter.VER);
                BrainFuckInterpreter bf = new BrainFuckInterpreter();
                string char1 = "ex";
                foreach (string s in args)
                {
                    if (s[0] == '-') // switch options
                    {
                        for (int i = 1; i < s.Length; i++)
                        {
                            switch (s[i])
                            {
                                case 'h':
                                    {
                                        PrintHelp();
                                        break;
                                    }
                                case 'd':
                                    {
                                        bf.echo = false;
                                        BrainFuckInterpreter.censored = false;
                                        break;
                                    }
                                case 'v':
                                    {
                                        Console.WriteLine(VER);
                                        break;
                                    }
                                case 'e':
                                    {
                                        bf.echo = true;
                                        break;
                                    }
                                case 'p':
                                    {
                                        
                                        Console.Write("Type your brainfuck code \n");
                                        Console.Write("Type exit to exit \n");
                                        Console.Write("Here is an example: ++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++. \n");
                                        Console.Write("=====================================================================================================\n");
                                        string src = Console.ReadLine();
                                        if(src !="exit"){
                                            try{
                                                bf.Interpret(src);
                                            }
                                            catch(System.Exception e)
                                            {
                                                Console.WriteLine("Err, Exceptions. Something went wrong in the interpretation of the code: " + src + ". Error: " + e.Message);
                                            }
                                            
                                        }
                                        System.Environment.Exit(0);
                                        break;
                                    }
                                case 'c':
                                    {
                                        BrainFuckInterpreter.censored = true;
                                        break;
                                    }
                                case 'E':
                                    {
                                        try
                                        {
                                            Console.WriteLine("Reading File: example.txt");
                                            bf.Interpret(File.ReadAllText("example.txt"));
                                            break;
                                        }
                                        catch (System.Exception e)
                                        {
                                           Console.WriteLine("Err, Exceptions. There was a problem opening the file " + s + " . Error: " + e.Message);
                                           break;
                                        }
                                        finally
                                        {
                                            System.Environment.Exit(1);
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("It desn't match any of the parameters, try -h if you need help.");
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine("Reading File: " + s);
                            bf.Interpret(File.ReadAllText(s));
                        }
                        catch (System.Exception e)
                        {
                            
                            Console.WriteLine("Err, Exceptions. There was a problem opening the file " + s + " . Error: " + e.Message);
                        }
                        finally
                        {
                            System.Environment.Exit(1);
                        }
                    }
                }
            }
        }
    }
}