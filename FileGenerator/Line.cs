using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGenerator
{
    public class Line : IComparable<Line>
    {
        private int pos;

        private string line;

        public int Number { get; set; }

        public ReadOnlySpan<char> Word => line.AsSpan(pos + 2);

        public Line(string line) 
        {
            pos = line.IndexOf(".");

            Number = int.Parse(line.AsSpan(0, pos));

            this.line = line;
        }

        public int CompareTo(Line other)
        {
            int result = Word.CompareTo(other.Word, StringComparison.InvariantCulture);

            if (result != 0)
                return result;
            return Number.CompareTo(other.Number);
        }

        internal string Build() => line;
    }
}
