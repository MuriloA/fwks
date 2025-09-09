using System.Collections.Generic;
using FluentValidation.Results;
using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Core.Types;

namespace FwksLabs.Libs.Core.Contexts;

public sealed class NotificationContext : INotificationContext
{
    private readonly Dictionary<string, object?> _messages = new();

    public ApplicationError? Error { get; private set; }
    public IDictionary<string, object?> Messages => _messages;

    public void Add(ApplicationError error, IDictionary<string, object?> messages)
    {
        Error = error;

        foreach (var message in messages)
            _messages.Add(message.Key, message.Value);
    }

    public void Add(ApplicationError error, ValidationResult validationResult)
    {
        Add(error, validationResult.ToErrorDictionary());
    }
}