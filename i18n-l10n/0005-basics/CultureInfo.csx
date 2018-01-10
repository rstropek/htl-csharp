using System.Globalization;
using System;
using static System.Console;

var cultures = new[] {
    new CultureInfo("de-DE", false),
    new CultureInfo("de-AT", false),
    new CultureInfo("en-us", false)
};

foreach (var culture in cultures)
{
    WriteLine(1000.ToString("#,##0.00", culture));
    WriteLine(new DateTime(2018, 1, 10).ToString("F", culture));
}
