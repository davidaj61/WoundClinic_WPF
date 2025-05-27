using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services;

public static class ApplicationUserRepository 
{
    public static void Create(ApplicationUser user, string password)
    {
        using var db = new ApplicationDbContext();
        user.PasswordHash = Encryption.GetSha256Hash(password);
        db.ApplicationUsers.Add(user);
        db.SaveChanges();
    }

    public static ApplicationUser GetByNationalCode(long nationalCode)
    {
        using var db = new ApplicationDbContext();
        return db.ApplicationUsers.Include(x=>x.Person).FirstOrDefault(u => u.NationalCode == nationalCode);
    }

    public static bool CheckPassword(ApplicationUser user, string password)=>user.PasswordHash == Encryption.GetSha256Hash(password);

    public static void SetUserLastLogin(ApplicationUser user)
    {
        using var db = new ApplicationDbContext();
        user.LastLogin = DateTime.Now;
        db.ApplicationUsers.Update(user);
        db.SaveChanges();
    }
}
