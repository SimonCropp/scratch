using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NaughtyStrings;
using Xunit;
using Xunit.Abstractions;

public class XunitUsage
{
    ITestOutputHelper output;

    public XunitUsage(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void Run(string naughtyString)
    {
        output.WriteLine(naughtyString);
    }

    public static IEnumerable<object[]> GetData()
    {
        return TheNaughtyStrings.All
            .Select(naughty => new object[] {naughty});
    }
}