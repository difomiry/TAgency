using System.Windows;
using TAgency.ViewModel;

namespace TAgency.View
{
    public partial class AddEditVoucherWindow : Window
    {
        public AddEditVoucherWindow(AddEditVoucherViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
