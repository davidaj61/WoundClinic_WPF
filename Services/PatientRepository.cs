using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WoundClinic.Models.ViewModels;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services;

public static class PatientRepository
{
    /// <summary>
    /// افزودن بیمار
    /// </summary>
    /// <param name="patient"></param>
    /// <returns></returns>
    public static Patient Create(Patient patient)
    {
        using var db = new ApplicationDbContext();
        db.Patients.Add(patient);
        db.SaveChanges();
        return patient;
    }
    /// <summary>
    /// ویرایش اطلاعات بیمار
    /// </summary>
    /// <param name="patient"></param>
    /// <returns></returns>
    public static Patient Update(Patient patient)
    {
        using var db = new ApplicationDbContext();
        db.Patients.Update(patient);
        db.SaveChanges();
        return patient;
    }
    /// <summary>
    /// حذف اطلاعات بیمار
    /// </summary>
    /// <param name="patient"></param>
    /// <returns></returns>
    public static bool Delete(Patient patient)
    {
        using var db = new ApplicationDbContext();
        db.Patients.Remove(patient);
        db.SaveChanges();
        return true;

    }
    /// <summary>
    /// دریافت اطلاعات بیمار+ اطلاعات دموگرافیک
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Patient Get(long id)
    {
        using var db = new ApplicationDbContext();
        var patient = db.Patients.Include(p=>p.Person).FirstOrDefault(x => x.NationalCode == id);
        if (patient == null)
        {
            return new Patient();
        }  
        return patient;
    }

    public static IEnumerable<Patient> GetAll()
    {
        using var db = new ApplicationDbContext();
        return db.Patients.Include(p=>p.Person).ToList();
    }

    public static async Task<Patient> CreateAsync(Patient patient)
    {
        using var db = new ApplicationDbContext();
        await db.Patients.AddAsync(patient);
        await db.SaveChangesAsync();
        return patient;
    }

    public static async Task<Patient> UpdateAsync(Patient patient)
    {
        using var db = new ApplicationDbContext();
        db.Patients.Update(patient);
        await db.SaveChangesAsync();
        return patient;
    }
    public static async Task<bool> DeleteAsync(Patient patient)
    {
        using var db = new ApplicationDbContext();
        db.Patients.Remove(patient);
        await db.SaveChangesAsync();
        return true;

    }

    public static async Task<Patient> GetAsync(long id)
    {
        using var db = new ApplicationDbContext();
        var patient = await db.Patients.Include(p=>p.Person).FirstOrDefaultAsync(x => x.NationalCode == id);
        if (patient == null)
        {
            return new Patient();
        }
        return patient;
    }

    public static async Task<IEnumerable<Patient>> GetAllAsync()
    {
        using var db = new ApplicationDbContext();
        return await db.Patients.ToListAsync();
    }

    public static ObservableCollection<SearchedPatientViewModel> SearchPatients(string str)
    {
        using var db = new ApplicationDbContext();
        var result=db.Patients.Include(p=>p.Person).Where(x => x.MobileNumber.ToString().Contains(str) || x.Person.NationalCode.ToString().Contains(str) || x.Person.FirstName.Contains(str) || x.Person.LastName.Contains(str)).Select(x => new SearchedPatientViewModel
        {
            NationalCodeString = x.Person.NationalCodeString,
            FullName = x.Person.FullName,
            MobileNumberString =x.MobileNumberString,
        }).ToList();
        return new ObservableCollection<SearchedPatientViewModel>(result);
    }


}
