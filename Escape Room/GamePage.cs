using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class GamePage
    {
        static char keyChar = '?';
        static string player = "██";
        public static int xPlayer;
        public static int yPlayer;
        static int xKey;
        static int yKey;
        public static int doorSide;
        public static int xDoor;
        public static int yDoor;
        static bool doorPrinted;
        static bool gotOut = false;
        static bool gotKey = false;
        static bool hasStarted = false;
        static bool hasEnded = false;
        static bool playerHasKey = false;
        public static ConsoleColor playerColor;
        public static ConsoleColor keyColor = ConsoleColor.DarkMagenta;
        static string youWon = @"
                                                     
                                                    
        __     __          __          __         _ 
        \ \   / /          \ \        / /        | |
         \ \_/ /__  _   _   \ \  /\  / /__  _ __ | |
          \   / _ \| | | |   \ \/  \/ / _ \| '_ \| |
           | | (_) | |_| |    \  /\  / (_) | | | |_|
           |_|\___/ \__,_|     \/  \/ \___/|_| |_(_)";
        static string escape = @"
         ______                           _ 
        |  ____|                         | |
        | |__   ___  ___ __ _ _ __   ___ | |
        |  __| / __|/ __/ _` | '_ \ / _ \| |
        | |____\__ \ (_| (_| | |_) |  __/|_|
        |______|___/\___\__,_| .__/ \___|(_)
                             | |                                         
                             |_|";

        public static void StartGame()
        {
            Console.SetWindowSize(Lobby.windowLength, Lobby.windowHight);
            Lobby.SetColorsToDefalt();
            Console.Clear();
            playerHasKey = false;
            doorPrinted = false;
            gotKey = false;
            playerColor = ConsoleColor.Blue;
            keyChar = '?';

            Random RndNum = new Random();
            xPlayer = RndNum.Next(1, (Room.roomLength - 2) / 2 + 1) * 2 - 1; //Rechnung, damit player nur auf eine ungerade Stelle kommt
            yPlayer = RndNum.Next(1, Room.roomHeight - 1);
            
            xKey = RndNum.Next(1, Room.roomLength - 1);
            yKey = RndNum.Next(1, Room.roomHeight - 1);

            doorSide = RndNum.Next(1, 5);
            if (doorSide == 1)
            {
                xDoor = RndNum.Next(3, (Room.roomLength - 6) / 2 + 1) * 2 - 1;
                yDoor = 0;
            }
            else if (doorSide == 2)
            {
                xDoor = Room.roomLength - 1;
                yDoor = RndNum.Next(3, Room.roomHeight - 4);
            }
            else if (doorSide == 3)
            {
                xDoor = RndNum.Next(3, (Room.roomLength - 6) / 2 + 1) * 2 - 1 ;
                yDoor = Room.roomHeight - 1;
            }
            else if (doorSide == 4)
            {
                xDoor = 0;
                yDoor = RndNum.Next(3, Room.roomHeight - 4);
            }

            while (true)
            {
                xKey = RndNum.Next(1, Room.roomLength - 1);
                yKey = RndNum.Next(1, Room.roomHeight - 1);

                if (((xKey == xPlayer || xKey == xPlayer + 1) && yKey == yPlayer) ||
                    (xKey == Room.roomLength - 2 && (yKey == Room.roomHeight / 2 - 1 || yKey == Room.roomHeight / 2)))
                    continue;
                else
                    break;
            }

            Room.PrintRoom();
            PrintGamePage();
            PrintGameElements();
            GetGameGoing();
        }

        public static void GetGameGoing()
        {
            MovePlayer();
            DoPlayerHaveKey();
            IsGameOver();
        }

        static void PrintGamePage()
        {
            Console.SetCursorPosition(Lobby.sideBorder, Lobby.topBorder);
            Console.WriteLine("The game has started!");
            Console.Write($"{Lobby.textBorder}Use ");
            Lobby.PrintWithColor("UpArrow", Lobby.imputColor);
            Console.Write(", ");
            Lobby.PrintWithColor("DownArrow", Lobby.imputColor);
            Console.Write(", ");
            Lobby.PrintWithColor("RightArrow", Lobby.imputColor);
            Console.Write(" or ");
            Lobby.PrintWithColor("LeftArrow", Lobby.imputColor);
            Console.Write($" to move arround.{Lobby.textBorder}Your Objective is to get the ");
            Lobby.PrintWithColor("Key", keyColor);
            Console.Write(" and exit the room through the door.");
            Console.Write($"{Lobby.textBorder}The door will open once you get the ");
            Lobby.PrintWithColor("Key", keyColor);
            Console.Write(".");

            Console.SetCursorPosition(Lobby.windowLength - 32, Lobby.windowHight - 14);
            Console.Write("Press ");//6
            Lobby.PrintWithColor("Escape", Lobby.imputColor);//6
            Console.Write(" to go back.");//12

            Console.SetCursorPosition(0, Lobby.windowHight - 13);
            Console.Write(escape);
        }

        static void MovePlayer()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            Console.BackgroundColor = ConsoleColor.Gray;
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (yPlayer > 1)
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
                        Console.Write("  ");
                        yPlayer -= 1;
                    } 
                    else if (playerHasKey && doorSide == 1 && (xPlayer == xDoor || xPlayer == xDoor + 2))
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
                        Console.Write("  ");
                        yPlayer -= 2;
                        hasEnded = true;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (yPlayer < Room.roomHeight - 2)
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
                        Console.Write("  ");
                        yPlayer += 1;
                    }
                    else if (playerHasKey && doorSide == 3 && (xPlayer == xDoor || xPlayer == xDoor + 2))
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
                        Console.Write("  ");
                        yPlayer += 2;
                        hasEnded = true;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (xPlayer > 2)
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
                        Console.Write("  ");
                        xPlayer -= 2;
                    }
                    else if (playerHasKey && doorSide == 4 && (yPlayer == yDoor || yPlayer == yDoor + 1))
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
                        Console.Write("  ");
                        xPlayer -= 4;
                        hasEnded = true;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (xPlayer < Room.roomLength - 3)
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
                        Console.Write("  ");
                        xPlayer += 2;
                    }
                    else if (playerHasKey && doorSide == 2 && (yPlayer == yDoor || yPlayer == yDoor + 1))
                    {
                        Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
                        Console.Write("  ");
                        xPlayer += 4;
                        hasEnded = true;
                    }
                    break;

                case ConsoleKey.Escape:
                    GoBack();
                    break;
            }
            Lobby.SetColorsToDefalt();

            PrintGameElements();
        }

        static void PrintGameElements()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = keyColor;
            Console.SetCursorPosition(xKey + Lobby.sideBorderToRoom, yKey + Lobby.topBorderToRoom);
            Console.Write(keyChar);
            Console.ForegroundColor = playerColor;
            Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
            Console.Write(player);
            Lobby.SetColorsToDefalt();

            Console.SetCursorPosition(Lobby.sideBorder, Lobby.windowHight - 14);
            Console.Write($"Player position:");
            Console.SetCursorPosition(Lobby.sideBorder, Lobby.windowHight - 13);
            Console.Write($"X: {xPlayer / 2 + 1} | Y: {Room.roomHeight - yPlayer - 1}           ");

            Lobby.PrintBackground(ConsoleColor.Gray);
        }

        static void DoPlayerHaveKey()
        {
            if ((xPlayer == xKey || xPlayer == xKey - 1) && yPlayer == yKey && !gotKey)
            {
                playerHasKey = true;
                playerColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = playerColor; 
                Console.SetCursorPosition(xPlayer + Lobby.sideBorderToRoom, yPlayer + Lobby.topBorderToRoom);
                Console.Write(player);
                keyChar = ' ';

                Console.ForegroundColor = ConsoleColor.Black;
                Console.Beep();
                Console.SetCursorPosition(Lobby.windowLength / 2 - 8, Lobby.windowHight - 14);
                Console.Write("You got the ");//12
                Lobby.PrintWithColor("Key", keyColor);//3
                Console.Write("!");//1

                if (!doorPrinted)
                {
                    Console.SetCursorPosition(xDoor + Lobby.sideBorderToRoom, yDoor + Lobby.topBorderToRoom);
                    if (doorSide == 1 || doorSide == 3)
                    {
                        Console.Write("    ");
                    } 
                    else if (doorSide == 2 || doorSide == 4)
                    {
                        Console.Write(' ');
                        Console.SetCursorPosition(xDoor + Lobby.sideBorderToRoom, yDoor + Lobby.topBorderToRoom + 1);
                        Console.Write(' ');
                    }
                    doorPrinted = true;
                }
                gotKey = true;
            }

        }

        static void GoBack()
        {
            Lobby.SetColorsToDefalt();

            hasEnded = false;
            playerHasKey = false;
            hasStarted = false;
            Room.roomLength = 22;
            Room.roomHeight = 12;
            Room.InitializeRoom();

            if (Lobby.gamemode == 0)
            {
                DefaultGm.difficulty = 0;
                Lobby.windowHight = 33;
                DefaultGm.PrintDefaultGmPage();
            }

            else if (Lobby.gamemode == 1)
            {
                Lobby.windowHight = 37;
                SandboxGm.PrintSandboxGmPage();
            }
        }

        static void IsGameOver()
        {
            if (hasEnded)
            {
                Lobby.ColorChangeAnimation(youWon, 0, Lobby.windowHight - 13);

                while (true)
                {
                    Lobby.PrintBackground(ConsoleColor.Gray);

                    ConsoleKeyInfo keyImput = Console.ReadKey(true);
                    if (keyImput.Key == ConsoleKey.Escape)
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
