using Injection.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Cli;
using Spectre.Console;
using SpectreConsoleTest.Commands;
using SpectreConsoleTest.Services.Implementation;
using SpectreConsoleTest.Services.Interface;

//AnsiConsole.Markup("[underline red]Hello[/] World!");
//AnsiConsole.WriteLine();

var registrations = new ServiceCollection();
registrations.AddSingleton<IGreeter, PoliteGreeter>();

// Create a type registrar and register any dependencies.
// A type registrar is an adapter for a DI framework.
var registrar = new TypeRegistrar(registrations);

//var table = new Table().Centered();
// await AnsiConsole.Live(table)
//     .StartAsync(async ctx => 
//     {
//         table.AddColumn("Foo");
//         ctx.Refresh();
//         await Task.Delay(1000);
//
//         table.AddColumn("Bar");
//         ctx.Refresh();
//         await Task.Delay(1000);
//     });
    
var app = new CommandApp(registrar);
app.Configure(config =>
{
    config.AddCommand<FileSizeCommand>("size")
        .IsHidden()
        .WithAlias("file-size")
        .WithDescription("Gets the file size for a directory.")
        .WithExample(new[] {"size", "c:\\windows", "--pattern", "*.dll"});
    //config.AddCommand<CommitCommand>("commit");
    //config.AddCommand<RebaseCommand>("rebase");
    
    config.PropagateExceptions();
    config.ValidateExamples();

});

return app.Run(args);