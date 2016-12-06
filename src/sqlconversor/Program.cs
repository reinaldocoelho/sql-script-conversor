using System;

namespace SqlConversor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args == null || args.Length == 0) 
            {
                ShowHelp();
                return;
            }

            var inputType = GetInputType(args);
            var outputType = GetOutputType(args);

            var canProcess = true;
            if(inputType == null) {
                Console.WriteLine("Please, inform a input type with -it param.");
                canProcess = false;
            }
            if(outputType == null){ 
                Console.WriteLine("Please, inform a output type with -ot param.");
                canProcess = false;
            }

            if(canProcess){
                var conversor = new ScriptConversor(inputType, outputType);
                conversor.Process();
            }
        }
        
        static void ShowHelp ()
        {
            Console.WriteLine ("Usage: sqlconversor [OPTIONS]");
            Console.WriteLine ("Convert SQL script to other database formmat.");
            Console.WriteLine ();
            Console.WriteLine ("Options:");
            Console.WriteLine ("-i input file - File name of input script file.");
            Console.WriteLine ("-o output file - Output file name.");
            Console.WriteLine ("-it input file type - Input script type [mssql, mysql]");
            Console.WriteLine ("-ot output file type - Output script type [mssql, mysql]");
        }

        static ISqlType GetInputType (string[] args)
        {
            var strInputType = "mssql";
            var inputFileName = "";
            
            for(var i = 0; i < args.Length; i++) {
                if(args[i].ToLower().Contains("-it")){
                    if(args.Length < (i+ 1)) continue;
                    strInputType = args[i+1];
                }
                if(args[i].ToLower().Contains("-i")){
                    if(args.Length < (i+ 1)) continue;
                    inputFileName = args[i+1];
                }
            }
            if(string.IsNullOrWhiteSpace(inputFileName)) return null;
            return SqlTypeFactory.GetInstanceFor(strInputType, inputFileName);
        }

        static ISqlType GetOutputType (string[] args)
        {
            var strInputType = "mysql";
            var inputFileName = "";
            
            for(var i = 0; i < args.Length; i++) {
                if(args[i].ToLower().Contains("-ot")){
                    if(args.Length < (i+ 1)) continue;
                    strInputType = args[i+1];
                }
                if(args[i].ToLower().Contains("-o")){
                    if(args.Length < (i+ 1)) continue;
                    inputFileName = args[i+1];
                }
            }
            if(string.IsNullOrWhiteSpace(inputFileName)) return null;
            return SqlTypeFactory.GetInstanceFor(strInputType, inputFileName);
        }

    }
}
