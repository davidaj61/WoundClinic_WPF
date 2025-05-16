using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services.IRepository;

public interface IDressingRepository
{
    public Task<Dressing> CreateAsync(Dressing dressing);

    public Task<Dressing> UpdateAsync(Dressing dressing);

    public Task<bool> DeleteAsync(Dressing dressing);

    public Task<Dressing> GetAsync(byte id);

    public Task<IEnumerable<Dressing>> GetAllAsync();
}
