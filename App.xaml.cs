using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Services.IRepository;
using WoundClinic_WPF.UI;
using WoundClinic_WPF.UI.UserControls;

namespace WoundClinic_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WoundCareDb;Trusted_Connection=True;"));
            // سرویس‌ها را اینجا ثبت کن
            services.AddTransient<IPatientRepository,PatientRepository>();
            services.AddTransient<IPersonRepository,PersonRepository>();
            services.AddTransient<IDressingRepository, DressingRepository>();
            services.AddTransient<IDressingCareRepository, DressingCareRepository>();

            // اگر پنجره ها هم نیاز به تزریق دارند:
            services.AddSingleton<MainWindow>();
            services.AddSingleton<winCares>();

            // اگر UserControl وابستگی دارد:
            services.AddTransient<DressingCareUserControl>();
            services.AddTransient<CareRegisterUserControl>();
            services.AddTransient<winPatient>();
        }
    }

}
