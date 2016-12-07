using System;

namespace SqlConversor
{
    public class LineProcessor
    {
        private ISqlType _from;
        private ISqlType _to;

        public LineProcessor(ISqlType from, ISqlType to) 
        {
            _from = from;
            _to = to;
        }

        public enum LineType {
            UNDEFINED,
            USE,
            INSERT,
            UPDATE,
            DELETE,
            SELECT,
            SET
        }

        public string ProcessLine(string line)
        {
            if(string.IsNullOrEmpty(line)) return "";
            var trimLine = line.Trim();
            if(trimLine.ToLower() == _from.CommandSeparator.ToLower()) return "";
            var lineBreaked = ProcessCommandSeparator(trimLine);
 
            var lineType = GetLineType(line);
            switch(lineType){
                case LineType.USE:
                    lineBreaked = FormatUseLine(lineBreaked);
                    break;
                case LineType.INSERT:
                    lineBreaked = FormatInsertLine(lineBreaked);
                    break;
                case LineType.UPDATE:
                    throw new NotImplementedException("Not implemented update replace.");
                case LineType.DELETE:
                    throw new NotImplementedException("Not implemented delete replace.");
                case LineType.SELECT:
                    throw new NotImplementedException("Not implemented select replace.");
                case LineType.SET:
                    lineBreaked = FormatSetLine(lineBreaked);
                    break;
                default:
                    throw new NotImplementedException("Unknow SQL line.");
            }

            return lineBreaked ?? "";
        }

        private string FormatUseLine(string line)
        {
            var formattedLine = line;
            formattedLine = formattedLine.Replace(_from.FieldDelimiterStart, _to.FieldDelimiterStart);
            formattedLine = formattedLine.Replace(_from.FieldDelimiterEnd, _to.FieldDelimiterEnd);
            return formattedLine;
        }

        private string FormatSetLine(string line)
        {
            // In first time, SET commands are ignored.
            return "";
        }

        private string FormatInsertLine(string line)
        {
            var valuesPosition = line.ToLower().IndexOf("values");
            var firstPart = line.Substring(0, valuesPosition);
            var lastPart = line.Substring(valuesPosition);
            
            // TODO: Adjust that to be maked only in MSSQL case and to know when exists in DBO or other SCHEMA.
            firstPart = firstPart.Replace("[dbo].", "");

            // Replace field delimiter in text before VALUES text
            firstPart = firstPart.Replace(_from.FieldDelimiterStart, _to.FieldDelimiterStart);
            firstPart = firstPart.Replace(_from.FieldDelimiterEnd, _to.FieldDelimiterEnd);
            var formattedLine = firstPart + lastPart;
            return formattedLine;
        }

        private LineType GetLineType(string line)
        {
            var lineToWork = line.Trim().ToLower();
            if(lineToWork.StartsWith("use")) return LineType.USE;
            if(lineToWork.StartsWith("insert")) return LineType.INSERT;
            if(lineToWork.StartsWith("update")) return LineType.UPDATE;
            if(lineToWork.StartsWith("delete")) return LineType.DELETE;
            if(lineToWork.StartsWith("select")) return LineType.SELECT;
            if(lineToWork.StartsWith("set")) return LineType.SET;
            return LineType.UNDEFINED;
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