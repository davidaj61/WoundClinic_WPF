using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Input;
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
            EventManager.RegisterClassHandler(typeof(FrameworkElement),
            Keyboard.PreviewKeyDownEvent,
            new KeyEventHandler(HandleEnterKeyPress), true);
            using (var db = new ApplicationDbContext())
            {
                db.Database.Migrate();
            }
        }
        private void HandleEnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var element = sender as FrameworkElement;

                // بعضی کنترل‌های HandyControl مثل NumericBox در واقع Controlهایی با قالب پیچیده‌اند
                e.Handled = true;
                element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
