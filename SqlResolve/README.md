So this has 

 * `<CopyLocalLockFileAssemblies>`
 * `<PackageReference Include="System.Data.SqlClient" Version="4.5.1" />`

So it should resolve System.Data.SqlClient from the local dir.

but this `Console.WriteLine( typeof(SqlConnection).Assembly.CodeBase);` gives 

```
file:///C:/Users/ascropp/.nuget/packages/system.data.sqlclient/4.5.1/runtimes/win/lib/netcoreapp2.1/System.Data.SqlClient.dll
```

