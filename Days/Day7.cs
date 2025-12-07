using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2025.Days
{
    public class Day7: Day_base
    {
        public override void Solve()
        {
            Solve_P2();
        }

        public void Solve_P1() 
        {
            char[][] input = ReadWholeInput().Split(Environment.NewLine).Select(line => line.ToCharArray()).ToArray();

            List<Vector2> beams = new() { new Vector2(input[0].Select((c, i) => (c, i)).First(c => c.c == 'S').i, 0) };

            int splits = 0;
            for(int i = 0; i < input.Length - 1; i++)
            {
                // get next line splitters and create positional entries for them
                List<Vector2> splitters = input[i + 1].Select((c, i) => (c: c, i: i)).Where( item => item.c == '^' && beams.Contains(new Vector2(item.i, i))).Select(item => new Vector2(item.i, i+1)).ToList();

                splits += splitters.Count;

                // duplicate splitters and shift left one and right one
                var newBeams = splitters.Select(s => new Vector2(s.X - 1, s.Y)).Concat(splitters.Select(s => new Vector2(s.X + 1, s.Y)));

                // remove duplicate entries in splitters from beams, then merge into beams
                beams = beams.Select(b => new Vector2(b.X, b.Y + 1)).Where(b => splitters.Contains(b) == false)
                    .Concat(newBeams).Where(vec => vec.X >= 0 && vec.X < input[i].Length).ToList();
            }

            Console.WriteLine("A: " + splits);
        }

        struct Beam
        {
            public UInt64 Count;
            public int XPos;
            public List<Vector2> Source;

            public Beam(UInt64 _count, int _xpos)
            {
                Count = _count;
                XPos = _xpos;
            }
        }

        public void Solve_P2() //390684413472684
        {
            var inputAsCharAndXY = ReadWholeInput().Split(Environment.NewLine)
                .Select((line, ypos) => line.ToCharArray().Select((c, i) => (c, i, ypos)).Where(item => item.c != '.'))
                .SelectMany(l => l).Select(item => (Char: item.c, X: item.i, Y: item.ypos));

            int startX = inputAsCharAndXY.Where(p => p.Char == 'S').First().X;
            var splitters = inputAsCharAndXY.Where(p => p.Char != 'S').Select(input => new Vector2(input.X, input.Y)).ToHashSet();

            int maxY = (int)splitters.OrderByDescending(p => p.Y).First().Y;
            
            List<Beam> beams = new() { new Beam( 1, startX) };

            for(int YSlice = 0; YSlice <= maxY; ++YSlice)
            {
                // get a list of current splitters, since we will use this 3 times
                var beamsThatHit = beams.AsParallel().Where(b => splitters.Contains(new Vector2(b.XPos, YSlice))).ToHashSet();

                // create new beams on either side of the splitter
                // and concat all the newly created beams into our beams list
                // remove all beams that hit splitters
                // group all beams in the same location together,
                //      then merge them into one beam with a cumulative count of all beams at that position
                beams = beams.Concat(beamsThatHit.Select(b => new Beam(b.Count, b.XPos - 1))
                             .Concat(beamsThatHit.Select(b => new Beam(b.Count, b.XPos + 1))))
                             .Where(b => beamsThatHit.Contains(b) == false)
                             .GroupBy(b => b.XPos).Select(l => new Beam(l.Aggregate((acum, lb) => new Beam(acum.Count + lb.Count, lb.XPos)).Count, l.Key))
                             .ToList();
            }


            Console.WriteLine("Total Beams: " + beams.Aggregate((acum, lb) => new Beam(acum.Count + lb.Count, lb.XPos)).Count );
        }

        public void Solve_P2_brute()
        {
            string[] transposedInput = ReadWholeInput().Split(Environment.NewLine).Select(line => line.ToCharArray()).ToArray()
                    //.Where(line => line.Any(c => c!= '.'))
                    .SelectMany(substr => substr.Select((item, i) => (item, i)))
                    .GroupBy(i => i.i).Select(charList => new string(charList.Select(x => x.item).ToArray())).ToArray();

            int startY = transposedInput.Select((s, i) => (s, i)).Where(item => item.s[0] == 'S').Select(x => x.i).First();
            Stack<Vector2> stack = new();
            stack.Push(new Vector2(0, startY));
            UInt64 paths = 0;
            while (stack.Any())
            {
                Vector2 node = stack.Pop();
                // walk to splitter
                int nextSplitCount = transposedInput[(int)node.Y].Skip((int)node.X).TakeWhile(c => c != '^').Count();
                if (nextSplitCount == 0 || node.X + nextSplitCount >= transposedInput[0].Length)
                    continue;

                paths += 1; // 2;
                //Console.WriteLine(node.X + nextSplitCount);
                int nextSplitX = (int)node.X + nextSplitCount;
                stack.Push(new Vector2(nextSplitX, node.Y - 1));
                stack.Push(new Vector2(nextSplitX, node.Y + 1));   
            }

            Console.WriteLine("Paths: " + paths);
        }




    }
}
