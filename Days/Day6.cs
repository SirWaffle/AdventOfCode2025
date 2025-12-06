using System;
using System.Text.RegularExpressions;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2025.Days
{
    public class Day6: Day_base
    {
        public override void Solve()
        {
            Solve_P2();
        }

        public void Solve_P1() 
        {
            Console.WriteLine("A: " + ReadWholeInput().Split(Environment.NewLine).Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                .SelectMany(line => line.Select((item, i) => (item, i))).GroupBy(i => i.i)
                .Select(l => l.AsEnumerable().Reverse().ToList())
                .Select(oplist => oplist.Skip(1).Select(x => Int64.Parse(x.item)).Aggregate( (accum, next) => accum == 0? next: oplist[0].item[0] == '*'? accum *= next: accum += next))
                .Sum());
        }

        public void Solve_P2()
        {
            var input = ReadWholeInput().Split(Environment.NewLine).AsEnumerable();
            int strPos = 0;
            UInt64 total = 0;
            while(strPos < input.Last().Length)
            {
                int substrLen = input.Last().Skip(strPos + 1).TakeWhile(c => c == ' ').Count();

                // total += for all but the last line, get the substring for the numbers that match the op + whitespace
                //    create a list of (char, index of char in string) for each string, and flatten into a single list
                //    group the (char, index)'s into separate lists based on index, (this effectively transposes chars)
                //    then flatten each list of chars into a string
                //    convert the strings into uints, and apply the operation across the list
                total += input.SkipLast(1).Select(line => line.Substring(strPos, substrLen + 1))
                    .SelectMany(substr => substr.Select((item, i) => (item, i)))
                    .GroupBy(i => i.i)
                    .Select(charList => new string(charList.Select(x => x.item).ToArray()))
                    .Where(str => str.Trim().Length > 0).Select(ns => UInt64.Parse(ns.Trim()))
                    .Aggregate((accum, next) => accum == 0 ? next : input.Last()[strPos] == '*' ? accum *= next : accum += next);

                strPos += substrLen + 1;
            }

            Console.WriteLine("A: " + total);
        }




    }
}
