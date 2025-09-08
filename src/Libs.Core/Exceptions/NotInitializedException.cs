using System;

namespace FwksLabs.Libs.Core.Exceptions;

public sealed class NotInitializedException(string property, string? message = null)
    : Exception(message ?? $"The required property '{property}' was not initialized before being called.");