using System;
using System.Threading.Tasks;
using GitHubSync;
using Octokit;

class Program
{
    static Task Main()
    {
        var githubToken = Environment.GetEnvironmentVariable("Octokit_OAuthToken");

        var credentials = new Credentials(githubToken);
        var sync = new RepoSync(
            log: Console.WriteLine,
            defaultCredentials: credentials,
            syncMode: SyncMode.ExcludeAllByDefault);

        sync.AddSourceRepository(
            owner: "SimonCropp",
            repository: "Scratch",
            branch: "master");

        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/.editorconfig", "src/.editorconfig");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/config.yml", ".github/ISSUE_TEMPLATE/config.yml");

        sync.AddTargetRepository("pmcau", "AustralianElectorates", "master");
        sync.AddTargetRepository("SimonCropp", "Alt.SharePoint.Client", "master");
        sync.AddTargetRepository("SimonCropp", "CountryData", "master");
        sync.AddTargetRepository("SimonCropp", "EmptyFiles", "master");
        sync.AddTargetRepository("FluentDateTime", "FluentDateTime", "master");
        sync.AddTargetRepository("SimonCropp", "GitHubSync", "master");
        sync.AddTargetRepository("SimonCropp", "GitModTimes", "master");
        sync.AddTargetRepository("SimonCropp", "GraphQL.Attachments", "master");
        sync.AddTargetRepository("SimonCropp", "GraphQL.EntityFramework", "master");
        sync.AddTargetRepository("SimonCropp", "GraphQL.Validation", "master");
        sync.AddTargetRepository("SimonCropp", "LocalDb", "master");
        sync.AddTargetRepository("SimonCropp", "MarkdownSnippets", "master");
        sync.AddTargetRepository("SimonCropp", "NaughtyStrings", "master");
        sync.AddTargetRepository("SimonCropp", "NodaTime.Bogus", "master");
        sync.AddTargetRepository("SimonCropp", "PackageUpdate", "master");
        sync.AddTargetRepository("SimonCropp", "SeqProxy", "master");
        sync.AddTargetRepository("SimonCropp", "SetStartupProjects", "master");
        sync.AddTargetRepository("SimonCropp", "TextCopy", "master");
        sync.AddTargetRepository("SimonCropp", "Timestamp", "master");
        sync.AddTargetRepository("SimonCropp", "Verify", "master");
        sync.AddTargetRepository("SimonCropp", "Verify.Aspose", "master");
        sync.AddTargetRepository("SimonCropp", "Verify.EntityFramework", "master");
        sync.AddTargetRepository("SimonCropp", "Verify.ImageSharp", "master");
        sync.AddTargetRepository("SimonCropp", "Verify.SqlServer", "master");
        sync.AddTargetRepository("SimonCropp", "Verify.Web", "master");
        sync.AddTargetRepository("SimonCropp", "WaffleGenerator", "master");
        sync.AddTargetRepository("SimonCropp", "XunitContext", "master");


        return sync.Sync(SyncOutput.CreatePullRequest);
    }
}