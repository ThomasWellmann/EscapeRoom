

namespace Escape_Room
{
    internal class Lobby
    {
        // Nummern an Kommentare nach Console.Write() is die Anzahl von Charakteren 
        #region Variabeln
        static bool onLobby = true;
        public static int WindowLength;
        public static int WindowHight;
        public static int TopBorder = 4;
        public static int TopBorderToRoom = 11;
        public static int SideBorder = 8;
        public static int SideBorderToRoom = WindowLength / 2 - Room.roomLength / 2;
        public static int Gamemode;
        static ConsoleColor titelColor = ConsoleColor.Black;
        static ConsoleColor defaultBColor = ConsoleColor.White;
        static ConsoleColor defaultFColor = ConsoleColor.Black;
        public static ConsoleColor InputColor = ConsoleColor.DarkGreen;
        public static ConsoleColor SizeColor = ConsoleColor.DarkYellow;
        public static string TextBorder = @"
        ";
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
        #endregion

        static void Main(string[] args)
        {
            PrintLobby();
        }

        public static void PrintLobby() // Diese Seite
        {
            ResizeWindow(90, 29);
            SetColorsToDefault();
            Console.Clear();
            onLobby = true;
            Console.CursorVisible = true;
            PrintGameText(gameTitel, 0, 1);
            PrintBackground(ConsoleColor.Gray);

            Console.SetCursorPosition(WindowLength / 2 - 14, 20);
            Console.Write("Press any "); //10
            PrintWithColor("key", InputColor); //3
            Console.Write(" to get started."); //16

            Console.ReadKey(false);

            GetGamemode(0);

            InitializeGamemode();
        }

        public static void InitializeGamemode() // Nächste Seite
        {
            if (onLobby)
                ColorChangeAnimation(gameTitel, 0, 1);

            onLobby = false;

            Room.ResizeRoom(22, 12);
            Console.CursorVisible = false;

            if (Gamemode == 0)
            {

                ResizeWindow(90, 33);
                DefaultGm.PrintDefaultGmPage();
            }
            else if (Gamemode == 1)
            {
                ResizeWindow(90, 37);
                SandboxGm.PrintSandboxGmPage();
            }
            
        }

        public static void ResizeWindow(int _length, int _height)
        {
            WindowLength = _length; 
            WindowHight = _height;
            Console.SetWindowSize(WindowLength, WindowHight);
        }

        static void GetGamemode(int _gm) // Input an Stertseite für Spielmodus
        {
            PrintGamemode(_gm);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.RightArrow && _gm == 0)
                {
                    PrintGamemode(1);
                    _gm = 1;
                    Gamemode = 1;
                    continue;
                }
                else if (key.Key == ConsoleKey.LeftArrow && _gm == 1)
                {
                    PrintGamemode(0);
                    _gm = 0;
                    Gamemode = 0;
                    continue;
                }
                else if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }

        static void PrintGamemode(int _gm)
        {
            ConsoleColor gm0BColor;
            ConsoleColor gm0FColor;
            ConsoleColor gm1BColor;
            ConsoleColor gm1FColor;
            if (_gm == 0)
            {
                Gamemode = 0;
                gm0BColor = ConsoleColor.DarkGray;
                gm0FColor = ConsoleColor.White;
                gm1BColor = ConsoleColor.Gray;
                gm1FColor = ConsoleColor.Black;
            }
            else
            {
                Gamemode = 1;
                gm0BColor = ConsoleColor.Gray;
                gm0FColor = ConsoleColor.Black;
                gm1BColor = ConsoleColor.DarkGray;
                gm1FColor = ConsoleColor.White;
            }

            Console.BackgroundColor = gm0BColor;
            Console.ForegroundColor = gm0FColor;
            for (int y = 0; y < 3; y++)
            {
                Console.SetCursorPosition(WindowLength / 4, 22 + y);
                Console.Write("           "); //11
            }
            Console.SetCursorPosition(WindowLength / 4 + 2, 23);
            Console.Write("Default"); //7


            Console.BackgroundColor = gm1BColor;
            Console.ForegroundColor = gm1FColor;
            for (int y = 0; y < 3; y++)
            {
                Console.SetCursorPosition(WindowLength / 4 * 3 - 9, 22 + y);
                Console.Write("           "); //11
            }
            Console.SetCursorPosition(WindowLength / 4 * 3 - 7, 23);
            Console.Write("Sandbox"); //7

            SetColorsToDefault();
            Console.SetCursorPosition(WindowLength / 2 - 24, 20);
            Console.Write("Use "); //4
            PrintWithColor("RightArrow", InputColor); //10
            Console.Write(" or "); //4
            PrintWithColor("LeftArrow", InputColor); //9
            Console.Write(" to switch gamemode."); //20
        }

