using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels.AddViewsModels;
using System.Windows.Controls;

namespace HappyTravel.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddContractView.xaml
    /// </summary>
    public partial class AddContractView : UserControl, INavigatable
    {
        public AddContractView()
        {
            InitializeComponent();
            DataContext = new AddContractViewModel();
        }
    }
}
