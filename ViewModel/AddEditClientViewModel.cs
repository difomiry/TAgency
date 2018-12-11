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
    /// The AddEditClientViewModel view model.
    /// </summary>
    public class AddEditClientViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// An instance of client.
        /// </summary>
        private Client client;

        /// <summary>
        /// The window title.
        /// </summary>
        private string title;

        /// <summary>
        /// The client name.
        /// </summary>
        private string name;

        /// <summary>
        /// The client address.
        /// </summary>
        private string address;

        /// <summary>
        /// The client phone number.
        /// </summary>
        private string phone;

        /// <summary>
        /// The observable collection of discounts.
        /// </summary>
        private ObservableCollection<Discount> discounts;

        /// <summary>
        /// The "Save" command that saves a client to database.
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
        /// The client name.
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        /// <summary>
        /// The client address.
        /// </summary>
        public string Address
        {
            get => address;
            set => Set(ref address, value);
        }

        /// <summary>
        /// The client phone number.
        /// </summary>
        public string Phone
        {
            get => phone;
            set => Set(ref phone, value);
        }

        /// <summary>
        /// The observable collection of discounts.
        /// </summary>
        public ObservableCollection<Discount> Discounts
        {
            get => discounts;
            set => Set(ref discounts, value);
        }

        /// <summary>
        /// The "Save" command that saves a client to database.
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
        /// Initializes a new instance of the AddEditClientViewModel class.
        /// </summary>
        public AddEditClientViewModel()
        {
            Title = "Добавление клиента";
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the AddEditClientViewModel class.
        /// </summary>
        /// <param name="client">A client.</param>
        public AddEditClientViewModel(Client client)
        {
            this.client = client;
            Title = string.Format("Редактирование клиента #{0}", client.ClientID);
            Name = client.Name;
            Address = client.Address;
            Phone = client.Phone.ToString();
            Initialize();
        }

        /// <summary>
        /// Initializes comands and discounts.
        /// </summary>
        private void Initialize()
        {
            if (client != null)
            {
                Discounts = new ObservableCollection<Discount>(connection.Query<Discount>("SELECT *, (SELECT COUNT(*) FROM ClientDiscount WHERE ClientID = @ClientID AND DiscountID = Discount.DiscountID) IsChecked FROM Discount", client));
            }
            else
            {
                Discounts = new ObservableCollection<Discount>(connection.Query<Discount>("SELECT * FROM Discount"));
            }

            SaveCommand = new RelayCommand<Window>(SaveCommandExecute, SaveCommandCanExecute);
            CancelCommand = new RelayCommand<Window>(CancelCommandExecute);
        }

        /// <summary>
        /// Saves a client to database.
        /// </summary>
        /// <param name="window">The window that binded to this view model.</param>
        private void SaveCommandExecute(Window window)
        {
            try
            {
                if (Name.Trim() == string.Empty || Address.Trim() == string.Empty || Phone.Trim() == string.Empty)
                {
                    throw new Exception();
                }

                var phone = Convert.ToInt64(Phone);

                if (phone < 0)
                {
                    throw new Exception();
                }

                if (client == null)
                {
                    var client = new Client();
                    client.Name = Name;
                    client.Address = Address;
                    client.Phone = Convert.ToString(phone);

                    client.ClientID = connection.Query<int>("INSERT INTO Client (Name, Address, Phone) VALUES (@Name, @Address, @Phone); SELECT last_insert_rowid();", client).Single();

                    this.client = client;
                }
                else
                {
                    client.Name = Name;
                    client.Address = Address;
                    client.Phone = Convert.ToString(phone);

                    connection.Execute("UPDATE Client SET Name = @Name, Address = @Address, Phone = @Phone WHERE ClientID = @ClientID;", client);
                }

                foreach (var discount in Discounts)
                {
                    if (discount.IsChecked)
                    {
                        connection.Execute("INSERT INTO ClientDiscount (ClientID, DiscountID) VALUES (@ClientID, @DiscountID);", new { client.ClientID, discount.DiscountID });
                    }
                    else
                    {
                        connection.Execute("DELETE FROM ClientDiscount WHERE ClientID = @ClientID AND DiscountID = @DiscountID;", new { client.ClientID, discount.DiscountID });
                    }
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
                if (Name.Trim() == string.Empty || Address.Trim() == string.Empty || Phone.Trim() == string.Empty)
                {
                    throw new Exception();
                }

                var phone = Convert.ToInt64(Phone);

                if (phone < 0)
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
