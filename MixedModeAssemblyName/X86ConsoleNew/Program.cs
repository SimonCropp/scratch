﻿using System;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        var token = AssemblyName.GetAssemblyName(@"C:\Code\scratch\MixedModeAssemblyName\X64ConsoleNew\bin\Debug\net452\X64ConsoleNew.exe").GetPublicKeyToken();
        var publicKeyToken = string.Concat(token.Select(i => i.ToString("x2")));
        Console.WriteLine(publicKeyToken);
        Console.ReadLine();
    }
}