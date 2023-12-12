using System;

namespace tl2_tp10_2023_JavvG.Exceptions;

public class ElementNotFoundException: Exception
{
    public ElementNotFoundException(string message): base(message) {}
    public ElementNotFoundException(string message, Exception innerException): base(message, innerException) {}
}