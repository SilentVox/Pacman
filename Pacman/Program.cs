using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace Pacman
{
    internal class Program
    {

        static void Main(string[] args)
        {
            
            char[,] map = MapReading("map.txt");
            
            int pacmanX = 1;
            int pacmanY = 1;
            int ScoreCount = 0;
            int coin = 0;

            Console.CursorVisible = false;

            while (true)
            {

                Console.Clear();
                
                MapDrowing(map);
                
                if (coin < 3)
                { 
                    GetRandomPoint(map, ref coin); 
                }
                
                Console.SetCursorPosition(33, 2);
                Console.Write(coin);

                Console.SetCursorPosition(33, 1);
                Console.Write("Score:"+ScoreCount);
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("@");
                ConsoleKeyInfo charKey = Console.ReadKey();
                
                


                Pacman(charKey, ref pacmanX, ref pacmanY, map, ref coin, ref ScoreCount);
            }
        }
        private static char[,] MapReading(string path)
        {
            string[] file = File.ReadAllLines("map.txt");
            char[,] map = new char[GetMaxLengthofLine(file), file.Length];
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];
            return map;
        }

        private static void MapDrowing(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(map[x, y]);

                    }
                    if (map[x, y] != 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(map[x, y]);
                    }
                }
                Console.WriteLine();
            }
        }

        private static int GetMaxLengthofLine(string[] lines)
        {
            int maxLength = lines[0].Length;
            foreach (string line in lines)
                if (line.Length > maxLength)
                    maxLength = line.Length;
            return maxLength;


        }

        private static int GetRandomPoint(char[,] map, ref int coin)
        {

            int LocalX;
            int LocalY;
            
            Random random = new Random();
            while (coin < 3)
            {


                LocalX = random.Next(1, map.GetLength(0));
                LocalY = random.Next(1, map.GetLength(1));
                //randompoint = [LocalX, LocalY];
                Console.SetCursorPosition(LocalX, LocalY);
                if (map[LocalX, LocalY] != '#')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(map[LocalX, LocalY] = 'O');
                    Console.ForegroundColor = ConsoleColor.White;
                    coin++;
                }

            }
            return coin;
        }

        

        private static int Pacman(ConsoleKeyInfo charKey, ref int pacmanX, ref int pacmanY, char[,]map, ref int coin, ref int Scorecount)
        {
            switch (charKey.Key)
            {
            
                case ConsoleKey.UpArrow:
                    if (map[pacmanX, pacmanY - 1] != '#')
                    {
                        pacmanY--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (map[pacmanX, pacmanY + 1] != '#')
                    {
                        pacmanY++;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (map[pacmanX + 1, pacmanY] != '#')
                    {
                        pacmanX++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (map[pacmanX - 1, pacmanY] != '#')
                    {
                        pacmanX--;
                    }
                    break;
            
            }
            if (map[pacmanX, pacmanY] == 'O')
            {
                map[pacmanX, pacmanY] = ' ';
                coin--;
                Scorecount += 100;
            }
            return coin;
        }


    }
}
