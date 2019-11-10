using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels;
using System.Windows.Controls;

namespace HappyTravel.Views
{
    /// <summary>
    /// Interaction logic for ResortContactPersonsView.xaml
    /// </summary>
    public partial class ResortContactPersonsView : UserControl, INavigatable
    {
        public ResortContactPersonsView()
        {
            InitializeComponent();
            DataContext = new ResortContactPersonsViewModel();
        }
    }
}
