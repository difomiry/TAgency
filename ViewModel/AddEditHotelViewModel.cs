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
    /// The AddEditHotelViewModel view model.
    /// </summary>
    public class AddEditHotelViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// An instance of hotel.
        /// </summary>
        private Hotel hotel;

        /// <summary>
        /// The window title.
        /// </summary>
        private string title;

        /// <summary>
        /// The hotel name.
        /// </summary>
        private string name;

        /// <summary>
        /// The hotel country.
        /// </summary>
        private string country;

        /// <summary>
        /// The hotel climate.
        /// </summary>
        private string climate;

        /// <summary>
        /// The hotel cost.
        /// </summary>
        private string cost;

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
        /// The hotel name.
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        /// <summary>
        /// The hotel country.
        /// </summary>
        public string Country
        {
            get => country;
            set => Set(ref country, value);
        }

        /// <summary>
        /// The hotel climate.
        /// </summary>
        public string Climate
        {
            get => climate;
            set => Set(ref climate, value);
        }

        /// <summary>
        /// The hotel cost.
        /// </summary>
        public string Cost
        {
            get => cost;
            set => Set(ref cost, value);
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
        /// Initializes a new instance of the AddEditHotelViewModel class.
        /// </summary>
        public AddEditHotelViewModel()
        {
            Title = "Добавление отеля";
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the AddEditHotelViewModel class.
        /// </summary>
        /// <param name="discont">A hotel.</param>
        public AddEditHotelViewModel(Hotel hotel)
        {
            this.hotel = hotel;
            Title = string.Format("Редактирование отеля #{0}", hotel.HotelID);
            Name = hotel.Name;
            Country = hotel.Country;
            Climate = hotel.Climate;
            Cost = hotel.Cost.ToString();
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
        /// Saves a hotel to database.
        /// </summary>
        /// <param name="window">The window that binded to this view model.</param>
        private void SaveCommandExecute(Window window)
        {
            try
            {
                if (Name.Trim() == string.Empty || Country.Trim() == string.Empty || Climate.Trim() == string.Empty || Cost.Trim() == string.Empty)
                {
                    throw new Exception();
                }

                var cost = Convert.ToSingle(Cost);

                if (cost < 0)
                {
                    throw new Exception();
                }

                if (hotel == null)
                {
                    var hotel = new Hotel();
                    hotel.Name = Name;
                    hotel.Country = Country;
                    hotel.Climate = Climate;
                    hotel.Cost = cost;

                    connection.Execute("INSERT INTO Hotel (Name, Country, Climate, Cost) VALUES (@Name, @Country, @Climate, @Cost);", hotel);
                }
                else
                {
                    hotel.Name = Name;
                    hotel.Country = Country;
                    hotel.Climate = Climate;
                    hotel.Cost = cost;

                    connection.Execute("UPDATE Hotel SET Name = @Name, Country = @Country, Climate = @Climate, Cost = @Cost WHERE HotelID = @HotelID;", hotel);
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
                if (Name.Trim() == string.Empty || Country.Trim() == string.Empty || Climate.Trim() == string.Empty || Cost.Trim() == string.Empty)
                {
                    throw new Exception();
                }

                var cost = Convert.ToSingle(Cost);

                if (cost < 0)
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
