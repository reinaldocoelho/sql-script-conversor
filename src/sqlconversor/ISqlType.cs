
namespace SqlConversor
{ 
    public interface ISqlType
    {
        string FileName { get; }
        string CommandSeparator { get; }
        string FieldDelimiterStart { get; }
        string FieldDelimiterEnd { get; }
        
    }
}