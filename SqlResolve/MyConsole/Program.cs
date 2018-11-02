using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        var assemblyCodeBase = typeof(SqlConnection).Assembly.CodeBase;
        Console.WriteLine( assemblyCodeBase);
        Console.ReadKey();
    }
}