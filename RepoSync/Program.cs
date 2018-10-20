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
        var sync = new RepoSync(credentials, "SimonCropp", "scratch", "master", Console.WriteLine);
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/.editorconfig", ".editorconfig");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/ISSUE_TEMPLATE/bug_report.md", ".github/ISSUE_TEMPLATE/bug_report.md");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/ISSUE_TEMPLATE/feature_request.md", ".github/ISSUE_TEMPLATE/feature_request.md");
        sync.AddTarget("SimonCropp", "CaptureSnippets", "master");
        sync.AddTarget("SimonCropp", "WaffleGenerator", "master");
        sync.AddTarget("SimonCropp", "Timestamp", "master");
        sync.AddTarget("SimonCropp", "TextCopy", "master");
        sync.AddTarget("SimonCropp", "SetStartupProjects", "master");
        sync.AddTarget("SimonCropp", "ObjectApproval", "master");
        sync.AddTarget("SimonCropp", "NodaTime.Bogus", "master");
        sync.AddTarget("SimonCropp", "Newtonsoft.Json.Encryption", "master");
        sync.AddTarget("SimonCropp", "NaughtyStrings", "master");
        sync.AddTarget("SimonCropp", "GraphQL.EntityFramework", "master");
        sync.AddTarget("SimonCropp", "GitModTimes", "master");
        sync.AddTarget("SimonCropp", "EfCore.InMemoryHelpers", "master");
        sync.AddTarget("SimonCropp", "CountryData", "master");
        return sync.Sync();
    }
}