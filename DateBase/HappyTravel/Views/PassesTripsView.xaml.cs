using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels;
using System.Windows.Controls;

namespace HappyTravel.Views
{
    /// <summary>
    /// Логика взаимодействия для PassesTripsView.xaml
    /// </summary>
    public partial class PassesTripsView : UserControl, INavigatable
    {
        public PassesTripsView()
        {
            InitializeComponent();
            DataContext = new PassesTripsViewModel();
        }
    }
}
