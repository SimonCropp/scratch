using System;
using System.Collections.Generic;
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
        await SyncDotSettings();
    }

    Task RunMdSnippetsSync()
    {
        var sync = BuildSync();
        AddCommonItems(sync);
        sync.AddTargetRepository("SimonCropp", "MarkdownSnippets", "main");
        return sync.Sync(SyncOutput.MergePullRequest);
    }

    Task RunSync()
    {
        var sync = BuildSync();
        AddCommonItems(sync);
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/on-push-do-doco.yml", ".github/workflows/on-push-do-doco.yml");
        sync.AddTargetRepository("SimonCropp", "MarkdownSnippets", "main");
        foreach (var (org, repo) in RepoList())
        {
            sync.AddTargetRepository(org, repo, "main");
        }

        return sync.Sync(SyncOutput.MergePullRequest);
    }

    async Task SyncDotSettings()
    {
        var snippetsSync = BuildSync();
        snippetsSync.AddTargetRepository("SimonCropp", "MarkdownSnippets", "main");
        snippetsSync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/Resharper.sln.DotSettings", $"src/MarkdownSnippets.sln.DotSettings");
        await snippetsSync.Sync(SyncOutput.MergePullRequest);

        foreach (var (org, repo) in RepoList())
        {
            var sync = BuildSync();
            sync.AddTargetRepository(org, repo, "main");
            sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/Resharper.sln.DotSettings", $"src/{repo}.sln.DotSettings");
            await sync.Sync(SyncOutput.MergePullRequest);
        }
    }

    static IEnumerable<(string org, string repo)> RepoList()
    {
        yield return new("pmcau", "AustralianElectorates");
        yield return new("SimonCropp", "Argon");
        yield return new("SimonCropp", "CountryData");
        yield return new("SimonCropp", "ExtendedFluentValidation");
        yield return new("FluentDateTime", "FluentDateTime");
        yield return new("SimonCropp", "GitHubSync");
        yield return new("SimonCropp", "GitModTimes");
        yield return new("SimonCropp", "GraphQL.Attachments");
        yield return new("SimonCropp", "GraphQL.EntityFramework");
        yield return new("SimonCropp", "GraphQL.Validation");
        yield return new("SimonCropp", "LocalDb");
        yield return new("SimonCropp", "NaughtyStrings");
        yield return new("SimonCropp", "NodaTime.Bogus");
        yield return new("SimonCropp", "NullabilityInfo");
        yield return new("SimonCropp", "PandocNet");
        yield return new("SimonCropp", "PackageUpdate");
        yield return new("SimonCropp", "OssIndexClient");
        yield return new("SimonCropp", "Replicant");
        yield return new("SimonCropp", "SeqProxy");
        yield return new("SimonCropp", "SetStartupProjects");
        yield return new("SimonCropp", "SimpleInfoName");
        yield return new("CopyText", "TextCopy");
        yield return new("SimonCropp", "Timestamp");
        yield return new("SimonCropp", "WaffleGenerator");
        yield return new("SimonCropp", "XunitContext");
        yield return new("VerifyTests", "EmptyFiles");
        yield return new("VerifyTests", "Verify");
        yield return new("VerifyTests", "Verify.AngleSharp");
        yield return new("VerifyTests", "Verify.AspNetCore");
        yield return new("VerifyTests", "Verify.Aspose");
        yield return new("VerifyTests", "Verify.Blazor");
        yield return new("VerifyTests", "Verify.Cosmos");
        yield return new("VerifyTests", "Verify.DocNet");
        yield return new("VerifyTests", "Verify.DiffPlex");
        yield return new("VerifyTests", "DiffEngine");
        yield return new("VerifyTests", "Verify.GrapeCity");
        yield return new("VerifyTests", "Verify.EntityFramework");
        yield return new("VerifyTests", "Verify.HeadlessBrowsers");
        yield return new("VerifyTests", "Verify.ImageHash");
        yield return new("VerifyTests", "Verify.ImageSharp");
        yield return new("VerifyTests", "Verify.ImageMagick");
        yield return new("VerifyTests", "Verify.MassTransit");
        yield return new("VerifyTests", "Verify.MicrosoftLogging");
        yield return new("VerifyTests", "Verify.ICSharpCode.Decompiler");
        yield return new("VerifyTests", "Verify.NServiceBus");
        yield return new("VerifyTests", "Verify.NodaTime");
        yield return new("VerifyTests", "Verify.NSubstitute");
        yield return new("VerifyTests", "Verify.Phash");
        yield return new("VerifyTests", "Verify.Phash");
        yield return new("VerifyTests", "Verify.Quibble");
        yield return new("VerifyTests", "Verify.RavenDB");
        yield return new("VerifyTests", "Verify.Syncfusion");
        yield return new("VerifyTests", "Verify.SourceGenerators");
        yield return new("VerifyTests", "Verify.SqlServer");
        yield return new("VerifyTests", "Verify.Http");
        yield return new("VerifyTests", "Verify.WinForms");
        yield return new("VerifyTests", "Verify.Xaml");
        yield return new("VerifyTests", "Verify.Xamarin");

        yield return new("NServiceBusExtensions", "Newtonsoft.Json.Encryption");
        yield return new("NServiceBusExtensions", "NServiceBus.Attachments");
        yield return new("NServiceBusExtensions", "NServiceBus.AuditFilter");
        yield return new("NServiceBusExtensions", "NServiceBus.Bond");
        yield return new("NServiceBusExtensions", "NServiceBus.HandlerOrdering");
        yield return new("NServiceBusExtensions", "NServiceBus.Hyperion");
        yield return new("NServiceBusExtensions", "NServiceBus.Jil");
        yield return new("NServiceBusExtensions", "NServiceBus.Json");
        yield return new("NServiceBusExtensions", "NServiceBus.MessagePack");
        yield return new("NServiceBusExtensions", "NServiceBus.MicrosoftLogging");
        yield return new("NServiceBusExtensions", "NServiceBus.MsgPack");
        yield return new("NServiceBusExtensions", "NServiceBus.ProtoBufGoogle");
        yield return new("NServiceBusExtensions", "NServiceBus.ProtoBufNet");
        yield return new("NServiceBusExtensions", "NServiceBus.Serilog");
        yield return new("NServiceBusExtensions", "NServiceBus.SqlNative");
        yield return new("NServiceBusExtensions", "NServiceBus.Utf8Json");
        yield return new("NServiceBusExtensions", "NServiceBus.Validation");
    }

    private static void AddCommonItems(RepoSync sync)
    {
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/.editorconfig", "src/.editorconfig");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/config.yml", ".github/ISSUE_TEMPLATE/config.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/on-tag-do-release.yml", ".github/workflows/on-tag-do-release.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/merge-dependabot.yml", ".github/workflows/merge-dependabot.yml");
        sync.AddSourceItem(TreeEntryTargetType.Blob, "RepoSync/Source/dependabot.yml", ".github/dependabot.yml");
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
            branch: "main");
        return sync;
    }

    public Tests(ITestOutputHelper output) : base(output)
    {
    }
}