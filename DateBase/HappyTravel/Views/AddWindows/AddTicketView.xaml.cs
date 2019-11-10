using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels.AddViewsModels;
using System.Windows.Controls;

namespace HappyTravel.Views.AddWindows
{
    /// <summary>
    /// Interaction logic for AddTicketView.xaml
    /// </summary>
    public partial class AddTicketView : UserControl, INavigatable
    {
        public AddTicketView()
        {
            InitializeComponent();
            DataContext = new AddTicketViewModel();
        }
    }
}
