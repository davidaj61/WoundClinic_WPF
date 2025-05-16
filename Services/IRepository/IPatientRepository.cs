using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services.IRepository;

public interface IPatientRepository
{
    public Patient Create(Patient patient);

    public Patient Update(Patient patient);

    public bool Delete(Patient patient);

    public Patient Get(long id);

    public IEnumerable<Patient> GetAll();

    public Task<Patient> CreateAsync(Patient patient);

    public Task<Patient> UpdateAsync(Patient patient);

    public Task<bool> DeleteAsync(Patient patient);

    public Task<Patient> GetAsync(long id);

    public Task<IEnumerable<Patient>> GetAllAsync();
}
