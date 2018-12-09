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
    /// The TourList view model.
    /// </summary>
    public class TourListViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// The observable collection of tours.
        /// </summary>
        private ObservableCollection<Tour> tours;

        /// <summary>
        /// The "Edit" command that opens the window for tour editing.
        /// </summary>
        private ICommand editCommand;

        /// <summary>
        /// The "Delete" command that opens the confirmation window for tour deleting.
        /// </summary>
        private ICommand deleteCommand;

        /// <summary>
        /// The "Add" command that opens the window for tour adding.
        /// </summary>
        private ICommand addCommand;

        #endregion

        #region Properties

        /// <summary>
        /// The observable collection of tours.
        /// </summary>
        public ObservableCollection<Tour> Tours
        {
            get => tours;
            set => Set(ref tours, value);
        }

        /// <summary>
        /// The "Edit" command that opens the window for tour editing.
        /// </summary>
        public ICommand EditCommand
        {
            get => editCommand;
            set => Set(ref editCommand, value);
        }

        /// <summary>
        /// The "Delete" command that opens the confirmation window for tour deleting.
        /// </summary>
        public ICommand DeleteCommand
        {
            get => deleteCommand;
            set => Set(ref deleteCommand, value);
        }

        /// <summary>
        /// The "Add" command that opens the window for tour adding.
        /// </summary>
        public ICommand AddCommand
        {
            get => addCommand;
            set => Set(ref addCommand, value);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the TourListViewModel class.
        /// </summary>
        public TourListViewModel()
        {
            ReloadData();

            EditCommand = new RelayCommand<Tour>(EditCommandExecute);
            DeleteCommand = new RelayCommand<Tour>(DeleteCommandExecute);
            AddCommand = new RelayCommand(AddCommandExecute);
        }

        /// <summary>
        /// Reloads the observable collection of tours.
        /// </summary>
        public void ReloadData()
        {
            Tours = new ObservableCollection<Tour>(connection.Query<Tour>("SELECT * FROM Tour"));
        }

        /// <summary>
        /// Opens the window for tour editing.
        /// </summary>
        /// <param name="tour">A tour entity.</param>
        private void EditCommandExecute(Tour tour)
        {
        }

        /// <summary>
        /// Opens the confirmation window for tour deleting.
        /// </summary>
        /// <param name="tour">A tour entity.</param>
        private void DeleteCommandExecute(Tour tour)
        {
            if (MessageBox.Show("Вы действительно хотите удалить тур?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                connection.Execute("DELETE FROM Tour WHERE TourID = @TourID", tour);
                ReloadData();
            }
        }

        /// <summary>
        /// Opens the window for tour adding.
        /// </summary>
        private void AddCommandExecute()
        {
        }
    }
}
