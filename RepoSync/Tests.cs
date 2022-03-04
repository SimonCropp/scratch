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

        sync.AddTargetRepository("pmcau", "AustralianElectorates", "main");
        sync.AddTargetRepository("SimonCropp", "Alt.SharePoint.Client", "main");
        sync.AddTargetRepository("SimonCropp", "CountryData", "main");
        sync.AddTargetRepository("VerifyTests", "EmptyFiles", "main");
        sync.AddTargetRepository("SimonCropp", "EfFluentValidation", "main");
        sync.AddTargetRepository("FluentDateTime", "FluentDateTime", "main");
        sync.AddTargetRepository("SimonCropp", "GitHubSync", "main");
        sync.AddTargetRepository("SimonCropp", "GitModTimes", "main");
        sync.AddTargetRepository("SimonCropp", "GraphQL.Attachments", "main");
        sync.AddTargetRepository("SimonCropp", "GraphQL.EntityFramework", "main");
        sync.AddTargetRepository("SimonCropp", "GraphQL.Validation", "main");
        sync.AddTargetRepository("SimonCropp", "LocalDb", "main");
        sync.AddTargetRepository("SimonCropp", "NaughtyStrings", "main");
        sync.AddTargetRepository("SimonCropp", "NodaTime.Bogus", "main");
        sync.AddTargetRepository("SimonCropp", "PackageUpdate", "main");
        sync.AddTargetRepository("SimonCropp", "OssIndexClient", "main");
        sync.AddTargetRepository("SimonCropp", "Replicant", "main");
        sync.AddTargetRepository("SimonCropp", "SeqProxy", "main");
        sync.AddTargetRepository("SimonCropp", "SetStartupProjects", "main");
        sync.AddTargetRepository("CopyText", "TextCopy", "main");
        sync.AddTargetRepository("SimonCropp", "Timestamp", "main");
        sync.AddTargetRepository("SimonCropp", "WaffleGenerator", "main");
        sync.AddTargetRepository("SimonCropp", "XunitContext", "main");
        sync.AddTargetRepository("VerifyTests", "Verify", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.AngleSharp", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.Aspose", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.Blazor", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.Cosmos", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.DiffPlex", "main");
        sync.AddTargetRepository("VerifyTests", "DiffEngine", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.EntityFramework", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.HeadlessBrowsers", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.ImageSharp", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.ImageMagick", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.ICSharpCode.Decompiler", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.NodaTime", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.Phash", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.RavenDB", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.SqlServer", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.Web", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.WinForms", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.Xaml", "main");
        sync.AddTargetRepository("VerifyTests", "Verify.Xamarin", "main");

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