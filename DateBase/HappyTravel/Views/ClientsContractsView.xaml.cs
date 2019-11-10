using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels;
using System.Windows.Controls;

namespace HappyTravel.Views
{
    /// <summary>
    /// Логика взаимодействия для ClientsContractsView.xaml
    /// </summary>
    public partial class ClientsContractsView : UserControl, INavigatable
    {
        public ClientsContractsView()
        {
            InitializeComponent();
            DataContext = new ClientsContractsViewModel();
        }
    }
}
