using System;

namespace FwksLabs.Libs.Core.Exceptions;

public class ResultValueNotNullException : Exception
{
    public ResultValueNotNullException() : base("Value for the Result cannot be 'null'.")
    {
    }

    public ResultValueNotNullException(string message) : base(message)
    {
    }
}