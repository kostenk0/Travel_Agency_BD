using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels;
using System.Windows.Controls;

namespace HappyTravel.Views
{
    /// <summary>
    /// Логика взаимодействия для PassesHotelsView.xaml
    /// </summary>
    public partial class PassesHotelsView : UserControl, INavigatable
    {
        public PassesHotelsView()
        {
            InitializeComponent();
            DataContext = new PassesHotelsViewModel();
        }
    }
}
