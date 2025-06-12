using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WoundClinic_WPF.Data;
using WoundClinic_WPF.Services;
using WoundClinic_WPF.UI;
using WoundClinic_WPF.UI.UserControls;
using Application = System.Windows.Application;


namespace WoundClinic_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // تنظیم زبان فارسی برای HandyControl
            HandyControl.Properties.Langs.Lang.Culture = new System.Globalization.CultureInfo("fa-IR");


            using (var db = new ApplicationDbContext()) 
            {
                db.Database.Migrate();
            }
            

            
        } 
    }
}
