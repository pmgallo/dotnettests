using Spectre.Console;
using SpectreConsoleTest.Services.Interface;

namespace SpectreConsoleTest.Services.Implementation;

public class PoliteGreeter : IGreeter
{
    public void SayHello(string customMessage)
    {
        AnsiConsole.Markup($"[underline yellow]Hello there!.[/] {customMessage}");
    }
}