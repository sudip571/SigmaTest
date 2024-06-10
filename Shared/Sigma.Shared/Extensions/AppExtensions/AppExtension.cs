using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Shared.Extensions.AppExtensions;

public static class AppExtension
{
    #region Extensions For string
    public static bool IsEqual(this string input, string stringToCompare) => String.Equals(input, stringToCompare, StringComparison.OrdinalIgnoreCase);
    public static int ToInteger(this string input) => int.TryParse(input, out var result) ? result : 0;
    public static bool ToBoolean(this string input) => bool.TryParse(input, out var result) ? result : false;

    #endregion
}
