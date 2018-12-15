using System.Windows;
using TAgency.ViewModel;

namespace TAgency.View
{
    public partial class AddEditTourWindow : Window
    {
        public AddEditTourWindow(AddEditTourViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
