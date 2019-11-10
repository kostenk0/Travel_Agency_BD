using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels;
using System.Windows.Controls;

namespace HappyTravel.Views
{
    /// <summary>
    /// Логика взаимодействия для ClientsPhonesView.xaml
    /// </summary>
    public partial class ClientsPhonesView : UserControl, INavigatable
    {
        public ClientsPhonesView()
        {
            InitializeComponent();
            DataContext = new ClientPhonesViewModel();
        }
    }
}
