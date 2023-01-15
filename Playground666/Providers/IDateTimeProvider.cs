namespace Providers;

public interface IDateTimeProvider
{
    public DateTimeOffset Now { get; }
}

// public class DateTimeProvider : IDateTimeProvider
// {
//     public DateTimeOffset Now => DateTimeOffset.Now;
// }