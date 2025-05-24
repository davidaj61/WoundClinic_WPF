using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services.IRepository;

namespace WoundClinic_WPF.Services;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _db;
    public PersonRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public Person Create(Person person)
    {
        _db.Persons.Add(person);
        _db.SaveChanges();
        return person;
    }

    public Person Update(Person person)
    {
        _db.Persons.Update(person);
        _db.SaveChanges();
        return person;
    }

    public bool Delete(Person person)
    {
        _db.Persons.Remove(person);
        _db.SaveChanges();
        return true;
    }

    public Person Get(long id)
    {
        var person = _db.Persons.FirstOrDefault(x => x.NationalCode == id);
        if (person == null)
        {
            return new Person();
        }
        return person;
    }

    public bool CheckPersonExist(long id)
    {
        return _db.Persons.Any(x => x.NationalCode == id);
    }

    public IEnumerable<Person> GetAll()
    {
        return _db.Persons.ToList();
    }

    public async Task<Person> CreateAsync(Person person)
    {
        await _db.Persons.AddAsync(person);
        await _db.SaveChangesAsync();
        return person;
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        _db.Persons.Update(person);
        await _db.SaveChangesAsync();
        return person;
    }
    public async Task<bool> DeleteAsync(Person person)
    {
        _db.Persons.Remove(person);
        await _db.SaveChangesAsync();
        return true;

    }

    public async Task<Person> GetAsync(long id)
    {
        var person = await _db.Persons.FirstOrDefaultAsync(x => x.NationalCode == id);
        if (person == null)
        {
            return new Person();
        }
        return person;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _db.Persons.ToListAsync();
    }

    public async Task<bool> CheckPersonExistAsync(long id)
    {
        return await _db.Persons.AnyAsync(x => x.NationalCode == id);
    }

    public long GetCodeForNewAtba()
    {
        var atba = _db.Persons.Where(x => x.IsAtba == true).OrderByDescending(x=>x.NationalCode).FirstOrDefault();
        if (atba == null)
            return 9000000001;
        return atba.NationalCode+1;
    }
}
