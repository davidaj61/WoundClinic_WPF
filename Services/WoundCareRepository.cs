using System.Globalization;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services;

public static class WoundCareRepository
{
    public static bool Add(WoundCare woundCare)
    {
        using var db = new ApplicationDbContext();
        db.WoundCares.Add(woundCare);
        return db.SaveChanges() > 0;
    }

    public static void Delete(WoundCare wc)
    {
        using var db = new ApplicationDbContext();
        if (wc != null)
            db.WoundCares.Remove(wc);
        db.SaveChanges();
    }
    public static List<int> GetPersianYearsFromGregorianDates()
    {
        using var db = new ApplicationDbContext();
        var persianCalendar = new PersianCalendar();
        var gregorianDates = db.WoundCares.Select(wc => wc.Date).ToList();
        return gregorianDates
            .Select(date => persianCalendar.GetYear(date))
            .Distinct()
            .OrderByDescending(y => y)
            .ToList();
    }
    public static (DateTime StartMiladi, DateTime EndMiladi) GetMiladiRangeOfPersianMonth(int yearShamsi, int monthShamsi)
    {
        var pc = new System.Globalization.PersianCalendar();

        DateTime start = pc.ToDateTime(yearShamsi, monthShamsi, 1, 0, 0, 0, 0);

        // تعیین تعداد روزهای ماه
        int daysInMonth = monthShamsi <= 6 ? 31 :
                          monthShamsi <= 11 ? 30 :
                          (pc.IsLeapYear(yearShamsi) ? 30 : 29);

        DateTime end = pc.ToDateTime(yearShamsi, monthShamsi, daysInMonth, 23, 59, 59, 999);

        return (start, end);
    }
    

}
