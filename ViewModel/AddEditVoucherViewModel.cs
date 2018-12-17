using Dapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TAgency.Model;

namespace TAgency.ViewModel
{
    /// <summary>
    /// The AddEditVoucherViewModel view model.
    /// </summary>
    public class AddEditVoucherViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// An instance of voucher.
        /// </summary>
        private Voucher voucher;

        /// <summary>
        /// The window title.
        /// </summary>
        private string title;

        /// <summary>
        /// The selected client index.
        /// </summary>
        private int selectedClientIndex;

        /// <summary>
        /// An selected tour index.
        /// </summary>
        private int selectedTourIndex;

        /// <summary>
        /// The number of persons.
        /// </summary>
        private string count;

        /// <summary>
        /// The observable collection of clients.
        /// </summary>
        private ObservableCollection<Client> clients;

        /// <summary>
        /// The observable collection of tours.
        /// </summary>
        private ObservableCollection<Tour> tours;

        /// <summary>
        /// The "Save" command that saves a hotel to database.
        /// </summary>
        private ICommand saveCommand;

        /// <summary>
        /// The "Cancel" command that closes the window without changes saving.
        /// </summary>
        private ICommand cancelCommand;

        #endregion

        #region Properties

        /// <summary>
        /// The window title.
        /// </summary>
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        /// <summary>
        /// The selected client index.
        /// </summary>
        public int SelectedClientIndex
        {
            get => selectedClientIndex;
            set => Set(ref selectedClientIndex, value);
        }

        /// <summary>
        /// The selected tour index.
        /// </summary>
        public int SelectedTourIndex
        {
            get => selectedTourIndex;
            set => Set(ref selectedTourIndex, value);
        }

        /// <summary>
        /// The number of persons.
        /// </summary>
        public string Count
        {
            get => count;
            set => Set(ref count, value);
        }

        /// <summary>
        /// The observable collection of clients.
        /// </summary>
        public ObservableCollection<Client> Clients
        {
            get => clients;
            set => Set(ref clients, value);
        }

        /// <summary>
        /// The observable collection of tours.
        /// </summary>
        public ObservableCollection<Tour> Tours
        {
            get => tours;
            set => Set(ref tours, value);
        }
        
        /// <summary>
        /// The "Save" command that saves a hotel to database.
        /// </summary>
        public ICommand SaveCommand
        {
            get => saveCommand;
            set => Set(ref saveCommand, value);
        }

        /// <summary>
        /// The "Cancel" command that closes the window without changes saving.
        /// </summary>
        public ICommand CancelCommand
        {
            get => cancelCommand;
            set => Set(ref cancelCommand, value);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the AddEditVoucherViewModel class.
        /// </summary>
        public AddEditVoucherViewModel()
        {
            Title = "Добавление ваучера";
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the AddEditVoucherViewModel class.
        /// </summary>
        /// <param name="voucher">A voucher.</param>
        public AddEditVoucherViewModel(Voucher voucher)
        {
            this.voucher = voucher;
            Title = string.Format("Редактирование ваучера #{0}", voucher.VoucherID);
            Count = voucher.Count.ToString();
            Initialize();
        }

        /// <summary>
        /// Initializes comands and collections.
        /// </summary>
        private void Initialize()
        {
            Clients = new ObservableCollection<Client>(connection.Query<Client>("SELECT * FROM Client"));
            Tours = new ObservableCollection<Tour>(connection.Query<Tour>("SELECT * FROM TourWithDurationAndCost"));

            if (voucher != null)
            {
                SelectedClientIndex = Clients.AsList().FindIndex((c) => c.ClientID == voucher.ClientID);
                SelectedTourIndex = Tours.AsList().FindIndex((t) => t.TourID == voucher.TourID);
            }

            SaveCommand = new RelayCommand<Window>(SaveCommandExecute, SaveCommandCanExecute);
            CancelCommand = new RelayCommand<Window>(CancelCommandExecute);
        }
        /// <summary>
        /// Saves a hotel to database.
        /// </summary>
        /// <param name="window">The window that binded to this view model.</param>
        private void SaveCommandExecute(Window window)
        {
            try
            {
                if (Count.Trim() == string.Empty)
                {
                    throw new Exception();
                }

                var count = Convert.ToInt32(Count);

                if (count < 1)
                {
                    throw new Exception();
                }

                if (voucher == null)
                {
                    voucher = new Voucher();
                    voucher.ClientID = Clients[SelectedClientIndex].ClientID;
                    voucher.TourID = Tours[SelectedTourIndex].TourID;
                    voucher.Count = count;

                    connection.Execute("INSERT INTO Voucher (ClientID, TourID, Count) VALUES (@ClientID, @TourID, @Count);", voucher);
                }
                else
                {
                    voucher.ClientID = Clients[SelectedClientIndex].ClientID;
                    voucher.TourID = Tours[SelectedTourIndex].TourID;
                    voucher.Count = count;

                    connection.Execute("UPDATE Voucher SET ClientID = @ClientID, TourID = @TourID, Count = @Count WHERE VoucherID = @VoucherID;", voucher);
                }

                window.DialogResult = true;
                window.Close();
            }
            catch { }
        }

        /// <summary>
        /// Verifies that the command can execute.
        /// </summary>
        /// <param name="window">A window that binded to this view model.</param>
        /// <returns>Can the command be executed?</returns>
        private bool SaveCommandCanExecute(Window window)
        {
            try
            {
                if (Count.Trim() == string.Empty)
                {
                    throw new Exception();
                }

                var count = Convert.ToInt32(Count);

                if (count < 1)
                {
                    throw new Exception();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Closes the window without changes saving.
        /// </summary>
        /// <param name="window">The window that binded to this view model.</param>
        private void CancelCommandExecute(Window window)
        {
            window.Close();
        }
    }
}
