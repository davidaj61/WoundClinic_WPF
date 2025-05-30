using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services
{
    public static class WoundCareRepository
    {
        public static bool Create(WoundCare woundCare)
        {
            using var db = new ApplicationDbContext();
            db.WoundCares.Add(woundCare);
            return db.SaveChanges() > 0;
        }

        public static void Delete(WoundCare wc)
        {
            using var db = new ApplicationDbContext();
            if (wc != null)
                db.WoundCares.Remove(wc);
            db.SaveChanges();
        }
    }
}

