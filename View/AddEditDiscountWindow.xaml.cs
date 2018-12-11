using System.Windows;
using TAgency.ViewModel;

namespace TAgency.View
{
    public partial class AddEditDiscountWindow : Window
    {
        public AddEditDiscountWindow(AddEditDiscountViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
