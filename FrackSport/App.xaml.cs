
using System.Windows;
using SQLitePCL;

namespace FrackSport
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            SQLitePCL.Batteries.Init();
            base.OnStartup(e);

        }
    }
}
