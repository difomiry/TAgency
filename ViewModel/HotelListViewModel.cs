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
    /// The HotelList view model.
    /// </summary>
    public class HotelListViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// The observable collection of hotels.
        /// </summary>
        private ObservableCollection<Hotel> hotels;

        /// <summary>
        /// The "Edit" command that opens the window for hotel editing.
        /// </summary>
        private ICommand editCommand;

        /// <summary>
        /// The "Delete" command that opens the confirmation window for hotel deleting.
        /// </summary>
        private ICommand deleteCommand;

        /// <summary>
        /// The "Add" command that opens the window for hotel adding.
        /// </summary>
        private ICommand addCommand;

        #endregion

        #region Properties

        /// <summary>
        /// The observable collection of hotels.
        /// </summary>
        public ObservableCollection<Hotel> Hotels
        {
            get => hotels;
            set => Set(ref hotels, value);
        }

        /// <summary>
        /// The "Edit" command that opens the window for hotel editing.
        /// </summary>
        public ICommand EditCommand
        {
            get => editCommand;
            set => Set(ref editCommand, value);
        }

        /// <summary>
        /// The "Delete" command that opens the confirmation window for hotel deleting.
        /// </summary>
        public ICommand DeleteCommand
        {
            get => deleteCommand;
            set => Set(ref deleteCommand, value);
        }

        /// <summary>
        /// The "Add" command that opens the window for hotel adding.
        /// </summary>
        public ICommand AddCommand
        {
            get => addCommand;
            set => Set(ref addCommand, value);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the HotelListViewModel class.
        /// </summary>
        public HotelListViewModel()
        {
            ReloadData();

            EditCommand = new RelayCommand<Hotel>(EditCommandExecute);
            DeleteCommand = new RelayCommand<Hotel>(DeleteCommandExecute);
            AddCommand = new RelayCommand(AddCommandExecute);
        }

        /// <summary>
        /// Reloads the observable collection of hotels.
        /// </summary>
        public void ReloadData()
        {
            Hotels = new ObservableCollection<Hotel>(connection.Query<Hotel>("SELECT * FROM Hotel"));
        }

        /// <summary>
        /// Opens the window for hotel editing.
        /// </summary>
        /// <param name="hotel">A hotel entity.</param>
        private void EditCommandExecute(Hotel hotel)
        {
        }

        /// <summary>
        /// Opens the confirmation window for hotel deleting.
        /// </summary>
        /// <param name="hotel">A hotel entity.</param>
        private void DeleteCommandExecute(Hotel hotel)
        {
            if (MessageBox.Show("Вы действительно хотите удалить отель?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                connection.Execute("DELETE FROM Hotel WHERE HotelID = @HotelID", hotel);
                ReloadData();
            }
        }

        /// <summary>
        /// Opens the window for hotel adding.
        /// </summary>
        private void AddCommandExecute()
        {
        }
    }
}
