using Dapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;
using TAgency.Model;

namespace TAgency.ViewModel
{
    /// <summary>
    /// The ClientList view model.
    /// </summary>
    public class ClientListViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// The observable collection of clients.
        /// </summary>
        private ObservableCollection<Client> clients;

        /// <summary>
        /// The "Edit" command that opens the window for client editing.
        /// </summary>
        private ICommand editCommand;

        /// <summary>
        /// The "Delete" command that opens the confirmation window for client deleting.
        /// </summary>
        private ICommand deleteCommand;

        /// <summary>
        /// The "Add" command that opens the window for client adding.
        /// </summary>
        private ICommand addCommand;

        #endregion

        #region Properties

        /// <summary>
        /// The observable collection of clients.
        /// </summary>
        public ObservableCollection<Client> Clients
        {
            get => clients;
            set => Set(ref clients, value);
        }

        /// <summary>
        /// The "Edit" command that opens the window for client editing.
        /// </summary>
        public ICommand EditCommand
        {
            get => editCommand;
            set => Set(ref editCommand, value);
        }

        /// <summary>
        /// The "Delete" command that opens the confirmation window for client deleting.
        /// </summary>
        public ICommand DeleteCommand
        {
            get => deleteCommand;
            set => Set(ref deleteCommand, value);
        }

        /// <summary>
        /// The "Add" command that opens the window for client adding.
        /// </summary>
        public ICommand AddCommand
        {
            get => addCommand;
            set => Set(ref addCommand, value);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the ClientListViewModel class.
        /// </summary>
        public ClientListViewModel()
        {
            ReloadData();

            EditCommand = new RelayCommand<Client>(EditCommandExecute);
            DeleteCommand = new RelayCommand<Client>(DeleteCommandExecute);
            AddCommand = new RelayCommand(AddCommandExecute);
        }

        /// <summary>
        /// Reloads the observable collection of clients.
        /// </summary>
        public void ReloadData()
        {
            Clients = new ObservableCollection<Client>(connection.Query<Client>("SELECT * FROM Client"));
        }

        /// <summary>
        /// Opens the window for client editing.
        /// </summary>
        /// <param name="client">A client entity.</param>
        private void EditCommandExecute(Client client)
        {
        }

        /// <summary>
        /// Opens the confirmation window for client deleting.
        /// </summary>
        /// <param name="client">A client entity.</param>
        private void DeleteCommandExecute(Client client)
        {
            if (MessageBox.Show("Вы действительно хотите удалить клиента?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                connection.Execute("DELETE FROM Client WHERE ClientID = @ClientID", client);
                ReloadData();
            }
        }

        /// <summary>
        /// Opens the window for client adding.
        /// </summary>
        private void AddCommandExecute()
        {
        }
    }
}
