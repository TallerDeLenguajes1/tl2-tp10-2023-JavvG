using System;

namespace tl2_tp10_2023_JavvG.Exceptions;

public class OperationFailedException: Exception
{
    public OperationFailedException(string message): base(message) {}
    public OperationFailedException(string message, Exception innerException): base(message, innerException) {}
}