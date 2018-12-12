using System.Windows;
using TAgency.ViewModel;

namespace TAgency.View
{
    public partial class AddEditHotelWindow : Window
    {
        public AddEditHotelWindow(AddEditHotelViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
