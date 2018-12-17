using Dapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;
using TAgency.Model;
using TAgency.View;

namespace TAgency.ViewModel
{
    /// <summary>
    /// The VoucherList view model.
    /// </summary>
    public class VoucherListViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// The observable collection of vouchers.
        /// </summary>
        private ObservableCollection<Voucher> vouchers;

        /// <summary>
        /// The "Edit" command that opens the window for voucher editing.
        /// </summary>
        private ICommand editCommand;

        /// <summary>
        /// The "Delete" command that opens the confirmation window for voucher deleting.
        /// </summary>
        private ICommand deleteCommand;

        /// <summary>
        /// The "Add" command that opens the window for voucher adding.
        /// </summary>
        private ICommand addCommand;

        #endregion

        #region Properties

        /// <summary>
        /// The observable collection of vouchers.
        /// </summary>
        public ObservableCollection<Voucher> Vouchers
        {
            get => vouchers;
            set => Set(ref vouchers, value);
        }

        /// <summary>
        /// The "Edit" command that opens the window for voucher editing.
        /// </summary>
        public ICommand EditCommand
        {
            get => editCommand;
            set => Set(ref editCommand, value);
        }

        /// <summary>
        /// The "Delete" command that opens the confirmation window for voucher deleting.
        /// </summary>
        public ICommand DeleteCommand
        {
            get => deleteCommand;
            set => Set(ref deleteCommand, value);
        }

        /// <summary>
        /// The "Add" command that opens the window for voucher adding.
        /// </summary>
        public ICommand AddCommand
        {
            get => addCommand;
            set => Set(ref addCommand, value);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the VoucherListViewModel class.
        /// </summary>
        public VoucherListViewModel()
        {
            ReloadData();

            EditCommand = new RelayCommand<Voucher>(EditCommandExecute);
            DeleteCommand = new RelayCommand<Voucher>(DeleteCommandExecute);
            AddCommand = new RelayCommand(AddCommandExecute);
        }

        /// <summary>
        /// Reloads the observable collection of vouchers.
        /// </summary>
        public void ReloadData()
        {
            Vouchers = new ObservableCollection<Voucher>(connection.Query<Voucher>("SELECT * FROM Voucher"));
        }

        /// <summary>
        /// Opens the window for voucher editing.
        /// </summary>
        /// <param name="voucher">A voucher entity.</param>
        private void EditCommandExecute(Voucher voucher)
        {
            var dialog = new AddEditVoucherWindow(new AddEditVoucherViewModel(voucher));
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (dialog.ShowDialog() ?? false)
            {
                ReloadData();
            }
        }

        /// <summary>
        /// Opens the confirmation window for voucher deleting.
        /// </summary>
        /// <param name="voucher">A voucher entity.</param>
        private void DeleteCommandExecute(Voucher voucher)
        {
            if (MessageBox.Show("Вы действительно хотите удалить ваучер?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                connection.Execute("DELETE FROM Voucher WHERE VoucherID = @VoucherID", voucher);
                ReloadData();
            }
        }

        /// <summary>
        /// Opens the window for voucher adding.
        /// </summary>
        private void AddCommandExecute()
        {
            var dialog = new AddEditVoucherWindow(new AddEditVoucherViewModel());
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (dialog.ShowDialog() ?? false)
            {
                ReloadData();
            }
        }
    }
}
