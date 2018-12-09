using Dapper;
using GalaSoft.MvvmLight.Ioc;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace TAgency
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!File.Exists(TAgency.Properties.Settings.Default.PathToDatabase))
            {
                TAgency.Properties.Settings.Default.PathToDatabase = "TAgency.db";
                TAgency.Properties.Settings.Default.Save();
            }

            SimpleIoc.Default.Register(() =>
            {
                var connection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", TAgency.Properties.Settings.Default.PathToDatabase));
                connection.Open();
                connection.Execute(TAgency.Properties.Resources.Schema);
                return connection;
            });

            base.OnStartup(e);
        }
    }
}
