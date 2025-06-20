using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace WoundClinic_WPF.Validations;

public static class Extensions
{
    public static DateTime ToMiladyDate(this string persianDate)
    {
        var date = persianDate.Split('/');

        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
        return pc.ToDateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), 0, 0, 0, 0);
    }

    public static string ToPersianDate(this DateTime date)
    {
        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
        return (pc.GetYear(date).ToString("0000") + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00"));
    }
    public static bool IsValidDate(this string date)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(date, @"^([1][3|4][0-9][0-9])[/]([0][1-9]|[1][012])[/]([0][1-9]|[1|2][0-9]|[3][01])$");
    }
    public static bool HasValue<T>(this T value) => value != null;
    public static bool HasValue(this string value) =>!string.IsNullOrEmpty(value)|| !string.IsNullOrWhiteSpace(value);

    public static string CreateAdmissionNumber(this DateTime date)
    {
        var sDate = date.ToPersianDate().Substring(0, 7).Replace("/", "");
        var xdate = DateTime.Now.ToString("HHmmss");
        return sDate+ xdate;
    }

}
