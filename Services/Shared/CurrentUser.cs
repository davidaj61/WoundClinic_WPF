using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services.Shared
{
    public static class CurrentUser
    {
        public static ApplicationUser? User { get; set; }
    }
}
