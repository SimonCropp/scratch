using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        Console.WriteLine( typeof(SqlConnection).Assembly.CodeBase);
        Console.ReadKey();
    }
}