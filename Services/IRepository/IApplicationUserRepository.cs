using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services.IRepository
{
    public interface IApplicationUserRepository
    {
        public void Create(ApplicationUser user, string password);
        public ApplicationUser GetByNationalCode(long nationalCode);
        public bool CheckPassword(ApplicationUser user, string password);
        public void SetUserLastLogin(ApplicationUser user);
    }
}
