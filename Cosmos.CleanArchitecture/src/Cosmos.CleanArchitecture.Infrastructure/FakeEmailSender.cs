using Cosmos.CleanArchitecture.Core.Interfaces;

namespace Cosmos.CleanArchitecture.Infrastructure;

public class FakeEmailSender : IEmailSender
{
  public Task SendEmailAsync(string to, string from, string subject, string body)
  {
    return Task.CompletedTask;
  }
}
