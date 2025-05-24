using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services.IRepository;

public interface IPersonRepository
{
    public Person Create(Person person);

    public Person Update(Person person);

    public bool Delete(Person person);

    public Person Get(long id);

    public long GetCodeForNewAtba();

    public bool CheckPersonExist(long id);
    
    public IEnumerable<Person> GetAll();

    public Task<Person> CreateAsync(Person person);

    public Task<Person> UpdateAsync(Person person);

    public Task<bool> DeleteAsync(Person person);

    public Task<Person> GetAsync(long id);

    public Task<bool> CheckPersonExistAsync(long id);

    public Task<IEnumerable<Person>> GetAllAsync();


}
