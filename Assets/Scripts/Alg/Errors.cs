using System;

public class AngleValueException : Exception
{
    public AngleValueException() : base("Angle is too great for the machine")
    {
    }

    public AngleValueException(string message)
        : base(message)
    {
    }

    public AngleValueException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
