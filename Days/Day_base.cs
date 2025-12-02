using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Days
{
    public class Day_base
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
    }
}
