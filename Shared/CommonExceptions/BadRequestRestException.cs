﻿using Microsoft.AspNetCore.Identity;

namespace Shared.CommonExceptions;

public class BadRequestRestException : Exception
{
    public IEnumerable<IdentityError>? Errors { get; }

    public BadRequestRestException(string message, IEnumerable<IdentityError>? errors = null)
        : base(message)
    {
        Errors = errors;
    }
}
