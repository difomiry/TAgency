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
    /// The AddEditTourViewModel view model.
    /// </summary>
    public class AddEditTourViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// An instance of database connection.
        /// </summary>
        private readonly SQLiteConnection connection = SimpleIoc.Default.GetInstance<SQLiteConnection>();

        /// <summary>
        /// An instance of tour.
        /// </summary>
        private Tour tour;

        /// <summary>
        /// The window title.
        /// </summary>
        private string title;

        /// <summary>
        /// The tour date.
        /// </summary>
        private string date;

        /// <summary>
        /// The observable collection of hotels.
        /// </summary>
        private ObservableCollection<Hotel> hotels;

        /// <summary>
        /// The "Save" command that saves a tour to database.
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
        /// The tour date.
        /// </summary>
        public string Date
        {
            get => date;
            set => Set(ref date, value);
        }

        /// <summary>
        /// The observable collection of hotels.
        /// </summary>
        public ObservableCollection<Hotel> Hotels
        {
            get => hotels;
            set => Set(ref hotels, value);
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
        /// Initializes a new instance of the AddEditTourViewModel class.
        /// </summary>
        public AddEditTourViewModel()
        {
            Title = "Добавление тура";
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the AddEditTourViewModel class.
        /// </summary>
        /// <param name="tour">A tour.</param>
        public AddEditTourViewModel(Tour tour)
        {
            this.tour = tour;
            Title = string.Format("Редактирование тура #{0}", tour.TourID);
            Date = tour.Date.ToString();
            Initialize();
        }

        /// <summary>
        /// Initializes comands and hotels.
        /// </summary>
        private void Initialize()
        {
            if (tour != null)
            {
                Hotels = new ObservableCollection<Hotel>(connection.Query<Hotel>("SELECT *, (SELECT TourHotel.Duration FROM TourHotel WHERE TourID = @TourID AND HotelID = Hotel.HotelID) Duration FROM Hotel", tour));
            }
            else
            {
                Hotels = new ObservableCollection<Hotel>(connection.Query<Hotel>("SELECT * FROM Hotel"));
            }

            SaveCommand = new RelayCommand<Window>(SaveCommandExecute, SaveCommandCanExecute);
            CancelCommand = new RelayCommand<Window>(CancelCommandExecute);
        }

        /// <summary>
        /// Saves a tour to database.
        /// </summary>
        /// <param name="window">The window that binded to this view model.</param>
        private void SaveCommandExecute(Window window)
        {
            try
            {
                if (tour == null)
                {
                    tour = new Tour();
                    tour.Date = DateTime.Parse(Date);

                    tour.TourID = connection.Query<int>("INSERT INTO Tour (Date) VALUES (@Date); SELECT last_insert_rowid();", tour).Single();
                }

                foreach (var hotel in Hotels)
                {
                    int duration;

                    try
                    {
                        duration = Convert.ToInt32(hotel.Duration);
                    }
                    catch
                    {
                        connection.Execute("DELETE FROM TourHotel WHERE HotelID = @HotelID AND TourID = @TourID;", new { hotel.HotelID, tour.TourID });
                        continue;
                    }

                    if (duration > 0)
                    {
                        connection.Execute("INSERT INTO TourHotel (TourID, HotelID, Duration) VALUES (@TourID, @HotelID, @Duration);", new { hotel.HotelID, tour.TourID, Duration = duration });
                    }
                    else
                    {
                        connection.Execute("DELETE FROM TourHotel WHERE HotelID = @HotelID AND TourID = @TourID;", new { hotel.HotelID, tour.TourID });
                    }
                }

                connection.Execute("UPDATE Tour SET Date = @Date WHERE TourID = @TourID;", tour);

                window.DialogResult = true;
                window.Close();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
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
                foreach (var hotel in Hotels)
                {
                    try
                    {
                        Convert.ToInt32(hotel.Duration);
                    }
                    catch
                    {
                        throw new Exception();
                    }
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
