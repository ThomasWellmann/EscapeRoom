using System;


namespace Escape_Room
{
    internal class GamePage
    {
        #region Variabeln
        static char keyChar = '?';
        static string player = "██";
        static int xPlayer;
        static int yPlayer;
        static int xKey;
        static int yKey;
        static int doorSide;
        static int xDoor;
        static int yDoor;
        static bool doorPrinted;
        static bool gotKey = false;
        static bool hasEnded = false;
        public static ConsoleColor PlayerColor;
        public static ConsoleColor KeyColor = ConsoleColor.DarkMagenta;
        static string youWon = @"
                                                     
                                                    
        __     __          __          __         _ 
        \ \   / /          \ \        / /        | |
         \ \_/ /__  _   _   \ \  /\  / /__  _ __ | |
          \   / _ \| | | |   \ \/  \/ / _ \| '_ \| |
           | | (_) | |_| |    \  /\  / (_) | | | |_|
           |_|\___/ \__,_|     \/  \/ \___/|_| |_(_)";// Extra Raum oben um Texte zu ersetzen
        static string escape = @"
         ______                           _ 
        |  ____|                         | |
        | |__   ___  ___ __ _ _ __   ___ | |
        |  __| / __|/ __/ _` | '_ \ / _ \| |
        | |____\__ \ (_| (_| | |_) |  __/|_|
        |______|___/\___\__,_| .__/ \___|(_)
                             | |                                         
                             |_|";
        #endregion

        public static void StartGame() // Diese Seite
        {
            Lobby.SetColorsToDefault();
            Console.Clear();
            gotKey = false;
            doorPrinted = false;
            gotKey = false;
            PlayerColor = ConsoleColor.Blue;
            keyChar = '?';

            Random RndNum = new Random();
            // Zufällige Spieler Koordinaten
            xPlayer = RndNum.Next(1, (Room.roomLength - 2) / 2 + 1) * 2 - 1; // Rechnung, damit der Spieler nur auf eine ungerade Stelle kommt.
            yPlayer = RndNum.Next(1, Room.roomHeight - 1);
            // Zufällige Schlüssel Koordinaten
            do
            {
                xKey = RndNum.Next(1, Room.roomLength - 1);
                yKey = RndNum.Next(1, Room.roomHeight - 1);
            } while (xKey == xPlayer && yKey == yPlayer);// Darf nicht gleich wie der Spieler sein

            doorSide = RndNum.Next(1, 5);
            if (doorSide == 1)//top
            {
                xDoor = RndNum.Next(3, (Room.roomLength - 6) / 2 + 1) * 2 - 1;
                yDoor = 0;
            }
            else if (doorSide == 2)//right
            {
                xDoor = Room.roomLength - 1;
                yDoor = RndNum.Next(3, Room.roomHeight - 4);
            }
            else if (doorSide == 3)//bottom
            {
                xDoor = RndNum.Next(3, (Room.roomLength - 6) / 2 + 1) * 2 - 1 ;
                yDoor = Room.roomHeight - 1;
            }
            else if (doorSide == 4)//left
            {
                xDoor = 0;
                yDoor = RndNum.Next(3, Room.roomHeight - 4);
            }

            Lobby.PrintBackground(ConsoleColor.Gray);
            Room.PrintRoom();
            PrintGamePage();
            PrintGameElements();
            GetGameGoing();
        }

        static void GetGameGoing() // Spiel läuft
        {
            MovePlayer();
            DoPlayerHaveKey();
            IsGameOver();
        }

        static void PrintGamePage() // Print Spielseite Texte
        {
            Console.SetCursorPosition(Lobby.SideBorder, Lobby.TopBorder);
            Console.WriteLine("The game has started!");
            Console.Write($"{Lobby.TextBorder}Use ");
            Lobby.PrintWithColor("UpArrow", Lobby.InputColor);
            Console.Write(", ");
            Lobby.PrintWithColor("DownArrow", Lobby.InputColor);
            Console.Write(", ");
            Lobby.PrintWithColor("RightArrow", Lobby.InputColor);
            Console.Write(" or ");
            Lobby.PrintWithColor("LeftArrow", Lobby.InputColor);
            Console.Write($" to move around.{Lobby.TextBorder}Your Objective is to get the ");
            Lobby.PrintWithColor("Key", KeyColor);
            Console.Write(" and exit the room through the door.");
            Console.Write($"{Lobby.TextBorder}The door will open once you get the ");
            Lobby.PrintWithColor("Key", KeyColor);
            Console.Write(".");

            Console.SetCursorPosition(Lobby.WindowLength - 29, Lobby.WindowHight - 14);
            Console.Write("Press ");//6
            Lobby.PrintWithColor("ESC", Lobby.InputColor);//3
            Console.Write(" to go back.");//12

            Console.SetCursorPosition(0, Lobby.WindowHight - 13);
            Console.Write(escape);
        }

        static void MovePlayer() // Spielerbewegung
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            Console.BackgroundColor = ConsoleColor.Gray;
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (yPlayer > 1)
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
                        Console.Write("  ");
                        yPlayer -= 1;
                    } 
                    else if (gotKey && doorSide == 1 && (xPlayer == xDoor || xPlayer == xDoor + 2))
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
                        Console.Write("  ");
                        yPlayer -= 2;
                        hasEnded = true;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (yPlayer < Room.roomHeight - 2)
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
                        Console.Write("  ");
                        yPlayer += 1;
                    }
                    else if (gotKey && doorSide == 3 && (xPlayer == xDoor || xPlayer == xDoor + 2))
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
                        Console.Write("  ");
                        yPlayer += 2;
                        hasEnded = true;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (xPlayer > 2)
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
                        Console.Write("  ");
                        xPlayer -= 2;
                    }
                    else if (gotKey && doorSide == 4 && (yPlayer == yDoor || yPlayer == yDoor + 1))
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
                        Console.Write("  ");
                        xPlayer -= 4;
                        hasEnded = true;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (xPlayer < Room.roomLength - 3)
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
                        Console.Write("  ");
                        xPlayer += 2;
                    }
                    else if (gotKey && doorSide == 2 && (yPlayer == yDoor || yPlayer == yDoor + 1))
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
                        Console.Write("  ");
                        xPlayer += 4;
                        hasEnded = true;
                    }
                    break;

                case ConsoleKey.Escape:
                    GoBack();
                    break;
            }
            Lobby.SetColorsToDefault();
            PrintGameElements();
        }

        static void PrintGameElements() //Print Spieler und Schlüssel
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = KeyColor;
            Console.SetCursorPosition(xKey + Lobby.SideBorderToRoom, yKey + Lobby.TopBorderToRoom);
            Console.Write(keyChar);
            Console.ForegroundColor = PlayerColor;
            Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
            Console.Write(player);
            Lobby.SetColorsToDefault();

            Console.SetCursorPosition(Lobby.SideBorder, Lobby.WindowHight - 14);
            Console.Write($"Player position:");
            Console.SetCursorPosition(Lobby.SideBorder, Lobby.WindowHight - 13);
            Console.Write($"X: {xPlayer / 2 + 1} | Y: {Room.roomHeight - yPlayer - 1}           ");

            Lobby.PrintBackground(ConsoleColor.Gray);
        }

        static void DoPlayerHaveKey() 
        {
            if ((xPlayer == xKey || xPlayer == xKey - 1) && yPlayer == yKey && !gotKey)
            {
                gotKey = true;
                PlayerColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = PlayerColor; 
                Console.SetCursorPosition(xPlayer + Lobby.SideBorderToRoom, yPlayer + Lobby.TopBorderToRoom);
                Console.Write(player);
                keyChar = ' ';

                Console.ForegroundColor = ConsoleColor.Black;
                Console.Beep();
                Console.SetCursorPosition(Lobby.WindowLength / 2 - 8, Lobby.WindowHight - 14);
                Console.Write("You got the ");//12
                Lobby.PrintWithColor("Key", KeyColor);//3
                Console.Write("!");//1

                if (!doorPrinted)
                {
                    Console.SetCursorPosition(xDoor + Lobby.SideBorderToRoom, yDoor + Lobby.TopBorderToRoom);
                    if (doorSide == 1 || doorSide == 3)
                    {
                        Console.Write("    ");
                    } 
                    else if (doorSide == 2 || doorSide == 4)
                    {
                        Console.Write(' ');
                        Console.SetCursorPosition(xDoor + Lobby.SideBorderToRoom, yDoor + Lobby.TopBorderToRoom + 1);
                        Console.Write(' ');
                    }
                    doorPrinted = true;
                }
            }

        }

        static void GoBack() // Beendet Spiel, letzte Seite
        {
            hasEnded = false;
            gotKey = false;
            Room.ResizeRoom(22, 12);

            Lobby.InitializeGamemode();
        }

        static void IsGameOver()
        {
            if (hasEnded)
            {
                Lobby.ColorChangeAnimation(youWon, 0, Lobby.WindowHight - 13);

                while (true)
                {
                    Lobby.PrintBackground(ConsoleColor.Gray);

                    ConsoleKeyInfo keyInput = Console.ReadKey(true);
                    if (keyInput.Key == ConsoleKey.Escape)
                    {
                        GoBack();
                    }
                    else
                        continue;
                }
            }
            else
                GetGameGoing();
        }
    }
}
