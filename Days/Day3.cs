using System;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2025.Days
{
    public class Day3: Day_base
    {
        public override void Solve()
        {
            Solve_P1();
        }

        public void Solve_P1() //17074
        {
            Console.WriteLine("a: " + DoThing(2));
        }

        public void Solve_P2()//169512729575727
        {
            Console.WriteLine("a: " + DoThing(12));
        }

        public UInt64 DoThing(int digits)
        {
            var input = ReadWholeInput().Split(Environment.NewLine).Select(x => x.Trim()).Select(x => x.ToCharArray().ToList());
            UInt64 total = 0;
            foreach (var b in input)
            {
                var bank = b;
                List<char> taken = new();
                
                while (taken.Count < digits)
                {
                    taken.Add(bank.Take(bank.Count - (digits - (taken.Count + 1))).OrderByDescending(x => int.Parse(x.ToString())).First());
                    bank = bank.Skip(bank.IndexOf(taken.Last()) + 1).ToList();
                }
                total += UInt64.Parse(new string(taken.ToArray()));
            }
            return total;
        }

    }
}
