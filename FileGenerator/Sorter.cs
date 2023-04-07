using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGenerator
{
    public class Sorter
    {
        public void Sort(string fileName, int partLinesCount)
        {
            var files = SpliteFile(fileName, partLinesCount);
            SortParts(files, partLinesCount);
            SortResult(files);
        }



        private void SortResult(string[] files)
        {
            var readers = files.Select(i => new StreamReader(i));

            try
            {
                var lines = readers.Select(i => new LineState
                {
                    Line = new Line(i.ReadLine()),
                    Reader = i
                }).ToList();

                using var writer = new StreamWriter("result.txt");

                while (lines.Count > 0)
                {
                    var current = lines.OrderBy(i => i.Line).First();

                    writer.WriteLine(current.Line.Build());

                    if (current.Reader.EndOfStream)
                    {
                        lines.Remove(current);
                        continue;
                    }

                    current.Line = new Line(current.Reader.ReadLine());


                }  

            }
            finally
            {
                foreach (var reader in readers)
                    reader.Dispose();
            }

        }

        private void SortParts(string[] files, int partsLinesCount)
        {
            foreach (var file in files)
            {
                var sortedLines = File.ReadAllLines(file)
                    .Select(i => new Line(i))
                    .OrderBy(x => x);

                File.WriteAllLines(file, sortedLines.Select(i => i.Build()));
            }
        }

        private string[] SpliteFile(string fileName,int partCount)
        {
            var list = new List<string>();

            using(var reader = new StreamReader(fileName))
            {
                int partNumber = 0;

                while (!reader.EndOfStream)
                {
                    partNumber++;

                    var partName = partNumber + ".txt";

                    list.Add(partName);

                    using (var writer = new StreamWriter(partName))
                    {
                        for (int i = 0; i < partCount; i++)
                        {
                            if (reader.EndOfStream)
                                break;

                            writer.WriteLine(reader.ReadLine());
                        }
                    }
                }
            }
            return list.ToArray();
        }
    }
}
