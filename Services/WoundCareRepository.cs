using System.Globalization;
using Microsoft.EntityFrameworkCore;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Models.ViewModels;
using WoundClinic_WPF.Validations;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

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
    public static List<ReportViewModel> GetMiladiRangeOfPersianMonth(int yearShamsi, int monthShamsi)
    {
        var pc = new System.Globalization.PersianCalendar();

        DateTime start = pc.ToDateTime(yearShamsi, monthShamsi, 1, 0, 0, 0, 0);

        // تعیین تعداد روزهای ماه
        int daysInMonth = monthShamsi <= 6 ? 31 :
                          monthShamsi <= 11 ? 30 :
                          (pc.IsLeapYear(yearShamsi) ? 30 : 29);

        DateTime end = pc.ToDateTime(yearShamsi, monthShamsi, daysInMonth, 23, 59, 59, 999);

        using var db = new ApplicationDbContext();


        return (from woundCare in db.WoundCares
                join patient in db.Patients on woundCare.PatientId equals patient.NationalCode
                join person in db.Persons on patient.NationalCode equals person.NationalCode
                select new ReportViewModel
                {
                    FullName = person.FullName,
                    NationalCode = person.NationalCodeString,
                    Mobile = patient.MobileNumberString,
                    Date = woundCare.Date.ToPersianDate(),
                    Care = string.Join(", ", woundCare.DressingCares.Select(s => s.Dressing.DressingName)),
                    Payment = woundCare.DressingCares.Sum(s => s.Price)
                }).ToList();
    }
    public static List<ReportViewModel> GetWoundCareBetweenTwoDates(DateTime start, DateTime end)
    {
        using var db = new ApplicationDbContext();
        return (from woundCare in db.WoundCares
                join patient in db.Patients on woundCare.PatientId equals patient.NationalCode
                join person in db.Persons on patient.NationalCode equals person.NationalCode
                where woundCare.Date >= start && woundCare.Date <= end
                select new ReportViewModel
                {
                    FullName = person.FullName,
                    NationalCode = person.NationalCodeString,
                    Mobile = patient.MobileNumberString,
                    Date = woundCare.Date.ToPersianDate(),
                    Care = string.Join(", ", woundCare.DressingCares.Select(s => s.Dressing.DressingName)),
                    Payment = woundCare.DressingCares.Sum(s => s.Price)
                }).ToList();
    }

    public static bool HasAdmissionAtDate(DateTime date, long nationalCode)
    {
        using var db = new ApplicationDbContext();
        return db.WoundCares.Any(x => x.PatientId == nationalCode && x.Date == date);
    }

    public static (WoundCare,List<DressingCare>) GetResentAdmission(long nationalCode, DateTime date)
    {
        using var db = new ApplicationDbContext();
        var q= (from woundCare in db.WoundCares
               join patient in db.Patients on woundCare.PatientId equals patient.NationalCode
               join person in db.Persons on patient.NationalCode equals person.NationalCode
               join dc in db.DressingCares on woundCare.Id equals dc.WoundCareId
               join d in db.Dressings on dc.DressingId equals d.Id
               where woundCare.Date == date && woundCare.PatientId == nationalCode select woundCare).First() ;
         return (q,q.DressingCares.ToList());       
        
    }

}
