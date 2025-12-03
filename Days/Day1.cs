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
        public override void Solve()
        {
            Solve_P2_linq();
        }

        public void Solve_P1()
        {
            Console.WriteLine(File.ReadAllText(@"Days\" + this.GetType().Name + "_example.txt").Split(Environment.NewLine).Select(x => x.Trim().ToLower()).Select(x => int.Parse(x.Substring(1)) * (x.ToLower()[0] == 'l' ? -1 : 1)).Select(x => (0, 0, x)).Prepend((50, 0, 0)).Aggregate((accum, item) => ((accum.Item1 + item.Item3 ) % 100, accum.Item2 + (((accum.Item1 + item.Item3) % 100) == 0 ? 1 : 0), 0)).Item2);
        }

        public void Solve_P2_linq()
        {
            Console.WriteLine("answer: " + File.ReadAllText(@"Days\" + this.GetType().Name + "_example.txt")
                .Split(Environment.NewLine).Select(x => x.Trim().ToLower())
                .Select(x => ((x.ToLower()[0] == 'l' ? -1 : 1), int.Parse(x.Substring(1))))
                .Select(item => Enumerable.Range(0, item.Item2).Select(x => item.Item1).ToList())
                .Aggregate((accum, item) => accum.Concat(item).ToList())
                .Select(x => (0, x, 0)).Prepend((50, 0, 0))
                .Aggregate((accum, x) => ((accum.Item1 + x.Item2) % 100, x.Item2, (((accum.Item1 + x.Item2) % 100) == 0 ? accum.Item3 + 1 : accum.Item3))).Item3);              
        }

        public void Solve_P2() // 5941
        {
            var turns = ReadWholeInput().Split(Environment.NewLine).Select(x => x.Trim().ToLower()).Select(x => int.Parse(x.Substring(1)) * (x.ToLower()[0] == 'l' ? -1 : 1));
            turns.ToList();

            int dial = 50;
            int clicks = 0;
            foreach(var turn in turns)
            {
                for (int i = 0; i < Math.Abs(turn); ++i)
                {
                    int inc = Math.Sign(turn) * 1;

                    dial += inc;
                    if (dial == 100)
                        dial = 0;
                    if (dial == -1)
                        dial = 99;
                    if (dial == 0)
                        clicks++;
                }
            }

            Console.WriteLine("answer: " + clicks);
            /*
            Console.WriteLine( "answer: " + 
                File.ReadAllText(@"Days\" + this.GetType().Name + "_input.txt")
                    .Split(Environment.NewLine)
                    .Select(x => x.Trim().ToLower())
                    .Select(x => int.Parse(x.Substring(1)) * (x.ToLower()[0] == 'l' ? -1 : 1))
                    .Select(x => (0, 0, x, Enumerable.Repeat( x> 0? 1:-1, Math.Abs(x)).ToList()))
                    .Prepend((50,0,0,new List<int>()))
                    .Aggregate( (accum, state) => )

                    .Select( (accum, state) => Enumerable.Range(0, Math.Abs(state.Item3))
                                              .Select(x => (state.Item1
                    .Item2
            );*/
        }
    }
}
