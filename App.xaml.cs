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

            HandyControl.Controls.MessageBox.CancelContentProperty.OverrideMetadata(
                typeof(HandyControl.Controls.MessageBox),
                new FrameworkPropertyMetadata("انصراف"));

            HandyControl.Controls.MessageBox.ConfirmContentProperty.OverrideMetadata(
                typeof(HandyControl.Controls.MessageBox),
                new FrameworkPropertyMetadata("تأیید"));

            HandyControl.Controls.MessageBox.YesContentProperty.OverrideMetadata(
                typeof(HandyControl.Controls.MessageBox),
                new FrameworkPropertyMetadata("بله"));

            HandyControl.Controls.MessageBox.NoContentProperty.OverrideMetadata(
                typeof(HandyControl.Controls.MessageBox),
                new FrameworkPropertyMetadata("خیر"));
        }
    }
}
