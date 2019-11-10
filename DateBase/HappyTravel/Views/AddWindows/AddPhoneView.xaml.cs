using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels.AddViewsModels;
using System.Windows.Controls;

namespace HappyTravel.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddPhoneView.xaml
    /// </summary>
    public partial class AddPhoneView : UserControl, INavigatable
    {
        public AddPhoneView()
        {
            InitializeComponent();
            DataContext = new AddPhoneViewModel();
        }
    }
}
