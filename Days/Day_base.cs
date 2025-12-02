using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Days
{
    public abstract class Day_base
    {
        public string ReadWholeExample()
        {
            string file = File.ReadAllText(@"Days\" + this.GetType().Name + "_example.txt");
            return file;
        }

        public string ReadWholeInput()
        {
            string file = File.ReadAllText(@"Days\" + this.GetType().Name + "_input.txt");
            return file;
        }

        public string ReadWholeInput2()
        {
            string file = File.ReadAllText(@"Days\" + this.GetType().Name + "_input2.txt");
            return file;
        }

        public abstract void Solve_P1();

        public abstract void Solve_P2();
    }
}
