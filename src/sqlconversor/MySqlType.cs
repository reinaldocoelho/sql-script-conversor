namespace SqlConversor 
{
    public class MySqlType : ISqlType
    {
        public string FileName { get; private set; }
        public string CommandSeparator { get { return ";"; } }
        public string FieldDelimiterStart { get { return ""; } }
        public string FieldDelimiterEnd { get { return ""; } }

        public MySqlType(string fileName){
            FileName = fileName;
        }
    }
}