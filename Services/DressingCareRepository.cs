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
    class DressingCareRepository:IDressingCareRepository
    {
        private readonly ApplicationDbContext _db;
        public DressingCareRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<DressingCare> CreateAsync(DressingCare dressingCare)
        {
            await _db.DressingCares.AddAsync(dressingCare);
            await _db.SaveChangesAsync();
            return dressingCare;
        }

        public async Task<bool> DeleteAsync(DressingCare dressingCare)
        {
            _db.DressingCares.Remove(dressingCare);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DressingCare>> GetAllAsync()
        {
            return await _db.DressingCares.ToListAsync();
        }

        public async Task<DressingCare> GetAsync(byte id)
        {
            var dressingCare = await _db.DressingCares.FirstOrDefaultAsync(x => x.Id == id);
            if (dressingCare == null)
                return new DressingCare();
            return dressingCare;
        }

        public async Task<DressingCare> UpdateAsync(DressingCare dressingCare)
        {
            _db.DressingCares.Update(dressingCare);
            await _db.SaveChangesAsync(true);
            return dressingCare;
        }
    }
}
