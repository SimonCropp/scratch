using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class EndpointNames
{
    public static readonly string EndpointName = $"WireCompatCallbacks{Assembly.GetEntryAssembly().GetName().Name}";

    static EndpointNames()
    {
        All = Directory.GetDirectories(FindSolutionRoot(), "*_*")
            .Where(x => !x.Contains("Common"))
            .Select(x => $"WireCompatCallbacks{Path.GetFileName(x)}")
            .ToList();
    }

    static string FindSolutionRoot()
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;

        while (true)
        {
            if (Directory.EnumerateFiles(directory).Any(file => file.EndsWith(".sln")))
            {
                return directory;
            }

            var parent = Directory.GetParent(directory);

            if (parent == null)
            {
                throw new Exception("Could not find the solution directory.");
            }

            directory = parent.FullName;
        }
    }

    public static List<string> All;
}