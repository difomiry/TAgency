using System.Windows;
using TAgency.ViewModel;

namespace TAgency.View
{
    public partial class AddEditClientWindow : Window
    {
        public AddEditClientWindow(AddEditClientViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
