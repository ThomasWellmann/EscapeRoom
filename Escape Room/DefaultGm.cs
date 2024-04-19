using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class DefaultGm // Gamemode 0
    {
        public static int difficulty = 0;

        public static void PrintDefaultGmPage() // Diese Seite
        {
            Lobby.SetColorsToDefault();
            Console.Clear();

            Lobby.PrintBackground(ConsoleColor.Gray);
            Console.SetCursorPosition(Lobby.sideBorder, Lobby.topBorder);
            Console.Write($"Difficulty Level: {difficulty + 1}. The room is now ");
            Lobby.PrintWithColor($"{Room.roomLength / 2 - 1}", Lobby.sizeColor);
            Console.Write('x');
            Lobby.PrintWithColor($"{Room.roomHeight - 2}", Lobby.sizeColor);
            Console.Write(":\n");

            if (difficulty == 0)
            {
                Console.Write($"{Lobby.textBorder}You are now at the least difficult level.\n" +
                    $"{Lobby.textBorder}Press ");
                Lobby.PrintWithColor("UpArrow", Lobby.inputColor);
                Console.Write($" to increase the Room's size, and with it the difficulty.\n\n");
            }
            else if (difficulty > 0 && difficulty < 2)
            {
                Console.Write($"{Lobby.textBorder}Press ");
                Lobby.PrintWithColor("UpArrow", Lobby.inputColor);
                Console.Write($" to increase the Room's size, and with it the difficulty\n" +
                    $"{Lobby.textBorder}Press ");
                Lobby.PrintWithColor($"DownArrow", Lobby.inputColor);
                Console.Write($" to do the opposite .\n\n");
            }
            else if (difficulty == 2)
            {
                Console.Write($"{Lobby.textBorder}You are now at the most difficult level.\n" +
                    $"{Lobby.textBorder}Press ");
                Lobby.PrintWithColor("DownArrow", Lobby.inputColor);
                Console.Write(" to decrease the Room's size, and with it the difficulty.\n\n");
            }

            Room.PrintRoom();

            Console.SetCursorPosition(Lobby.sideBorder, Lobby.topBorderToRoom + Room.roomHeight + 2);
            Console.Write("When ready, press ");
            Lobby.PrintWithColor("SpaceBar", Lobby.inputColor);
            Console.Write(" to start.\n");
            Console.Write($"{Lobby.textBorder}Press ");
            Lobby.PrintWithColor("Escape", Lobby.inputColor);
            Console.Write(" to go back.");
            Lobby.PrintBackground(ConsoleColor.Gray);

            GetInputInfo();

            Lobby.ResizeWindow(90, Lobby.windowHight + 6);
            Console.Beep();
            GamePage.StartGame(); // Nächste Seite
        }
        static void GetInputInfo() // Input, um Raumgröße zu ändern
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow && Room.roomHeight < 30)
                {
                    Room.ResizeRoom(Room.roomLength + 20, Room.roomHeight + 10);
                    Lobby.ResizeWindow(90, Lobby.windowHight + 10);
                    difficulty += 1;
                    Console.Beep();
                    PrintDefaultGmPage();
                    continue;
                }
                else if (key.Key == ConsoleKey.DownArrow && Room.roomHeight > 12)
                {
                    Room.ResizeRoom(Room.roomLength - 20, Room.roomHeight - 10);
                    Lobby.ResizeWindow(90, Lobby.windowHight - 10);
                    difficulty -= 1;
                    Console.Beep();
                    PrintDefaultGmPage();
                    continue;
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Lobby.PrintLobby();
                    return;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
