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
        public Dressing Create(Dressing dressing)
        {
            _db.Dressings.Add(dressing);
            _db.SaveChanges();
            return dressing;
        }

        public bool Delete(Dressing dressing)
        {
            _db.Dressings.Remove(dressing);
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<Dressing> GetAll()
        {
            return _db.Dressings.ToList();
        }

        public Dressing Get(byte id)
        {
            var dressing=_db.Dressings.FirstOrDefault(x=> x.Id==id);
            if (dressing == null)
                return new Dressing();
            return dressing;
        }

        public Dressing Update(Dressing dressing)
        {
            _db.Dressings.Update(dressing);
            _db.SaveChanges(true);
            return dressing;
        }
    }
}
