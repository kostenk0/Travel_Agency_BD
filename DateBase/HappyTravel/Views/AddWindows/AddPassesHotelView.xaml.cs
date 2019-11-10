using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels.AddViewsModels;
using System.Windows.Controls;

namespace HappyTravel.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddPassesHotelView.xaml
    /// </summary>
    public partial class AddPassesHotelView : UserControl, INavigatable
    {
        public AddPassesHotelView()
        {
            InitializeComponent();
            DataContext = new AddPassesHotelViewModel();
        }
    }
}
