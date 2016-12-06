using System;
using System.IO;

namespace SqlConversor
{
    public class ScriptConversor 
    {
        private ISqlType _from;
        private ISqlType _to;

        public ScriptConversor(ISqlType from, ISqlType to) 
        {
            _from = from;
            _to = to;
        }

        public void Process()
        {
            Console.WriteLine("Starting convertion...");
            ProcessLineByLine();
            Console.WriteLine("Finished.");
        }

        private void ProcessLineByLine()
        {
            var counter = 0;  
            var line = "";  
            var outputfile = _to.FileName ?? "outputscript.sql";

            using (var outfile = new StreamWriter(File.OpenWrite(outputfile))) 
            {
                using(var infile = new System.IO.StreamReader(File.OpenRead(_from.FileName)))
                {  
                    while((line = infile.ReadLine()) != null)  
                    {  
                        outfile.WriteLine(ProcessLine(line));  
                        counter++;
                        if(counter > 100) return;
                    }  
                }
            }
            System.Console.WriteLine("Was processed {0} lines.", counter);
        }

        private string ProcessLine(string line)
        {
            if(string.IsNullOrEmpty(line)) return "";
            var trimLine = line.Trim();
            if(trimLine.ToLower() == _from.CommandSeparator.ToLower()) return "";
            var lineBreaked = ProcessCommandSeparator(trimLine);
            if(lineBreaked.ToLower().StartsWith("use")) {
                lineBreaked = lineBreaked.Replace(_from.FieldDelimiterStart, _to.FieldDelimiterStart);
                lineBreaked = lineBreaked.Replace(_from.FieldDelimiterEnd, _to.FieldDelimiterEnd);
                return lineBreaked;
            }

            return "";
        }

        private string ProcessCommandSeparator(string line)
        {
            var lineBreakCount = _from.CommandSeparator.Length;
            if(line.Length < lineBreakCount) return line;
            if(line.Substring(line.Length-lineBreakCount, lineBreakCount).ToLower() == _from.CommandSeparator.ToLower())
            {
                var withoutFromLineBreak = line.Substring(0, line.Length-lineBreakCount);
                if(string.IsNullOrWhiteSpace(withoutFromLineBreak)) return "";
                return withoutFromLineBreak + _to.CommandSeparator;
            }
            if(!string.IsNullOrWhiteSpace(line)) return line + _to.CommandSeparator;
            return "";
        }
    }
}