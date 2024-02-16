using Microsoft.AspNetCore.Identity.UI.Services;

namespace Bookstore_Web.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to email
            return Task.CompletedTask;
        }
    }
}
