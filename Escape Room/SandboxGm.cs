using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class SandboxGm
    {
        public static void PrintSandboxGmPage()
        {
            Console.SetWindowSize(Lobby.windowLength, Lobby.windowHight);
            Lobby.SetColorsToDefalt();
            Console.Clear();

            Console.SetCursorPosition(Lobby.sideBorder, Lobby.topBorder);
            Console.Write($"Sandbox: You can customize the room with the size of your liking.\n" +
                $"{Lobby.textBorder}The smallest room you can have is ");
            Lobby.PrintWithColor("10", Lobby.sizeColor);
            Console.Write('x');
            Lobby.PrintWithColor("10", Lobby.sizeColor);
            Console.Write(", and the bigest ");
            Lobby.PrintWithColor("30", Lobby.sizeColor);
            Console.Write('x');
            Lobby.PrintWithColor("30", Lobby.sizeColor);
            Console.Write(".\n" +
                $"{Lobby.textBorder}The room is now ");
            Lobby.PrintWithColor($"{Room.roomLength / 2 - 1}", Lobby.sizeColor);
            Console.Write('x');
            Lobby.PrintWithColor($"{Room.roomHeight - 2}", Lobby.sizeColor);
            Console.Write(":\n");

            Room.PrintRoom();

            Console.SetCursorPosition(Lobby.sideBorder, Lobby.windowHight - 12);
            Console.Write("Press ");
            Lobby.PrintWithColor("Enter", Lobby.imputColor);
            Console.Write(" to start and end the value imput.");

            Console.SetCursorPosition(Lobby.sideBorder, Lobby.windowHight - 8);
            Console.Write("Type first it's length ");
            Lobby.PrintWithColor("x", Lobby.sizeColor);
            Console.Write(", than it's height ");
            Lobby.PrintWithColor("y", Lobby.sizeColor);
            Console.Write(".\n" +
                $"{Lobby.textBorder}Example: \"10x20\" (10 is here the ");
            Lobby.PrintWithColor("x", Lobby.sizeColor);
            Console.Write(" value and 20 the ");
            Lobby.PrintWithColor("y", Lobby.sizeColor);
            Console.Write(" one).");

            Lobby.PrintBackground(ConsoleColor.Gray);

            GetImputInfo();

            Lobby.windowHight += 4;
            GamePage.StartGame();
        }

        private static void GetImputInfo()
        {
            while (true)
            {
                Console.SetCursorPosition(Lobby.sideBorder, Lobby.windowHight - 10);
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write("       ");
                Console.SetCursorPosition(Lobby.sideBorder + 3, Lobby.windowHight - 10);
                Console.Write('x');

                ConsoleKeyInfo keyImput = Console.ReadKey(true);
                if (keyImput.Key == ConsoleKey.Spacebar)
                {
                    break;
                }
                else if (keyImput.Key == ConsoleKey.Enter)
                {

                    GetRoomSizeInput();
                    PrintSandboxGmPage();
                    continue;
                }
                else if (keyImput.Key == ConsoleKey.Escape) 
                {
                    Lobby.PrintLobby();
                    return;
                }
            }
        }

        private static void GetRoomSizeInput()
        {
            string input = "";

            Console.SetCursorPosition(Lobby.sideBorder + 1, Lobby.windowHight - 10);

            while (true)
            {
                Console.CursorVisible = true;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    break;
                }
                else if (((Char.IsDigit(key.KeyChar) == true) || (input.Length == 2 && key.KeyChar == 'x')) && input.Length < 5)
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    if (input.Length == 3)
                        input = input.Remove(input.Length - 2);

                    else
                        input = input.Remove(input.Length - 1);

                    Console.SetCursorPosition(Lobby.sideBorder + 1 + input.Length, Lobby.windowHight - 10);
                    Console.Write(' ');
                    Console.SetCursorPosition(Lobby.sideBorder + 1 + input.Length, Lobby.windowHight - 10);
                }
                else if (input.Length == 5 && key.Key == ConsoleKey.Enter)
                {
                    Console.CursorVisible = false;
                    string[] filteredInput = input.Split('x');
                    if (int.TryParse(filteredInput[0], out int xRoom) && int.TryParse(filteredInput[1], out int yRoom))
                    {
                        if (xRoom > 30 || yRoom > 30 || xRoom < 10 || yRoom < 10)
                            continue;
                        else
                        {
                            Room.roomLength = xRoom * 2 + 2;
                            Room.roomHeight = yRoom + 2;
                            Room.InitializeRoom();
                            if (yRoom % 2 == 0)
                                Lobby.windowHight = yRoom + 27;
                            else
                                Lobby.windowHight = yRoom + 28;
                        }
                    }
                    break;
                }

                if (input.Length == 2 && key.Key != ConsoleKey.Backspace)
                {
                    input += 'x';
                    Console.Write('x');
                }
            }
        }
    }
}
