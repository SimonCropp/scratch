using System;
using System.Threading.Tasks;
using GitHubSync;
using Octokit;
using Xunit;
using Xunit.Abstractions;

public class Tests : XunitContextBase
{
    [Fact]
    public async Task Run()
    {
        await RunSync();
        await RunMdSnippetsSync();
    }

    Task RunMdSnippetsSync()
    {
        var sync = BuildSync();
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/.editorconfig", "src/.editorconfig");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/config.yml", ".github/ISSUE_TEMPLATE/config.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/on-tag-do-release.yml", ".github/workflows/on-tag-do-release.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/merge-dependabot.yml", ".github/workflows/merge-dependabot.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/dependabot.yml", ".github/dependabot.yml");

        sync.AddTargetRepository("SimonCropp", "MarkdownSnippets", "master");

        return sync.Sync(SyncOutput.MergePullRequest);
    }

    Task RunSync()
    {
        var sync = BuildSync();
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/.editorconfig", "src/.editorconfig");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/config.yml", ".github/ISSUE_TEMPLATE/config.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/on-push-do-doco.yml", ".github/workflows/on-push-do-doco.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/on-tag-do-release.yml", ".github/workflows/on-tag-do-release.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/merge-dependabot.yml", ".github/workflows/merge-dependabot.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/dependabot.yml", ".github/dependabot.yml");

        sync.AddTargetRepository("pmcau", "AustralianElectorates", "master");
        sync.AddTargetRepository("SimonCropp", "Alt.SharePoint.Client", "master");
        sync.AddTargetRepository("SimonCropp", "CountryData", "master");
        sync.AddTargetRepository("VerifyTests", "EmptyFiles", "master");
        sync.AddTargetRepository("SimonCropp", "EfFluentValidation", "master");
        sync.AddTargetRepository("FluentDateTime", "FluentDateTime", "master");
        sync.AddTargetRepository("SimonCropp", "GitHubSync", "master");
        sync.AddTargetRepository("SimonCropp", "GitModTimes", "master");
        sync.AddTargetRepository("SimonCropp", "GraphQL.Attachments", "master");
        sync.AddTargetRepository("SimonCropp", "GraphQL.EntityFramework", "master");
        sync.AddTargetRepository("SimonCropp", "GraphQL.Validation", "master");
        sync.AddTargetRepository("SimonCropp", "LocalDb", "master");
        sync.AddTargetRepository("SimonCropp", "NaughtyStrings", "master");
        sync.AddTargetRepository("SimonCropp", "NodaTime.Bogus", "master");
        sync.AddTargetRepository("SimonCropp", "PackageUpdate", "master");
        sync.AddTargetRepository("SimonCropp", "OssIndexClient", "master");
        sync.AddTargetRepository("SimonCropp", "Replicant", "master");
        sync.AddTargetRepository("SimonCropp", "SeqProxy", "master");
        sync.AddTargetRepository("SimonCropp", "SetStartupProjects", "master");
        sync.AddTargetRepository("CopyText", "TextCopy", "master");
        sync.AddTargetRepository("SimonCropp", "Timestamp", "master");
        sync.AddTargetRepository("VerifyTests", "Verify", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.AngleSharp", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.Aspose", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.Blazor", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.Cosmos", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.DiffPlex", "master");
        sync.AddTargetRepository("VerifyTests", "DiffEngine", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.EntityFramework", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.HeadlessBrowsers", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.ImageSharp", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.ImageMagick", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.ICSharpCode.Decompiler", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.Phash", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.RavenDB", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.SqlServer", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.Web", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.WinForms", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.Xaml", "master");
        sync.AddTargetRepository("VerifyTests", "Verify.Xamarin", "master");
        sync.AddTargetRepository("SimonCropp", "WaffleGenerator", "master");
        sync.AddTargetRepository("SimonCropp", "XunitContext", "master");

        return sync.Sync(SyncOutput.MergePullRequest);
    }

    RepoSync BuildSync()
    {
        var githubToken = Environment.GetEnvironmentVariable("Octokit_OAuthToken");

        var credentials = new Credentials(githubToken);
        var sync = new RepoSync(
            log: WriteLine,
            defaultCredentials: credentials,
            syncMode: SyncMode.ExcludeAllByDefault);

        sync.AddSourceRepository(
            owner: "SimonCropp",
            repository: "Scratch",
            branch: "master");
        return sync;
    }

    public Tests(ITestOutputHelper output) : base(output)
    {
    }

}