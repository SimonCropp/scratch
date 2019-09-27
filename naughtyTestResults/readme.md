test run work in in resharper

fails with `dotnet test`:

```
PS C:\Code\scratch\naughtyTestResults> dotnet test
Test run for C:\Code\scratch\naughtyTestResults\naughtyTestResults\bin\Debug\netcoreapp3.0\naughtyTestResults.dll(.NETCoreApp,Version=v3.0)
Microsoft (R) Test Execution Command Line Tool Version 16.3.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.
[xUnit.net 00:00:00.43] naughtyTestResults: Skipping test case with duplicate ID '7348127cf8651a60289e2acbce7e4360e64b02f6' ('XunitUsage.Run(naughtyString: "<foo val="bar" />")' and 'XunitUsage.Run(naughtyString: "<foo val="bar" />")')
[xUnit.net 00:00:00.44] naughtyTestResults: Skipping test case with duplicate ID 'd25954935eb7ba6bc371c9b485b663c8f1c81c3c' ('XunitUsage.Run(naughtyString: "<img\\x47src=x onerror=\"javascript:alert(1)\">")' and 'XunitUsage.Run(naughtyString: "<img\\x47src=x onerror=\"javascript:alert(1)\">")')
[xUnit.net 00:00:00.44] naughtyTestResults: Skipping test case with duplicate ID 'ed5b0aa040cb454efee6c7827a3345e17df0a1be' ('XunitUsage.Run(naughtyString: "<img \\x47src=x onerror=\"javascript:alert(1)\">")' and 'XunitUsage.Run(naughtyString: "<img \\x47src=x onerror=\"javascript:alert(1)\">")')
[xUnit.net 00:00:00.44] naughtyTestResults: Skipping test case with duplicate ID 'f888c336bba02ea9ce674e34bb3a4156c41e5c92' ('XunitUsage.Run(naughtyString: "-")' and 'XunitUsage.Run(naughtyString: "-")')

Test Run Successful.
Total tests: 501
     Passed: 501
 Total time: 1.4585 Seconds
 ```