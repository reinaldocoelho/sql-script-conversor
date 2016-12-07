# sql-script-conversor
SQL script conversor

How to use:
Imagine you has a MSSSQL Data Script exported from Query Manager.
Then you need to convert this to MySql insert script.

Run:
<pre><code class="csharp">
sqlconversor.exe -i mssql-filename.sql -o mysql-filename.sql -it mssql -ot mysql
</code></pre>
