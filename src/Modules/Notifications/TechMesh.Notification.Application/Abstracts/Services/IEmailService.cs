﻿namespace TechMesh.Notification.Application.Abstracts.Services;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
}