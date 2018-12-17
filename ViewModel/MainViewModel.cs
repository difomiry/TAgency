using Dapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace TAgency.ViewModel
{
    /// <summary>
    /// The Main view model.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// The path to database.
        /// </summary>
        private string pathToDatabase;

        /// <summary>
        /// The "New" command that opens SaveFileDialog for new database creation.
        /// </summary>
        private ICommand newCommand;

        /// <summary>
        /// The "Open" command that opens OpenFileDialog for new database selection.
        /// </summary>
        private ICommand openCommand;

        /// <summary>
        /// The "Save" command that ppens SaveFileDialog for current database saving.
        /// </summary>
        private ICommand saveCommand;

        /// <summary>
        /// The "Exit" command that exits the applicaton.
        /// </summary>
        private ICommand exitCommand;

        /// <summary>
        /// The "About" command that shows information about the application.
        /// </summary>
        private ICommand aboutCommand;

        #endregion

        #region Properties

        /// <summary>
        /// The path to database.
        /// </summary>
        public string PathToDatabase
        {
            get => pathToDatabase;
            set => Set(ref pathToDatabase, value);
        }

        /// <summary>
        /// The "New" command that opens SaveFileDialog for new database creation.
        /// </summary>
        public ICommand NewCommand
        {
            get => newCommand;
            set => Set(ref newCommand, value);
        }

        /// <summary>
        /// The "Open" command that opens OpenFileDialog for new database selection.
        /// </summary>
        public ICommand OpenCommand
        {
            get => openCommand;
            set => Set(ref openCommand, value);
        }

        /// <summary>
        /// The "Save" command that ppens SaveFileDialog for current database saving.
        /// </summary>
        public ICommand SaveCommand
        {
            get => saveCommand;
            set => Set(ref saveCommand, value);
        }

        /// <summary>
        /// The "Exit" command that exits the applicaton.
        /// </summary>
        public ICommand ExitCommand
        {
            get => exitCommand;
            set => Set(ref exitCommand, value);
        }

        /// <summary>
        /// The "About" command that shows information about the application.
        /// </summary>
        public ICommand AboutCommand
        {
            get => aboutCommand;
            set => Set(ref aboutCommand, value);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            PathToDatabase = Properties.Settings.Default.PathToDatabase;

            NewCommand = new RelayCommand(NewCommandExecute);
            OpenCommand = new RelayCommand(OpenCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            ExitCommand = new RelayCommand(ExitCommandExecute);
            AboutCommand = new RelayCommand(AboutCommandExecute);
        }

        /// <summary>
        /// Opens SaveFileDialog for new database creation.
        /// </summary>
        private void NewCommandExecute()
        {
            var dialog = new SaveFileDialog();

            dialog.Title = "Новая база данных";
            dialog.DefaultExt = "db";
            dialog.Filter = "SQLite (*.db;*.sqlite;*.sqlite3)|*.db;*.sqlite;*.sqlite3";

            if (dialog.ShowDialog() ?? false)
            {
                Properties.Settings.Default.PathToDatabase = dialog.FileName;
                Properties.Settings.Default.Save();
                SimpleIoc.Default.Unregister<SQLiteConnection>();
                SimpleIoc.Default.Register(() =>
                {
                    var connection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", dialog.FileName));
                    connection.Open();
                    connection.Execute(Properties.Resources.Schema);
                    return connection;
                });
                PathToDatabase = dialog.FileName;
                SimpleIoc.Default.GetInstance<ClientListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<DiscountListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<TourListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<HotelListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<VoucherListViewModel>().ReloadData();
            }
        }

        /// <summary>
        /// Opens OpenFileDialog for new database selection.
        /// </summary>
        private void OpenCommandExecute()
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "Открыть базу данных";
            dialog.DefaultExt = "db";
            dialog.Filter = "SQLite (*.db;*.sqlite;*.sqlite3)|*.db;*.sqlite;*.sqlite3";

            if (dialog.ShowDialog() ?? false)
            {
                Properties.Settings.Default.PathToDatabase = dialog.FileName;
                Properties.Settings.Default.Save();
                SimpleIoc.Default.Unregister<SQLiteConnection>();
                SimpleIoc.Default.Register(() =>
                {
                    var connection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", dialog.FileName));
                    connection.Open();
                    connection.Execute(Properties.Resources.Schema);
                    return connection;
                });
                PathToDatabase = dialog.FileName;
                SimpleIoc.Default.GetInstance<ClientListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<DiscountListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<TourListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<HotelListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<VoucherListViewModel>().ReloadData();
            }
        }

        /// <summary>
        /// Opens SaveFileDialog for current database saving.
        /// </summary>
        private void SaveCommandExecute()
        {
            var dialog = new SaveFileDialog();

            dialog.Title = "Сохранить базу данных";
            dialog.DefaultExt = "db";
            dialog.Filter = "SQLite (*.db;*.sqlite;*.sqlite3)|*.db;*.sqlite;*.sqlite3";

            if (dialog.ShowDialog() ?? false)
            {
                var oldPath = Properties.Settings.Default.PathToDatabase;
                File.Copy(oldPath, dialog.FileName, true);
                Properties.Settings.Default.PathToDatabase = dialog.FileName;
                Properties.Settings.Default.Save();
                SimpleIoc.Default.Unregister<SQLiteConnection>();
                SimpleIoc.Default.Register(() =>
                {
                    var connection = new SQLiteConnection(string.Format("Data Source={0}; Version=3", dialog.FileName));
                    connection.Open();
                    connection.Execute(Properties.Resources.Schema);
                    return connection;
                });
                PathToDatabase = dialog.FileName;
                SimpleIoc.Default.GetInstance<ClientListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<DiscountListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<TourListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<HotelListViewModel>().ReloadData();
                SimpleIoc.Default.GetInstance<VoucherListViewModel>().ReloadData();
            }
        }

        /// <summary>
        /// Exits the applicaton.
        /// </summary>
        private void ExitCommandExecute()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Shows information about the application.
        /// </summary>
        private void AboutCommandExecute()
        {
            MessageBox.Show("Разработано в качестве курсового проекта по дисциплине \"Управление данными\".", "О программе");
        }
    }
}