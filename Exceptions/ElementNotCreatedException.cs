using System;

namespace tl2_tp10_2023_JavvG.Exceptions;

public class ElementNotCreatedException: Exception
{
    public ElementNotCreatedException(string message): base(message) {}
    public ElementNotCreatedException(string message, Exception innerException): base(message, innerException) {}
}