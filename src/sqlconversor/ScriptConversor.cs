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

            var processor = new LineProcessor(_from, _to);
            using (var outfile = new StreamWriter(File.OpenWrite(outputfile))) 
            {
                using(var infile = new System.IO.StreamReader(File.OpenRead(_from.FileName)))
                {  
                    while((line = infile.ReadLine()) != null)  
                    {
                        outfile.WriteLine(processor.ProcessLine(line));  
                        counter++;
                        if(counter > 100) return;
                    }  
                }
            }
            System.Console.WriteLine("Was processed {0} lines.", counter);
        }
    }
}