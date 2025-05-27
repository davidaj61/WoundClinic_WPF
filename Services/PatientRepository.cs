using Microsoft.EntityFrameworkCore;
using WoundClinic.Models.ViewModels;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services;

public static class PatientRepository
{
    public static Patient Create(Patient patient)
    {
        using var db = new ApplicationDbContext();
        db.Patients.Add(patient);
        db.SaveChanges();
        return patient;
    }

    public static Patient Update(Patient patient)
    {
        using var db = new ApplicationDbContext();
        db.Patients.Update(patient);
        db.SaveChanges();
        return patient;
    }
    public static bool Delete(Patient patient)
    {
        using var db = new ApplicationDbContext();
        db.Patients.Remove(patient);
        db.SaveChanges();
        return true;

    }

    public static Patient Get(long id)
    {
        using var db = new ApplicationDbContext();
        var patient = db.Patients.Include(x=>x.Person).FirstOrDefault(x => x.NationalCode == id);
        if (patient == null)
        {
            return new Patient();
        }
        return patient;
    }

    public static IEnumerable<Patient> GetAll()
    {
        using var db = new ApplicationDbContext();
        return db.Patients.ToList();
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
        var patient = await db.Patients.FirstOrDefaultAsync(x => x.NationalCode == id);
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

    public static List<SearchedPatientViewModel> SearchPatients(string str)
    {
        using var db = new ApplicationDbContext();
        return db.Patients.Where(x => x.MobileNumberString.Contains(str) || x.Person.NationalCodeString.Contains(str) || x.Person.FullName.Contains(str)).Select(x => new SearchedPatientViewModel
        {
            NationalCodeString = x.Person.NationalCodeString,
            FirstName = x.Person.FirstName,
            LastName = x.Person.LastName,   
            MobileNumberString=x.MobileNumberString,
            Address=x.Address,
        }).ToList();
    }
}
