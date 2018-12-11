using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System.Data.SQLite;

namespace TAgency.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<ClientListViewModel>();
            SimpleIoc.Default.Register<HotelListViewModel>();
            SimpleIoc.Default.Register<TourListViewModel>();
            SimpleIoc.Default.Register<DiscountListViewModel>();
            SimpleIoc.Default.Register<VoucherListViewModel>();
        }

        /// <summary>
        /// Gets the Main view model.
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// Gets the ClientList view model.
        /// </summary>
        public ClientListViewModel ClientList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ClientListViewModel>();
            }
        }

        /// <summary>
        /// Gets the HotelList view model.
        /// </summary>
        public HotelListViewModel HotelList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HotelListViewModel>();
            }
        }

        /// <summary>
        /// Gets the TourList view model.
        /// </summary>
        public TourListViewModel TourList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TourListViewModel>();
            }
        }

        /// <summary>
        /// Gets the DiscountList view model.
        /// </summary>
        public DiscountListViewModel DiscountList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DiscountListViewModel>();
            }
        }

        /// <summary>
        /// Gets the VoucherList view model.
        /// </summary>
        public VoucherListViewModel VoucherList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VoucherListViewModel>();
            }
        }

        /// <summary>
        /// Cleans up the view models.
        /// </summary>
        public static void Cleanup()
        {
            SimpleIoc.Default.GetInstance<SQLiteConnection>().Close();
        }
    }
}