using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class Lobby
    {
        //door on array
        //beep sounds

        
        static bool onLobby = true;
        public static int windowLength;
        public static int windowHight;
        public static int topBorder = 4;
        public static int topBorderToRoom = 11;
        public static int sideBorder = 8;
        public static int sideBorderToRoom = windowLength / 2 - Room.roomLength / 2;
        public static int gamemode;
        static ConsoleColor titelColor = ConsoleColor.Black;
        public static ConsoleColor defaultBColor = ConsoleColor.White;
        public static ConsoleColor defaultFColor = ConsoleColor.Black;
        public static ConsoleColor imputColor = ConsoleColor.DarkGreen;
        public static ConsoleColor sizeColor = ConsoleColor.DarkYellow;
        public static string textBorder = @"
        ";
        static int times;
        static char[] loading = {'|', '/', '-', '\\'};
        static string gameTitel = @"
         __          __  _                            _          _   _          
         \ \        / / | |                          | |        | | | |         
          \ \  /\  / /__| | ___ ___  _ __ ___   ___  | |_ ___   | |_| |__   ___ 
           \ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \  | __| '_ \ / _ \
            \  /\  /  __/ | (_| (_) | | | | | |  __/ | |_ (_) | | |_| | | |  __/
             \/  \/ \___|_|\___\___/|_| |_| |_|\___| \___\___/  \___|_| |_|\___|
                                                                           
                                                                           
             ______                            _____                       _ 
            |  ____|                          |  __ \                     | |
            | |__   ___  ___ __ _ _ __   ___  | |__) |___   ___  _ __ ___ | |
            |  __| / __|/ __/ _` | '_ \ / _ \ |  _   / _ \ / _ \| '_ ` _ \| |
            | |____\__ \ (_| (_| | |_) |  __/ | | \ \ (_) | (_) | | | | | |_|
            |______|___/\___\__,_| .__/ \___| |_|  \_\___/ \___/|_| |_| |_(_)
                                 | |                                         
                                 |_|";
        

        static void Main(string[] args)
        {
            PrintLobby();
        }

        public static void PrintLobby()
        {
            windowLength = 90;
            windowHight = 29;
            times = 0;
            onLobby = true;
            Console.CursorVisible = true;
            Console.SetWindowSize(windowLength, windowHight);
            SetColorsToDefalt();
            Console.Clear();
            PrinGameText(gameTitel, 0, 1);
            //█▀▄
            PrintBackground(ConsoleColor.Gray);

            Console.SetCursorPosition(windowLength / 2 - 14, 20);
            Console.Write("Press any "); //10
            PrintWithColor("key", imputColor); //3
            Console.Write(" to get started."); //16

            Console.ReadKey(true);

            GetGamemode(0);

            GetStarted();
        }

        static void GetGamemode(int _gm)
        {
            PrintGm(_gm);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.RightArrow && _gm == 0)
                {
                    PrintGm(1);
                    _gm = 1;
                    gamemode = 1;
                    continue;
                }
                else if (key.Key == ConsoleKey.LeftArrow && _gm == 1)
                {
                    PrintGm(0);
                    _gm = 0;
                    gamemode = 0;
                    continue;
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    break;
                }
            }
        }

        private static void PrintGm(int _gm)
        {
            ConsoleColor gm0BColor;
            ConsoleColor gm0FColor;
            ConsoleColor gm1BColor;
            ConsoleColor gm1FColor;
            if (_gm == 0)
            {
                gamemode = 0;
                gm0BColor = ConsoleColor.DarkGray;
                gm0FColor = ConsoleColor.White;
                gm1BColor = ConsoleColor.Gray;
                gm1FColor = ConsoleColor.Black;
            }
            else
            {
                gamemode = 1;
                gm0BColor = ConsoleColor.Gray;
                gm0FColor = ConsoleColor.Black;
                gm1BColor = ConsoleColor.DarkGray;
                gm1FColor = ConsoleColor.White;
            }

            Console.BackgroundColor = gm0BColor;
            Console.ForegroundColor = gm0FColor;
            for (int y = 0; y < 3; y++)
            {
                Console.SetCursorPosition(windowLength / 4, 22 + y);
                Console.Write("           "); //11
            }
            Console.SetCursorPosition(windowLength / 4 + 2, 23);
            Console.Write("Default"); //7


            Console.BackgroundColor = gm1BColor;
            Console.ForegroundColor = gm1FColor;
            for (int y = 0; y < 3; y++)
            {
                Console.SetCursorPosition(windowLength / 4 * 3 - 9, 22 + y);
                Console.Write("           "); //11
            }
            Console.SetCursorPosition(windowLength / 4 * 3 - 7, 23);
            Console.Write("Sandbox"); //7

            SetColorsToDefalt();
            Console.SetCursorPosition(windowLength / 2 - 24, 20);
            Console.Write("Use "); //4
            PrintWithColor("RightArrow", imputColor); //10
            Console.Write(" or "); //4
            PrintWithColor("LeftArrow", imputColor); //9
            Console.Write(" to switch gamemode."); //20
        }

        private static void GetStarted()
        {
            //ColorChangeAnimation(gameTitel, 0, 1);
            SetColorsToDefalt();
            onLobby = false;

            Room.roomLength = 22;
            Room.roomHeight = 12;
            Room.InitializeRoom();
            Console.CursorVisible = false;

            if (gamemode == 0)
            {
                windowHight = 33;
                DefaultGm.PrintDefaultGmPage();
            }
            else if (gamemode == 1)
            {
                windowHight = 37;
                SandboxGm.PrintSandboxGmPage();
            }
            
        }

        public static void ColorChangeAnimation(string _gameText, int _x, int _y)
        {
            Console.CursorVisible = false;
            while (times < 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    PrinGameText(_gameText, _x, _y);
                    PrintBackground(titelColor);
                    if (onLobby)
                    {
                        Console.SetCursorPosition(windowLength / 2, 22);
                        Console.Write(loading[i]);
                    }
                    Thread.Sleep(250);
                }
                times++;
            }
            times = 0;
        }

        private static ConsoleColor PrinGameText(string _gameText, int _x, int _y)
        {
            Random RndColor = new Random();
            int rnd = RndColor.Next(1, 7);
            switch (rnd)
            {
                case 1:
                    titelColor = ConsoleColor.DarkBlue;
                    break;
                case 2:
                    titelColor = ConsoleColor.DarkCyan;
                    break;
                case 3:
                    titelColor = ConsoleColor.DarkGreen;
                    break;
                case 4:
                    titelColor = ConsoleColor.DarkMagenta;
                    break;
                case 5:
                    titelColor = ConsoleColor.DarkRed;
                    break;
                case 6:
                    titelColor = ConsoleColor.DarkYellow;
                    break;
            }
            Console.ForegroundColor = titelColor;
            Console.SetCursorPosition(_x, _y);
            Console.Write(_gameText);
            SetColorsToDefalt(); 
            return titelColor;
        }

        public static void PrintWithColor(string _toPrint, ConsoleColor _color)
        {
            ConsoleColor currentTextColor = Console.ForegroundColor;
            Console.ForegroundColor = _color;
            Console.Write(_toPrint);
            Console.ForegroundColor = currentTextColor;
        }

        public static void PrintBackground(ConsoleColor _color)
        {
            Console.ForegroundColor = _color;

            if (!onLobby)
            {
                //top
                for (int i = 1; i < 3; i++)
                {
                    for (int x = 0; x < windowLength - 6; x += 4)
                    {
                        Console.SetCursorPosition(x + i * 2, i);
                        Console.Write("██");
                    }
                }
                //left
                for (int i = 1; i < 3; i++)
                {
                    for (int y = 0; y < windowHight - 4; y += 2)
                    {
                        Console.SetCursorPosition(i * 2, y + i);
                        Console.Write("██");
                    }
                }
                //right
                for (int i = 1; i < 3; i++)
                {
                    for (int y = 0; y < windowHight - 3; y += 2)
                    {
                        Console.SetCursorPosition(windowLength - 2 - i * 2, y + i);
                        Console.Write("██");
                    }
                }
                //bottom
                for (int i = 1; i < 3; i++)
                {
                    for (int x = 2; x < windowLength - 6; x += 4)
                    {
                        Console.SetCursorPosition(x + i * 2 - 2, windowHight - 1 - i);
                        Console.Write("██");
                    }
                }
                Console.SetCursorPosition(windowLength - 4, windowHight - 2);
                Console.Write("██");
            }
            else
            {
                //top
                for (int x = 0; x < windowLength; x += 4)
                {
                    Console.SetCursorPosition(x, 0);
                    Console.Write("▀▀");
                }
                //bottom
                for (int x = 0; x < windowLength; x += 4)
                {
                    Console.SetCursorPosition(x, windowHight - 1);
                    Console.Write("▄▄");
                } 
                //left
                for (int y = 0; y < windowHight; y += 2)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write("█");
                }
                //right
                for (int y = 0; y < windowHight; y += 2)
                {
                    Console.SetCursorPosition(windowLength - 1, y);
                    Console.Write("█");
                }
            }

            SetColorsToDefalt();
        }

        public static void SetColorsToDefalt()
        {
            Console.ForegroundColor = defaultFColor;
            Console.BackgroundColor = defaultBColor;
        }
    }
}
