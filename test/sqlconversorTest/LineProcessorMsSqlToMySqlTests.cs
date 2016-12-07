using System;
using Xunit;
using SqlConversor;

namespace SqlConversorTests
{
    public class LineProcessorMsSqlToMySqlTests
    {
        [Fact]
        public void MustProcessEmptyLine() 
        {
            // Simple Insert
            var processor = new LineProcessor(new MsSqlType("mssql.sql"), new MySqlType("mysql.sql"));
            var mssqlLine = "";
            var mysqlLine = processor.ProcessLine(mssqlLine); 
            var expectedMySql = "";
            Assert.Equal(expectedMySql, mysqlLine);
        }

        [Fact]
        public void MustProcessUseLine() 
        {
            // Simple Use
            var processor = new LineProcessor(new MsSqlType("mssql.sql"), new MySqlType("mysql.sql"));
            var mssqlLine = "USE [encontact]";
            var mysqlLine = processor.ProcessLine(mssqlLine); 
            var expectedMySql = "USE encontact;";
            Assert.Equal(expectedMySql, mysqlLine);

            // With GO Use
            mssqlLine = @"USE [encontact]
GO";
            mysqlLine = processor.ProcessLine(mssqlLine); 
            expectedMySql = @"USE encontact
;";
            Assert.Equal(expectedMySql, mysqlLine);
        }
        
        [Fact]
        public void MustProcessInsertLine() 
        {
            // Simple Insert
            var processor = new LineProcessor(new MsSqlType("mssql.sql"), new MySqlType("mysql.sql"));
            var mssqlLine = "INSERT [dbo].[accounts] ([id], [create_date], [name], [email], [username], [password], [is_active], [deleted], [ClientId], [AuthMethod], [ProfileId]) VALUES (1, CAST(N'2015-03-27T10:44:10.323' AS DateTime), N'Admin', N'admin@local.com', N'admin@local.com', N'481F6CC0511143CCDD7E2D1B1B94FAF0A700A8B49CD13922A70B5AE28ACAA8C5', 1, 0, 0, 0, 1)";
            var mysqlLine = processor.ProcessLine(mssqlLine); 
            var expectedMySql = "INSERT accounts (id, create_date, name, email, username, password, is_active, deleted, ClientId, AuthMethod, ProfileId) VALUES (1, CAST(N'2015-03-27T10:44:10.323' AS DateTime), N'Admin', N'admin@local.com', N'admin@local.com', N'481F6CC0511143CCDD7E2D1B1B94FAF0A700A8B49CD13922A70B5AE28ACAA8C5', 1, 0, 0, 0, 1);";
            Assert.Equal(expectedMySql, mysqlLine);
        }
        
        [Fact]
        public void MustProcessSetLine() 
        {
            // Simple Insert
            var processor = new LineProcessor(new MsSqlType("mssql.sql"), new MySqlType("mysql.sql"));
            var mssqlLine = "SET IDENTITY_INSERT [dbo].[accounts] ON";
            var mysqlLine = processor.ProcessLine(mssqlLine); 
            var expectedMySql = "";
            Assert.Equal(expectedMySql, mysqlLine);
        }
    }
}
