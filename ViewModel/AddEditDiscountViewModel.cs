using Dapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;
using TAgency.Model;

namespace TAgency.ViewModel
{
    /// <summary>
    /// The AddEditDiscountViewModel view model.
    /// </summary>
    public class AddEditDiscountViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// An instance of discount.
        /// </summary>
        private Discount discount;

        /// <summary>
        /// The window title.
        /// </summary>
        private string title;

        /// <summary>
        /// The client name.
        /// </summary>
        private string name;

        /// <summary>
        /// The discount percent.
        /// </summary>
        private string percent;

        /// <summary>
        /// The "Save" command that saves a discount to database.
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
        /// The discount name.
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        
        /// <summary>
        /// The discount percent.
        /// </summary>
        public string Percent
        {
            get => percent;
            set => Set(ref percent, value);
        }

        /// <summary>
        /// The "Save" command that saves a discount to database.
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
        /// Initializes a new instance of the AddEditDiscountViewModel class.
        /// </summary>
        public AddEditDiscountViewModel()
        {
            Title = "Добавление скидки";
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the AddEditDiscountViewModel class.
        /// </summary>
        /// <param name="discont">A discount.</param>
        public AddEditDiscountViewModel(Discount discount)
        {
            this.discount = discount;
            Title = string.Format("Редактирование скидки #{0}", discount.DiscountID);
            Name = discount.Name;
            Percent = discount.Percent.ToString();
            Initialize();
        }

        /// <summary>
        /// Initializes comands.
        /// </summary>
        private void Initialize()
        {
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
                if (Name.Trim() == string.Empty || Percent.Trim() == string.Empty)
                {
                    throw new Exception();
                }

                var percent = Convert.ToSingle(Percent);

                if (percent < 0 || percent > 1)
                {
                    throw new Exception();
                }

                if (discount == null)
                {
                    var discount = new Discount();
                    discount.Name = Name;
                    discount.Percent = percent;

                    connection.Execute("INSERT INTO Discount (Name, Percent) VALUES (@Name, @Percent);", discount);
                }
                else
                {
                    discount.Name = Name;
                    discount.Percent = percent;

                    connection.Execute("UPDATE Discount SET Name = @Name, Percent = @Percent WHERE DiscountID = @DiscountID;", discount);
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
                if (Name.Trim() == string.Empty || Percent.Trim() == string.Empty)
                {
                    throw new Exception();
                }

                var percent = Convert.ToSingle(Percent);

                if (percent < 0 || percent > 1)
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
