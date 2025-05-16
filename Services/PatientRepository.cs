using Microsoft.EntityFrameworkCore;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services.IRepository;

namespace WoundClinic_WPF.Services
{
    public class PatientRepository:IPatientRepository
    {
        private readonly ApplicationDbContext _db;
        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Patient Create(Patient patient)
        {
            _db.Patients.Add(patient);
            _db.SaveChanges();
            return patient;
        }

        public  Patient Update(Patient patient)
        {
            _db.Patients.Update(patient);
            _db.SaveChanges();
            return patient;
        }
        public  bool Delete(Patient patient)
        {
            _db.Patients.Remove(patient);
            _db.SaveChanges();
            return true;

        }

        public  Patient Get(long id)
        {
            var patient = _db.Patients.FirstOrDefault(x => x.NationalCode == id);
            if (patient == null)
            {
                return new Patient();
            }
            return patient;
        }

        public  IEnumerable<Patient> GetAll()
        {
            return _db.Patients.ToList();
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            await _db.Patients.AddAsync(patient);
            await _db.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            _db.Patients.Update(patient);
            await _db.SaveChangesAsync();
            return patient;
        }
        public async Task<bool> DeleteAsync(Patient patient)
        {
            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<Patient> GetAsync(long id)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(x => x.NationalCode == id);
            if (patient == null)
            {
                return new Patient();
            }
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _db.Patients.ToListAsync();
        }
    }
}
