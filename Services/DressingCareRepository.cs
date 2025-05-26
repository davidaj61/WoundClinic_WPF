using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services;

public static class DressingCareRepository
{
    public static async Task<DressingCare> CreateAsync(DressingCare dressingCare)
    {
        using var db = new ApplicationDbContext();
        await db.DressingCares.AddAsync(dressingCare);
        await db.SaveChangesAsync();
        return dressingCare;
    }

    public static async Task<bool> DeleteAsync(DressingCare dressingCare)
    {
        using var db = new ApplicationDbContext();
        db.DressingCares.Remove(dressingCare);
        await db.SaveChangesAsync();
        return true;
    }

    public static async Task<IEnumerable<DressingCare>> GetAllAsync()
    {
        using var db = new ApplicationDbContext();
        return await db.DressingCares.ToListAsync();
    }

    public static async Task<DressingCare> GetAsync(byte id)
    {
        using var db = new ApplicationDbContext();
        var dressingCare = await db.DressingCares.FirstOrDefaultAsync(x => x.Id == id);
        if (dressingCare == null)
            return new DressingCare();
        return dressingCare;
    }

    public static async Task<DressingCare> UpdateAsync(DressingCare dressingCare)
    {
        using var db = new ApplicationDbContext();
        db.DressingCares.Update(dressingCare);
        await db.SaveChangesAsync(true);
        return dressingCare;
    }
}
