using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DrawingCanvasApp
{
    class Program
    {
        #region Static Variable Declaration
        static string commandExp = "Please enter command => 'C - Canvas', 'L - Line', 'R - Rectangle', 'B - Fill Color' , 'Q - Quit' ";
        #endregion

        #region Main Program
        static void Main(string[] args)
        {
            Console.WriteLine("Drawing Canvas Application");

            Console.WindowHeight = Console.LargestWindowHeight - 20;
            Console.WindowWidth = Console.LargestWindowWidth - 50;
            Console.SetWindowPosition(0, 0);
            
            Console.WriteLine(commandExp);
            string command = Console.ReadLine();
            GetCommand(command);
            
        }
        #endregion

        #region Get Command Line from Users
        private static void GetCommand(string command)
        {
            command = command.ToUpper();
            switch (command)
            {
                case "C":
                case "B":
                case "R":
                case "L":
                    CommandCanvas(command);
                    Console.WriteLine("\n" + commandExp);
                    command = Console.ReadLine();
                    GetCommand(command);
                    break;                                           
                case "Q":
                    Console.WriteLine("Quit");
                    break;
                default:
                    Console.WriteLine("Please enter proper argument");
                    command=Console.ReadLine();
                    GetCommand(command);
                    break;
            }
        }
        #endregion

        #region Create Canvas Based on User input
        private static void CommandCanvas(string command)
        {
            Console.WriteLine("Please enter canvas width/height for ex: 20=> Press Enter=> 20 ");
            string canvasw = Console.ReadLine();
            int cw = 0;
            bool success = (int.TryParse(canvasw, out cw));
            if (!success)
            {
                Console.WriteLine("Please enter proper width");
            }
            else
            {
                string canvash = Console.ReadLine();
                int ch = 0;
                success = (int.TryParse(canvash, out ch));
                if (!success)
                {
                    Console.WriteLine("Please enter proper height");
                }
                else
                {
                    int bcolor = 0;
                    if (command.ToUpper() == "B")
                    {
                        Console.WriteLine("Please enter background color number range between 0 to 15.");
                        success = (int.TryParse(Console.ReadLine(), out bcolor));
                        if (!success)
                        {
                            Console.WriteLine("Please enter proper value.");
                        }                        
                    }
                    if (command.ToUpper() == "R")
                    {
                        Console.WriteLine("Please enter width, height, x, y location for rectangle");
                        int rw = 0;
                        int rh = 0;
                        int rt = 0;
                        int rl = 0;
                        success = (int.TryParse(Console.ReadLine(), out rw));
                        if (success)
                        {
                            success = (int.TryParse(Console.ReadLine(), out rh));
                            if (success)
                            {
                                success = (int.TryParse(Console.ReadLine(), out rt));
                                if (success)
                                {
                                    success = (int.TryParse(Console.ReadLine(), out rl));
                                }
                            }
                        }
                        
                        if (!success || cw<rw || ch<rh)
                        {
                            Console.WriteLine("Please enter proper value or rect height/width is highter than canvas height/width");
                            Console.ReadKey();
                        }
                        else
                        {
                            ConsoleCanvas cc = new ConsoleCanvas(cw, ch, rw, rh, rt, rl, command);
                            cc.DrawCanvas();                            
                        }
                    }
                    else if(command.ToUpper() == "L")
                    {
                        Console.WriteLine("Please enter x1,x2,y1,y2 for Line");
                        int x1 = 0;
                        int y1 = 0;
                        int x2 = 0;
                        int y2 = 0;
                        success = (int.TryParse(Console.ReadLine(), out x1));
                        if (success)
                        {
                            success = (int.TryParse(Console.ReadLine(), out y1));
                            if (success)
                            {
                                success = (int.TryParse(Console.ReadLine(), out x2));
                                if (success)
                                {
                                    success = (int.TryParse(Console.ReadLine(), out y2));
                                }
                            }
                        }
                        if (!success)
                        {
                            Console.WriteLine("Please enter proper value.");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (x1 != x2 && y1 != y2)
                            {
                                Console.WriteLine("Please enter x1 and x2 same or y1 and y2 same.");
                                Console.ReadKey();
                            }
                            else
                            {
                                string type = (x1 == x2) ? "H" : "V";
                                ConsoleCanvas cc = new ConsoleCanvas(command, cw, ch,type, x1, y1, x2, y2);
                                cc.DrawCanvas();
                            }
                        }
                    }
                    else
                    {
                        ConsoleCanvas cc = new ConsoleCanvas(cw, ch, command, bcolor);
                        cc.DrawCanvas();
                    }
                }
            }
        }
        #endregion

    }

    #region Drawing Shapes in Canvas
    /*
     * Draw Canvas using Width and Height
     */
    public class ConsoleCanvas
    {

        #region Declare Properties
        public int CanvasWidth
        {
            get; set;
        }

        public int CanvasHeight
        {
            get; set;
        }

        public int BackgroundColor
        {
            get; set;
        }

        public string Command
        {
            get;set;
        }

        public int RectWidth
        {
            get; set;
        }

        public int RectHeight
        {
            get; set;
        }

        public int RectLeft
        {
            get; set;
        }

        public int RectTop
        {
            get; set;
        }

        public int LX1
        {
            get; set;
        }
        public int LY1
        {
            get; set;
        }
        public int LX2
        {
            get; set;
        }
        public int LY2
        {
            get; set;
        }

        public string LineType
        {
            get;set;
        }

        #endregion

        #region Parameterized Constructor
        public ConsoleCanvas(int width, int height, string command, int bcolor)
        {
            CanvasWidth = width;
            CanvasHeight = height;
            BackgroundColor = bcolor;
            Command = command;
        }

        public ConsoleCanvas(int width, int height, int rwidth, int rheight, int top, int left, string command)
        {
            CanvasHeight = height;
            CanvasWidth = width;
            RectWidth = rwidth;
            RectHeight= rheight;
            RectTop = top;
            RectLeft = left;
            Command = command;
        }

        public ConsoleCanvas(string command, int width, int height,string type,int x1, int y1, int x2, int y2)
        {
            CanvasHeight = height;
            CanvasWidth = width;
            LX1 = x1;
            LY1 = y1;
            LX2 = x2; 
            LY2 = y2;
            Command = command;
            LineType = type;
        }

        #endregion

        #region Draw Canvas
        /*
         * Draw Canvas using width and height with selected fill color using x character
         */
        public void DrawCanvas()
        {
            string strCanvas = "x";
            string space = "";
            string temp = "";
            int top = Console.CursorTop;
            int left = Console.CursorLeft;


            for (int i = 0; i < CanvasWidth; i++)
            {
                space += " ";
                strCanvas += "x";
            }

            for (int j = 0; j < left; j++)
                temp += " ";

            strCanvas += "x" + "\n";

            for (int i = 0; i < CanvasHeight; i++)
                strCanvas += temp + "x" + space + "x" + "\n";

            strCanvas += temp + "x";
            for (int i = 0; i < CanvasWidth; i++)
                strCanvas += "x";

            strCanvas += "x" + "\n";


            Type type = typeof(ConsoleColor);
            ConsoleColor c = (ConsoleColor)BackgroundColor;
            Console.BackgroundColor = c;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(strCanvas);
            Console.ResetColor();

            int originalTop = Console.CursorTop;

            if(Command == "R")
            {
                RectTop = top + RectTop;
                RectLeft = left + RectLeft;
                Console.SetCursorPosition(RectLeft, RectTop);
                DrawRect();
                Console.CursorTop = originalTop;
            }
            else if(Command == "L")
            {
                top = top + LY1;
                left = left + LX1;
                Console.SetCursorPosition(left, top);
                DrawLine();
                Console.CursorTop = originalTop;
            }
        }

        #endregion

        #region Draw Rectangle
        /*
         * *Draw rectangle using width and height with selected position with in a canvas
         */
        public void DrawRect()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            string strCanvas = "x";
            string space = "";
            int top = RectTop;
            int left = RectLeft;
            for (int i = 0; i <= RectWidth; i++)
            {
                space += " ";
                strCanvas += "x";
            }
            strCanvas += "x" + "\n";
            Console.Write(strCanvas);
            Console.CursorLeft = RectLeft;
            strCanvas = "";
            for (int i = 0; i < RectHeight; i++)
            {
                strCanvas = "x" + space + "x" + "\n";
                Console.Write(strCanvas);
                Console.CursorLeft = RectLeft;
            }

            Console.CursorLeft = RectLeft;
            strCanvas = "x";
            space = "";

            for (int i = 0; i <= RectWidth; i++)
            {
                space += " ";
                strCanvas += "x";
            }
            strCanvas += "x" + "\n";
            
            Console.Write(strCanvas);
            Console.ResetColor();
        }
        #endregion

        #region Draw Line
        /*
         * Draw a line using x1, y1 and x2, y2 within a canvas
         */
        public void DrawLine()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string strCanvas = "x";
            string space = "";
            int top = LY1;
            int left = LX1;

            if (LineType == "V")
            {
                for (int i = LX1; i <= LX2; i++)
                {
                    space += " ";
                    strCanvas += "x";
                }
            }
            else
            {
                for (int i = LY1; i <= LY2; i++)
                {
                    space += " ";
                    strCanvas = "x" + "\n";
                    Console.Write(strCanvas);
                    Console.CursorLeft = left;
                }
            }
            strCanvas += "\n";
            
            Console.Write(strCanvas);
            Console.ResetColor();
        }
        #endregion
    }
    #endregion
}
