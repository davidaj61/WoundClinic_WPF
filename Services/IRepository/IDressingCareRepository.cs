using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services.IRepository;

public interface IDressingCareRepository
{
    public Task<DressingCare> CreateAsync(DressingCare dressing);

    public Task<DressingCare> UpdateAsync(DressingCare dressing);

    public Task<bool> DeleteAsync(DressingCare dressing);

    public Task<DressingCare> GetAsync(byte id);

    public Task<IEnumerable<DressingCare>> GetAllAsync();
}
