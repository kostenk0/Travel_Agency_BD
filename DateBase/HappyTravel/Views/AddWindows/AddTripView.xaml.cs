using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels.AddViewsModels;
using System.Windows.Controls;

namespace HappyTravel.Views.AddWindows
{
    /// <summary>
    /// Interaction logic for AddTripView.xaml
    /// </summary>
    public partial class AddTripView : UserControl, INavigatable
    {
        public AddTripView()
        {
            InitializeComponent();
            DataContext = new AddTripViewModel();
        }
    }
}
