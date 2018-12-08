using GalaSoft.MvvmLight;

namespace TAgency.ViewModel
{
    /// <summary>
    /// The Main view model.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The path to database.
        /// </summary>
        private string pathToDatabase = "undefined";

        /// <summary>
        /// The path to database.
        /// </summary>
        public string PathToDatabase
        {
            get => pathToDatabase;
            set => Set(ref pathToDatabase, value);
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
        }
    }
}