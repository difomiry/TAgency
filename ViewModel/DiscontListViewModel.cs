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
    /// The DiscontList view model.
    /// </summary>
    public class DiscontListViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// The observable collection of disconts.
        /// </summary>
        private ObservableCollection<Discont> disconts;

        /// <summary>
        /// The "Edit" command that opens the window for discont editing.
        /// </summary>
        private ICommand editCommand;

        /// <summary>
        /// The "Delete" command that opens the confirmation window for discont deleting.
        /// </summary>
        private ICommand deleteCommand;

        /// <summary>
        /// The "Add" command that opens the window for discont adding.
        /// </summary>
        private ICommand addCommand;

        #endregion

        #region Properties

        /// <summary>
        /// The observable collection of disconts.
        /// </summary>
        public ObservableCollection<Discont> Disconts
        {
            get => disconts;
            set => Set(ref disconts, value);
        }

        /// <summary>
        /// The "Edit" command that opens the window for discont editing.
        /// </summary>
        public ICommand EditCommand
        {
            get => editCommand;
            set => Set(ref editCommand, value);
        }

        /// <summary>
        /// The "Delete" command that opens the confirmation window for discont deleting.
        /// </summary>
        public ICommand DeleteCommand
        {
            get => deleteCommand;
            set => Set(ref deleteCommand, value);
        }

        /// <summary>
        /// The "Add" command that opens the window for discont adding.
        /// </summary>
        public ICommand AddCommand
        {
            get => addCommand;
            set => Set(ref addCommand, value);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the DiscontListViewModel class.
        /// </summary>
        public DiscontListViewModel()
        {
            ReloadData();

            EditCommand = new RelayCommand<Discont>(EditCommandExecute);
            DeleteCommand = new RelayCommand<Discont>(DeleteCommandExecute);
            AddCommand = new RelayCommand(AddCommandExecute);
        }

        /// <summary>
        /// Reloads the observable collection of disconts.
        /// </summary>
        public void ReloadData()
        {
            Disconts = new ObservableCollection<Discont>(connection.Query<Discont>("SELECT * FROM Discont"));
        }

        /// <summary>
        /// Opens the window for discont editing.
        /// </summary>
        /// <param name="discont">A discont entity.</param>
        private void EditCommandExecute(Discont discont)
        {
        }

        /// <summary>
        /// Opens the confirmation window for discont deleting.
        /// </summary>
        /// <param name="discont">A discont entity.</param>
        private void DeleteCommandExecute(Discont discont)
        {
            if (MessageBox.Show("Вы действительно хотите удалить скидку?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                connection.Execute("DELETE FROM Discont WHERE DiscontID = @DiscontID", discont);
                ReloadData();
            }
        }

        /// <summary>
        /// Opens the window for discont adding.
        /// </summary>
        private void AddCommandExecute()
        {
        }
    }
}
