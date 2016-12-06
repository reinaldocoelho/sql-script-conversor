using System;

namespace SqlConversor
{
    public class ScriptConversor 
    {
        private ISqlType _from;
        private ISqlType _to;
        private string _outputFile;

        public ScriptConversor(ISqlType from, ISqlType to) {
            _from = from;
            _to = to;
            _outputFile = "output.sql";
        }

        public void Process(){
            Console.WriteLine("Processing convertion.");
            Console.WriteLine("Finished.");
        }

        public void SetOutputFileName(string filename){
            _outputFile = filename;
        }
    }
}