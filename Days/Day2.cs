using System;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2025.Days
{
    public class Day2: Day_base
    {
        public override void Solve()
        {
            Solve_P2_linq();
        }

        public void Solve_P1()
        {
            var list = ReadWholeInput().Split(",").Select(x => x.Trim().Split("-")).Select(x => (x[0], x[1])).ToList();

            List<UInt64> found = new();

            foreach(var strRange in list)
            {
                UInt64 startInt = UInt64.Parse(strRange.Item1);
                UInt64 endInt = UInt64.Parse(strRange.Item2);

                for (UInt64 start = startInt; start <= endInt; start++)
                {
                    string numStr = start.ToString();
                    if (numStr.Length % 2 != 0)
                        continue;

                    string highNumStr = numStr.Substring(0, numStr.Length / 2);
                    string lowNumStr = numStr.Substring(numStr.Length / 2);

                    if (highNumStr == lowNumStr)
                    {
                        Console.WriteLine(start);
                        found.Add(start);
                    }
                }
            }

            UInt64 total = 0;
            foreach (var val in found)
            {
                total += (UInt64)val;
            }

            Console.WriteLine("val: " + total);
        }

        public IEnumerable<UInt64> CreateRange(UInt64 start, UInt64 count)
        {
            UInt64 limit = start + count;

            while (start < limit)
            {
                yield return start;
                start++;
            }
        }

        public void Solve_P1_linq()
        {
            Console.WriteLine("Answer: " + ReadWholeInput().Split(",").Select(x => x.Trim().Split("-"))
                .Select(x => CreateRange(UInt64.Parse(x[0]), UInt64.Parse(x[1]) - UInt64.Parse(x[0])).Select(x => x.ToString())
                                       .Where(n => (n.Length % 2 == 0) && (n.Substring(0, n.Length / 2) == n.Substring(n.Length / 2))).ToList())
                .Where(x => x.Count > 0).Aggregate((accum, val) => accum.Concat(val).ToList())
                .Select(x => UInt64.Parse(x)).Aggregate((accum, val) => accum += val).ToString());
        }

        public void Solve_P2_linq()
        {
            Console.WriteLine("Answer: " + ReadWholeInput().Split(",").Select(x => x.Trim().Split("-"))
                .Select(x => CreateRange(UInt64.Parse(x[0]), UInt64.Parse(x[1]) - UInt64.Parse(x[0])).Select(x => x.ToString())
                                   .Where(numStr => Enumerable.Range(1, (numStr.Length / 2) + 1)
                                   .Where(seq => Enumerable.Range(0, numStr.Length - seq)
                                   .Where(i => (Regex.Matches(numStr, numStr.Substring(0, seq)).Count * seq == numStr.Length)).Count() > 0).Count() > 0)
                                   .ToList()
                        )
                .Where(x => x.Count > 0).Aggregate((accum, val) => accum.Concat(val).ToList())
                .Select(x => UInt64.Parse(x)).Aggregate((accum, val) => accum += val).ToString());
        }

        public void Solve_P2() //30962646823
        {
            var list = ReadWholeInput().Split(",").Select(x => x.Trim().Split("-")).Select(x => (x[0], x[1])).ToList();

            List<UInt64> found = new();

            foreach (var strRange in list)
            {
                UInt64 startInt = UInt64.Parse(strRange.Item1);
                UInt64 endInt = UInt64.Parse(strRange.Item2);

                for (UInt64 start = startInt; start <= endInt; start++)
                {
                    string num = start.ToString();
                    for (int seq = 1; seq < (num.Length / 2) + 1; seq++)
                    {
                        for (int i = 0; i < num.Length - seq; i++)
                        {
                            if (Regex.Matches(num, num.Substring(0, seq)).Count * seq == num.Length)
                            {
                                Console.WriteLine("num: " + num);
                                found.Add(UInt64.Parse(num));
                                i = num.Length;
                                seq = num.Length;
                            }
                        }
                    }
                }
            }

            UInt64 total = 0;
            foreach (var val in found)
            {
                total += (UInt64)val;
            }

            Console.WriteLine("val: " + total);

        }
    }
}
