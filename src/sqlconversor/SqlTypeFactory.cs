namespace SqlConversor
{
    public static class SqlTypeFactory
    {
        public static ISqlType GetInstanceFor(string type, string fileName)
        {
            switch(type){
                case "mssql":
                    return new MsSqlType(fileName);
                case "mysql":
                case "mariadb":
                    return new MySqlType(fileName);
                default:
                    return null;
            }
        }
    }
}