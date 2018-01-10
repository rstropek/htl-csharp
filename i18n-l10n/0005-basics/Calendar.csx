using System.Globalization;
using static System.Console;

var myDT = new DateTime(2018, 1, 10, new GregorianCalendar());
var myCal = CultureInfo.InvariantCulture.Calendar;
WriteLine($"## Original:\n\tYear: {myCal.GetYear(myDT)}\n\tMonth: {myCal.GetMonth(myDT)}");

myDT = myCal.AddMonths(myDT, 5);
WriteLine($"## +5 Months:\n\tYear: {myCal.GetYear(myDT)}\n\tMonth: {myCal.GetMonth(myDT)}");

var thaiCal = new ThaiBuddhistCalendar();
WriteLine($"## Thai:\n\tYear: {thaiCal.GetYear(myDT)}\n\tMonth: {thaiCal.GetMonth(myDT)}");
