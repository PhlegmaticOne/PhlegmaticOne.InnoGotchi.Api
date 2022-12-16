﻿namespace PhlegmaticOne.InnoGotchi.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) => Message = message;

    public override string Message { get; }
}