using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services;

public static class PersonRepository 
{
    public static Person Create(Person person)
    {
        using var db = new ApplicationDbContext();
        db.Persons.Add(person);
        db.SaveChanges();
        return person;
    }

    public static Person Update(Person person)
    {
        using var db = new ApplicationDbContext();
        db.Persons.Update(person);
        db.SaveChanges();
        return person;
    }

    public static bool Delete(Person person)
    {
        using var db = new ApplicationDbContext();
        db.Persons.Remove(person);
        db.SaveChanges();
        return true;
    }

    public static Person Get(long id)
    {
        using var db = new ApplicationDbContext();
        var person = db.Persons.FirstOrDefault(x => x.NationalCode == id);
        if (person == null)
        {
            return new Person();
        }
        return person;
    }

    public static bool CheckPersonExist(long id)
    {
        using var db = new ApplicationDbContext();
        return db.Persons.Any(x => x.NationalCode == id);
    }

    public static IEnumerable<Person> GetAll()
    {
        using var db = new ApplicationDbContext();
        return db.Persons.ToList();
    }

    public static async Task<Person> CreateAsync(Person person)
    {
        using var db = new ApplicationDbContext();
        await db.Persons.AddAsync(person);
        await db.SaveChangesAsync();
        return person;
    }

    public static async Task<Person> UpdateAsync(Person person)
    {
        using var db = new ApplicationDbContext();
        db.Persons.Update(person);
        await db.SaveChangesAsync();
        return person;
    }
    public static async Task<bool> DeleteAsync(Person person)
    {
        using var db = new ApplicationDbContext();
        db.Persons.Remove(person);
        await db.SaveChangesAsync();
        return true;

    }

    public static async Task<Person> GetAsync(long id)
    {
        using var db = new ApplicationDbContext();
        var person = await db.Persons.FirstOrDefaultAsync(x => x.NationalCode == id);
        if (person == null)
        {
            return new Person();
        }
        return person;
    }

    public static async Task<IEnumerable<Person>> GetAllAsync()
    {
        using var db = new ApplicationDbContext();
        return await db.Persons.ToListAsync();
    }

    public static async Task<bool> CheckPersonExistAsync(long id)
    {
        using var db = new ApplicationDbContext();
        return await db.Persons.AnyAsync(x => x.NationalCode == id);
    }

    public static long GetCodeForNewAtba()
    {
        using var db = new ApplicationDbContext();
        var atba = db.Persons.Where(x => x.IsAtba == true).OrderByDescending(x=>x.NationalCode).FirstOrDefault();
        if (atba == null)
            return 9000000001;
        return atba.NationalCode+1;
    }
}
