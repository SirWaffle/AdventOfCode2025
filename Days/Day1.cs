using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Days
{
    public class Day1: Day_base
    {
        public Day1() { }

        public class Dial
        {
            public int count { get; set; } = 0;

            int val = 50;            
            int exclusive_max = 100;
            bool countClicks = false;

            public Dial(int val, int exclusive_max, bool countClicks)
            {
                this.val = val;
                this.exclusive_max = exclusive_max;
                this.countClicks = countClicks;
            }

            public void Turn(int turn)
            {
                val += turn;

                bool clicked = val < 0 || val >= 100;
                while (val < 0)
                {
                    val += exclusive_max;
                    if(countClicks)
                        count++;
                }

                while (val >= exclusive_max)
                {
                    val -= exclusive_max;
                    if (countClicks)
                        count++;
                }

                if(false)//!clicked)
                    if (val == 0)
                        count++;


            }
        };

        public override void Solve_P1()
        {
            Console.WriteLine(File.ReadAllText(@"Days\" + this.GetType().Name + "_example.txt").Split(Environment.NewLine).Select(x => x.Trim().ToLower()).Select(x => int.Parse(x.Substring(1)) * (x.ToLower()[0] == 'l' ? -1 : 1)).Select(x => (0, 0, x)).Prepend((50, 0, 0)).Aggregate((accum, item) => ((accum.Item1 + item.Item3 ) % 100, accum.Item2 + (((accum.Item1 + item.Item3) % 100) == 0 ? 1 : 0), 0)).Item2);
        }

        public override void Solve_P2()
        {

            List<string> input = ReadWholeInput().Split(Environment.NewLine).Select(x => x.Trim().ToLower()).ToList();

            Dial dial = new Dial(50, 100, true);
            input.ForEach(x => dial.Turn(int.Parse(x.Substring(1)) * (x.ToLower()[0] == 'l' ? -1 : 1)));

            Console.WriteLine(dial.count); // too high 5949

            /*
            Console.WriteLine(
                File.ReadAllText(@"Days\" + this.GetType().Name + "_input.txt")
                    .Split(Environment.NewLine)
                    .Select(x => x.Trim().ToLower())
                    .Select(x => int.Parse(x.Substring(1)) * (x.ToLower()[0] == 'l' ? -1 : 1))
                    .Select(x => (0, 0, x))
                    .Prepend((50, 0, 0))
                    .Aggregate((accum, item) =>(
                                                   (accum.Item1 + item.Item3) % 100
                                                  , accum.Item2 + (int)Math.Truncate(Math.Abs((float)(accum.Item1 + item.Item3) / 100.0f))
                                                    + ((accum.Item1 + item.Item3 != 0 && Math.Sign((decimal)accum.Item1) != Math.Sign((accum.Item1 + item.Item3)))? 1: 0)
                                                    + ((accum.Item1 + item.Item3 == 0 && !(Math.Sign((decimal)accum.Item1) != Math.Sign((accum.Item1 + item.Item3)))) ? 1 : 0)
                                                  , 0
                                               )
                ).Item2
            );*/
        }
    }
}
