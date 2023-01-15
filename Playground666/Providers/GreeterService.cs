namespace Providers;

public class GreeterService
{

    private readonly IDateTimeProvider _dateTimeProvider;

    public GreeterService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateGreetText()
    {
        var dateTimeNow = _dateTimeProvider.Now;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}