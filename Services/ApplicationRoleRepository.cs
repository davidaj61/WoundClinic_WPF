using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services
{
    public static class ApplicationRoleRepository
    {
        public static ApplicationRole Create(ApplicationRole role)
        {
            using var db = new ApplicationDbContext();
            db.ApplicationRoles.Add(role);
            db.SaveChanges();
            return role;
        }
        public static ApplicationRole Update(ApplicationRole role)
        {
            using var db = new ApplicationDbContext();
            db.ApplicationRoles.Update(role);
            db.SaveChanges();
            return role;
        }
        public static bool Delete(ApplicationRole role)
        {
            using var db = new ApplicationDbContext();
            db.ApplicationRoles.Remove(role);
            db.SaveChanges();
            return true;
        }
        public static ApplicationRole Get(int id)
        {
            using var db = new ApplicationDbContext();
            var role = db.ApplicationRoles.FirstOrDefault(x => x.Id == id);
            if (role == null)
            {
                return new ApplicationRole();
            }
            return role;
        }
        public static bool CheckRoleExist(int id)
        {
            using var db = new ApplicationDbContext();
            return db.ApplicationRoles.Any(x => x.Id == id);
        }
        public static List<ApplicationRole> GetAll()
        {
            using var db = new ApplicationDbContext();
            return db.ApplicationRoles.ToList();
        }
    }
}
