﻿namespace BusinessLogic.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlContent = null);
    }
}
