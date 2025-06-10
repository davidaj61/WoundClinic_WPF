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
    public static void Add(ApplicationUser user, string password)
    {
        using var db = new ApplicationDbContext();
        user.IsActive = true;
        db.ApplicationUsers.Add(user);
        db.SaveChanges();
    }

    public static void Edit(ApplicationUser user)
    {
        using var db = new ApplicationDbContext();
        db.ApplicationUsers.Update(user);
        db.SaveChanges();
    }

    public static void ChangePassword(ApplicationUser user, string newPassword)
    {
        using var db = new ApplicationDbContext();
        user.PasswordHash = Encryption.GetSha256Hash(newPassword);
        db.ApplicationUsers.Update(user);
        db.SaveChanges();
    }

    public static bool ChangeUserActivate (ApplicationUser user)
    {
        using var db = new ApplicationDbContext();
        user.IsActive = !user.IsActive;
        db.ApplicationUsers.Update(user);
        return db.SaveChanges()>0;
    }

    public static ApplicationUser GetByNationalCode(long nationalCode)
    {
        using var db = new ApplicationDbContext();
        return db.ApplicationUsers.Include(x => x.Person).FirstOrDefault(u => u.NationalCode == nationalCode);
    }

    public static bool CheckPassword(ApplicationUser user, string password) => user.PasswordHash == Encryption.GetSha256Hash(password);

    public static void SetUserLastLogin(ApplicationUser user)
    {
        using var db = new ApplicationDbContext();
        user.LastLogin = DateTime.Now;
        db.ApplicationUsers.Update(user);
        db.SaveChanges();
    }

    public static List<ApplicationUser> GetAllUsers()
    {
        using var db = new ApplicationDbContext();
        return db.ApplicationUsers.Include(x => x.Person).ToList();
    }
}
