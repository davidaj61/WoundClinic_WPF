using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;
using WoundClinic_WPF.Services.IRepository;

namespace WoundClinic_WPF.Services
{
    class DressingRepository : IDressingRepository
    {
        private readonly ApplicationDbContext _db;
        public DressingRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Dressing> CreateAsync(Dressing dressing)
        {
            await _db.Dressings.AddAsync(dressing);
            await _db.SaveChangesAsync();
            return dressing;
        }

        public async Task<bool> DeleteAsync(Dressing dressing)
        {
            _db.Dressings.Remove(dressing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Dressing>> GetAllAsync()
        {
            return await _db.Dressings.ToListAsync();
        }

        public async Task<Dressing> GetAsync(byte id)
        {
            var dressing=await _db.Dressings.FirstOrDefaultAsync(x=> x.Id==id);
            if (dressing == null)
                return new Dressing();
            return dressing;
        }

        public async Task<Dressing> UpdateAsync(Dressing dressing)
        {
            _db.Dressings.Update(dressing);
            await _db.SaveChangesAsync(true);
            return dressing;
        }
    }
}
