using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels.AddViewsModels;
using System.Windows.Controls;

namespace HappyTravel.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddPass.xaml
    /// </summary>
    public partial class AddPass : UserControl, INavigatable
    {
        public AddPass()
        {
            InitializeComponent();
            DataContext = new AddPassViewModel();
        }
    }
}
