using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services;

public static class DressingRepository
{
    public static Dressing Create(Dressing dressing)
    {
        using var db = new ApplicationDbContext();
        db.Dressings.Add(dressing);
        db.SaveChanges();
        return dressing;
    }

    public static bool Delete(Dressing dressing)
    {
        using var db = new ApplicationDbContext();
        db.Dressings.Remove(dressing);
        db.SaveChanges();
        return true;
    }

    public static IEnumerable<Dressing> GetAll()
    {
        using var db = new ApplicationDbContext();
        return db.Dressings.ToList();
    }

    public static Dressing Get(byte id)
    {
        using var db = new ApplicationDbContext();
        var dressing=db.Dressings.FirstOrDefault(x=> x.Id==id);
        if (dressing == null)
            return new Dressing();
        return dressing;
    }

    public static Dressing Update(Dressing dressing)
    {
        using var db = new ApplicationDbContext();
        db.Dressings.Update(dressing);
        db.SaveChanges(true);
        return dressing;
    }
}
