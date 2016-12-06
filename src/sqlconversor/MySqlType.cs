namespace SqlConversor 
{
    public class MySqlType : ISqlType
    {
        public string FileName { get; private set; }

        public MySqlType(string fileName){
            FileName = fileName;
        }
    }
}