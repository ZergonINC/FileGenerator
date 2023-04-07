using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGenerator
{
   public class Generator
   {
        private readonly Random random = new();
        private readonly string[] words;

        public Generator()
        {
            words = Enumerable.Range(0, 10000).Select(i => 
            {
                var range = Enumerable.Range(0, random.Next(20, 100));

                var chars = range.Select(y => (char)random.Next('A', 'Z')).ToArray();

                var str = new string(chars);

                return str;
            }).ToArray();       
        }


        public string Generate(int linesCount)
        {
            var filename = "L" + linesCount + ".txt";

            using(var writer = new StreamWriter(filename))
            {
                for (int i = 0; i < linesCount; i++)
                {
                    writer.WriteLine(GenerateNumber() + ". " + GenerateString());
                }
            }

            return filename;
        }

        private string GenerateString()
        {
            return words[random.Next(0, words.Length)];
        }

        private string GenerateNumber()
        {
            return random.Next(0, 10000).ToString();
        }
    }
}
