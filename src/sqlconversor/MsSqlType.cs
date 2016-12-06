namespace SqlConversor 
{
    public class MsSqlType : ISqlType
    {
        public string FileName { get; private set; }
        public string CommandSeparator { get { return "GO"; } }
        public string FieldDelimiterStart { get { return "["; } }
        public string FieldDelimiterEnd { get { return "]"; } }
        
        public MsSqlType(string fileName){
            FileName = fileName;
        }
    }
}