using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoundClinic_WPF.Models;

namespace WoundClinic_WPF.Services.IRepository;

public interface IDressingRepository
{
    public Dressing Create(Dressing dressing);

    public Dressing Update(Dressing dressing);

    public bool Delete(Dressing dressing);

    public Dressing Get(byte id);

    public IEnumerable<Dressing> GetAll();
}
