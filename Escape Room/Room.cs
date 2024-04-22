using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Room
{
    internal class Room
    {
        public static char[,] room;
        public static int roomLength = 22;
        public static int roomHeight = 12;
        //█▀▄ Room Wände
        public static void InitializeRoom() // Array herstellen
        {
            room = new char[roomLength, roomHeight];
            
            for (int x = 0; x < roomLength; x++) 
                room[x, 0] = '▄'; 
            
            for (int y = 1; y < roomHeight - 1; y++)
            {
                room[0, y] = '█';

                for (int x = 1; x < roomLength - 1; x++)
                    room[x, y] = ' ';

                room[roomLength - 1, y] = '█';
            }

            for (int x = 0; x < roomLength; x++)
                room[x, roomHeight - 1] = '▀';
        }

        public static void PrintRoom() // Raum Array Printen
        {
            Lobby.SetColorsToDefault();
            Lobby.SideBorderToRoom = Lobby.WindowLength / 2 - roomLength / 2;
            Console.SetCursorPosition(Lobby.SideBorderToRoom, Lobby.TopBorderToRoom);

            for (int x = 0; x < roomLength; x++)
                Console.Write(room[x, 0]);

            Console.WriteLine();
            
            for (int y = 1; y < roomHeight - 1; y++)
            {
                Console.SetCursorPosition(Lobby.SideBorderToRoom, y + Lobby.TopBorderToRoom);

                for (int x = 0; x < roomLength; x++)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(room[x, y]);
                    Lobby.SetColorsToDefault();
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(Lobby.SideBorderToRoom, roomHeight + Lobby.TopBorderToRoom - 1);

            for (int x = 0; x < roomLength; x++)
                Console.Write(room[x, roomHeight - 1 ]);
        }

        public static void ResizeRoom(int _length, int _height) // Ändere Raumgröße
        {
            roomLength = _length;
            roomHeight = _height;
            InitializeRoom();
        }
    }
}
