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
        return sync.Sync();
    }
}