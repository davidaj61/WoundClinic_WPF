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
    public class ApplicationUserRepository:IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(ApplicationUser user, string password)
        {
            user.PasswordHash = Encryption.GetSha256Hash(password);
            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
        }

        public ApplicationUser GetByNationalCode(long nationalCode)
        {
            return _context.ApplicationUsers
                .Include(u => u.Person)
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.NationalCode == nationalCode);
        }

        public bool CheckPassword(ApplicationUser user, string password)
        {
            return user.PasswordHash == Encryption.GetSha256Hash(password);
        }
    }
}