        #region Extras
        public static void ColorChangeAnimation(string _gameText, int _x, int _y) // Background + Texte Farbeänderung
        {
            Console.CursorVisible = false;
            for (int p = 0; p < 2; p++)
            {
                for (int i = 0; i < 4; i++)
                {
                    PrintGameText(_gameText, _x, _y);
                    PrintBackground(titelColor);
                    Console.Beep();
                    if (onLobby)
                    {
                        Console.SetCursorPosition(WindowLength / 2, 22);
                        Console.Write(loading[i]);
                    }
                    Thread.Sleep(250);
                }
            }
        }

        static int rnd;
        static int currentRnd;
        static void PrintGameText(string _gameText, int _x, int _y) // Größe Spieltexte mit Farben
        {
            Random RndColor = new Random();

            while (true)
            {
                rnd = RndColor.Next(1, 7);
                if (rnd != currentRnd)
                    break;
                else
                    continue;
            }

            currentRnd = rnd;

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
            SetColorsToDefault(); 
        }

        public static void PrintWithColor(string _toPrint, ConsoleColor _color)
        {
            ConsoleColor currentTextColor = Console.ForegroundColor;
            Console.ForegroundColor = _color;
            Console.Write(_toPrint);
            Console.ForegroundColor = currentTextColor;
        }

        public static void PrintBackground(ConsoleColor _color) // Graue Fläche rund um das Fenster
        {
            Console.ForegroundColor = _color;

            if (!onLobby)
            {
                //top
                for (int i = 1; i < 3; i++)
                {
                    for (int x = 0; x < WindowLength - 6; x += 4)
                    {
                        Console.SetCursorPosition(x + i * 2, i);
                        Console.Write("██");
                    }
                }
                //left
                for (int i = 1; i < 3; i++)
                {
                    for (int y = 0; y < WindowHight - 4; y += 2)
                    {
                        Console.SetCursorPosition(i * 2, y + i);
                        Console.Write("██");
                    }
                }
                //right
                for (int i = 1; i < 3; i++)
                {
                    for (int y = 0; y < WindowHight - 3; y += 2)
                    {
                        Console.SetCursorPosition(WindowLength - 2 - i * 2, y + i);
                        Console.Write("██");
                    }
                }
                //bottom
                for (int i = 1; i < 3; i++)
                {
                    for (int x = 2; x < WindowLength - 6; x += 4)
                    {
                        Console.SetCursorPosition(x + i * 2 - 2, WindowHight - 1 - i);
                        Console.Write("██");
                    }
                }
                Console.SetCursorPosition(WindowLength - 4, WindowHight - 2);
                Console.Write("██");
            }
            else
            {
                //top
                for (int x = 0; x < WindowLength; x += 4)
                {
                    Console.SetCursorPosition(x, 0);
                    Console.Write("▀▀");
                }
                //bottom
                for (int x = 0; x < WindowLength; x += 4)
                {
                    Console.SetCursorPosition(x, WindowHight - 1);
                    Console.Write("▄▄");
                } 
                //left
                for (int y = 0; y < WindowHight; y += 2)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write("█");
                }
                //right
                for (int y = 0; y < WindowHight; y += 2)
                {
                    Console.SetCursorPosition(WindowLength - 1, y);
                    Console.Write("█");
                }
            }

            SetColorsToDefault();
        }

        public static void SetColorsToDefault()
        {
            Console.ForegroundColor = defaultFColor;
            Console.BackgroundColor = defaultBColor;
        }
    }
    #endregion
}//End version check! ||
