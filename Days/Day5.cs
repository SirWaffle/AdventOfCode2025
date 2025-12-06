using System;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2025.Days
{
    public class Day5: Day_base
    {
        public override void Solve()
        {
            Solve_P2();
        }
     
        public void Solve_P1() //698
        {
            var all = ReadWholeInput().Split(Environment.NewLine);
            var ranges = all.TakeWhile(x => x.Length > 0).Select(x => (UInt64.Parse(x.Split('-')[0]), UInt64.Parse(x.Split('-')[1]))).ToList();
            Console.WriteLine("A: " + all.Skip(ranges.Count + 1).Select(x => UInt64.Parse(x)).Where(item => ranges.Where(x => item >= x.Item1 && item <= x.Item2).Any()).ToList().Count);
        }

        //           352807801032167

        public void Solve_P2()
        {
            var ranges = ReadWholeInput().Split(Environment.NewLine)
                .TakeWhile(x => x.Length > 0).Select(x => (Start: UInt64.Parse(x.Split('-')[0]), End: UInt64.Parse(x.Split('-')[1]))).ToList();

            for(int i = 0; i < ranges.Count; i++) 
            {
                var range1 = ranges[i];
                for(int j = i + 1; j < ranges.Count; ++j)
                {
                    var range2 = ranges[j];

                    if(    (range1.Start >= range2.Start && range1.Start <= range2.End) 
                        || (range1.End >= range2.Start && range1.End <= range2.End)
                        || (range1.Start <= range2.End && range1.End >= range2.Start) )
                    {
                        ranges.Remove(range1);
                        ranges.Remove(range2);

                        ranges.Insert(i, (Math.Min(range1.Start, range2.Start), Math.Max(range1.End, range2.End)));
                        i = 0;
                        break;
                    }
                }

            }

            Console.WriteLine("A: " + ranges.Select(x => (x.Item2 - x.Item1) + 1).Aggregate((acum, next) => acum += next));
        }



    }
}
