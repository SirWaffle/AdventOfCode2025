using System;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2025.Days
{
    public class Day4: Day_base
    {
        public override void Solve()
        {
            Solve_P2();
        }
        public int CountNeighbors(char[][] map, int _x, int _y)
        {
            int count = 0;
            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    if (y == 0 && x == 0)
                        continue;

                    if (y + _y < 0 || y + _y >= map.Length)
                        continue;

                    if (x + _x < 0 || x + _x >= map[y + _y].Length)
                        continue;

                    if (map[y + _y][x + _x] == '@')
                        ++count;
                }
            }

            return count;
        }

        public void Solve_P1() 
        {
            char[][] input = ReadWholeInput().Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();
            Console.WriteLine("A: " + input.Select((y, yi) => y.Select(c => c).Where((c, xi) => c != '.' && CountNeighbors(input, xi, yi) < 4)).SelectMany(l => l).ToList().Count);
        }

        public void Solve_P2()
        {
            char[][] input = ReadWholeInput().Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();

            int movable = 0;
            int curMove = 0;
            do
            {
                curMove = 0;
                for (int y = 0; y < input.Length; ++y)
                {
                    for (int x = 0; x < input[y].Length; ++x)
                    {
                        if (input[y][x] != '.' && input[y][x] != 'X')
                        {
                            if (CountNeighbors(input, x, y) < 4)
                            {
                                ++movable;
                                ++curMove;
                                input[y][x] = 'X';
                            }
                        }
                    }
                }
            } while (curMove > 0);
            Console.WriteLine("A: " + movable);
        }



    }
}
