using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels.AddViewsModels;
using System.Windows.Controls;

namespace HappyTravel.Views.AddWindows
{
    /// <summary>
    /// Interaction logic for AddResortView.xaml
    /// </summary>
    public partial class AddResortView : UserControl, INavigatable
    {
        public AddResortView()
        {
            InitializeComponent();
            DataContext = new AddResortViewModel();
        }
    }
}

