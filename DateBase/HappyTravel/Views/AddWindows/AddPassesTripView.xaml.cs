using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels.AddViewsModels;
using System.Windows.Controls;

namespace HappyTravel.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddPassesTripView.xaml
    /// </summary>
    public partial class AddPassesTripView : UserControl, INavigatable
    {
        public AddPassesTripView()
        {
            InitializeComponent();
            DataContext = new AddPassesTripViewModel();
        }
    }
}
