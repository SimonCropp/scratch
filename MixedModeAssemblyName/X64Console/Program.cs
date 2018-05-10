﻿using System;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        var token = AssemblyName.GetAssemblyName(@"C:\Code\scratch\MixedModeAssemblyName\X86Console\bin\Debug\X86Console.exe").GetPublicKeyToken();
        var publicKeyToken = string.Concat(token.Select(i => i.ToString("x2")));
        Console.WriteLine(publicKeyToken);
        Console.ReadLine();
    }
}