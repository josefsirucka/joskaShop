// <copyright file="CurrencyService.cs" company="Papirfly Group">
// Copyright (c) Papirfly Group. All rights reserved.
// </copyright>

using ISO._4217;

namespace JoskaShop.Services;

/// <summary>
/// Currency service.
/// </summary>
public class CurrencyService
{
    /// <summary>
    /// Checks if the provided code is a valid ISO 4217 currency code.
    /// </summary>
    /// <param name="code">Currency string.</param>
    /// <returns>True if the code is a valid ISO 4217 currency code; otherwise, false.</returns>
    public bool IsIso4217Code(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return false;
        }

        code = code.ToUpperInvariant();

        return CurrencyCodesResolver.Codes.Any(c => c.Code == code);
    }
}
