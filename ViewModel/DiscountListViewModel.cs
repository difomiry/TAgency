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
    /// The DiscountList view model.
    /// </summary>
    public class DiscountListViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// The observable collection of discounts.
        /// </summary>
        private ObservableCollection<Discount> discounts;

        /// <summary>
        /// The "Edit" command that opens the window for discount editing.
        /// </summary>
        private ICommand editCommand;

        /// <summary>
        /// The "Delete" command that opens the confirmation window for discount deleting.
        /// </summary>
        private ICommand deleteCommand;

        /// <summary>
        /// The "Add" command that opens the window for discount adding.
        /// </summary>
        private ICommand addCommand;

        #endregion

        #region Properties

        /// <summary>
        /// The observable collection of discounts.
        /// </summary>
        public ObservableCollection<Discount> Discounts
        {
            get => discounts;
            set => Set(ref discounts, value);
        }

        /// <summary>
        /// The "Edit" command that opens the window for discount editing.
        /// </summary>
        public ICommand EditCommand
        {
            get => editCommand;
            set => Set(ref editCommand, value);
        }

        /// <summary>
        /// The "Delete" command that opens the confirmation window for discount deleting.
        /// </summary>
        public ICommand DeleteCommand
        {
            get => deleteCommand;
            set => Set(ref deleteCommand, value);
        }

        /// <summary>
        /// The "Add" command that opens the window for discount adding.
        /// </summary>
        public ICommand AddCommand
        {
            get => addCommand;
            set => Set(ref addCommand, value);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the DiscountListViewModel class.
        /// </summary>
        public DiscountListViewModel()
        {
            ReloadData();

            EditCommand = new RelayCommand<Discount>(EditCommandExecute);
            DeleteCommand = new RelayCommand<Discount>(DeleteCommandExecute);
            AddCommand = new RelayCommand(AddCommandExecute);
        }

        /// <summary>
        /// Reloads the observable collection of discounts.
        /// </summary>
        public void ReloadData()
        {
            Discounts = new ObservableCollection<Discount>(connection.Query<Discount>("SELECT * FROM Discount"));
        }

        /// <summary>
        /// Opens the window for discount editing.
        /// </summary>
        /// <param name="discount">A discount entity.</param>
        private void EditCommandExecute(Discount discount)
        {
        }

        /// <summary>
        /// Opens the confirmation window for discount deleting.
        /// </summary>
        /// <param name="discount">A discount entity.</param>
        private void DeleteCommandExecute(Discount discount)
        {
            if (MessageBox.Show("Вы действительно хотите удалить скидку?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                connection.Execute("DELETE FROM Discount WHERE DiscountID = @DiscountID", discount);
                ReloadData();
            }
        }

        /// <summary>
        /// Opens the window for discount adding.
        /// </summary>
        private void AddCommandExecute()
        {
        }
    }
}
