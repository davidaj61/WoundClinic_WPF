using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.Services.IRepository;
using WoundClinic_WPF.UI;
using WoundClinic_WPF.UI.UserControls;
using FormApplication = System.Windows.Forms;

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
            var login = ServiceProvider.GetRequiredService<winLogin>();
            if (login != null)
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                    mainWindow.Show();

                }
            }
            else
            {
                FormApplication.MessageBox.Show("خطا در بارگذاری پنجره ورود");
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WoundCareDb;Trusted_Connection=True;"));
            // سرویس‌ها را اینجا ثبت کن
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IDressingRepository, DressingRepository>();
            services.AddTransient<IDressingCareRepository, DressingCareRepository>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();

            // اگر پنجره ها هم نیاز به تزریق دارند:
            services.AddSingleton<MainWindow>();
            services.AddSingleton<winCares>();

            // اگر UserControl وابستگی دارد:
            services.AddTransient<DressingCareUserControl>();
            services.AddTransient<CareRegisterUserControl>();
            services.AddTransient<winPatient>();
            services.AddTransient<winLogin>();
        }
    }

}
