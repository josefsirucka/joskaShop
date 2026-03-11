// <copyright file="GlobalUsings.cs" company="Josef Širůčka">
// Copyright (c) Josef Širůčka. All rights reserved.
// </copyright>
// <summary>Created on: 11.03 2026</summary>

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
