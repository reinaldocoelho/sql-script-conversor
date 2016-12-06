namespace SqlConversor 
{
    public class MsSqlType : ISqlType
    {
        public string FileName { get; private set; }

        public MsSqlType(string fileName){
            FileName = fileName;
        }
    }
}