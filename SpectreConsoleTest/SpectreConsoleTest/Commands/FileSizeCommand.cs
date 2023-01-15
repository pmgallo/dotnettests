using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Cli;
using Spectre.Console;
using SpectreConsoleTest.Services.Interface;
using ValidationResult = Spectre.Cli.ValidationResult;

namespace SpectreConsoleTest.Commands;

public sealed class FileSizeCommand : AsyncCommand<FileSizeCommand.Settings>
{
    public IGreeter _greeter { get; set; }
    public sealed class Settings : CommandSettings
    {
        [Description("Path to search. Defaults to current directory.")]
        [CommandArgument(0, "[searchPath]")]
        public string? SearchPath { get; init; }

        [Description("Pattern of file name's to be used in the search")]
        [CommandOption("-p|--pattern")]
        public string? SearchPattern { get; init; }

        [Description("Search for hidden files (true by default)")]
        [CommandOption("--hidden")]
        [DefaultValue(true)]
        public bool IncludeHidden { get; init; }
    }

    public FileSizeCommand(IGreeter greeter)
    {
        _greeter = greeter;
    }

    public override ValidationResult Validate(CommandContext context, Settings settings)
    {
        base.Validate(context, settings);
        
        // if(!Directory.Exists(settings.SearchPath))
        //     return ValidationResult.Error($"Path {settings.SearchPath} does not exists.");
        
        return ValidationResult.Success();
    }

    public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        var searchOptions = new EnumerationOptions
        {
            AttributesToSkip = settings.IncludeHidden
                ? FileAttributes.Hidden | FileAttributes.System
                : FileAttributes.System
        };

        var searchPattern = settings.SearchPattern ?? "*.*";
        var searchPath = settings.SearchPath ?? Directory.GetCurrentDirectory();
        var files = new DirectoryInfo(searchPath)
            .GetFiles(searchPattern, searchOptions);

        var totalFileSize = files
            .Sum(fileInfo => fileInfo.Length);

        _greeter.SayHello("You results are ready!");
        var table = new Table();

        // Add some columns
        table.AddColumn("Pattern");
        table.AddColumn("Path");
        table.AddColumn(new TableColumn("Size (bytes)").Centered());

        await AnsiConsole.Progress()
            .Columns(new ProgressColumn[] 
            {
                new TaskDescriptionColumn(),    // Task description
                new ProgressBarColumn(),        // Progress bar
                new PercentageColumn(),         // Percentage
                new RemainingTimeColumn(),      // Remaining time
                new SpinnerColumn(),            // Spinner
            })
            .StartAsync(async ctx =>
            {
                // Define tasks
                var task1 = ctx.AddTask("[green]Reticulating splines[/]");
                var task2 = ctx.AddTask("[green]Folding space[/]");

                while (!ctx.IsFinished)
                {
                    // Simulate some work
                    await Task.Delay(20);

                    // Increment
                    task1.Increment(1.5);
                    task2.Increment(1.0);
                }
            });
        
        // Add some rows
        table.AddRow($"[green]{searchPattern}[/]", $"[green]{searchPath}[/]", $"[blue]{totalFileSize:N0}[/]");

        // Render the table to the console
        AnsiConsole.Write(table);
        //AnsiConsole.MarkupLine($"Total file size for [green]{searchPattern}[/] files in [green]{searchPath}[/]: [blue]{totalFileSize:N0}[/] bytes");

        return 0;
    }
}