using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HappyTravel.ViewModels
{
    internal class ContractViewModel : BaseViewModel
    {
        private RelayCommand<object> _closeCommand;
        public ObservableCollection<Contract> Contract { get; private set; }
        public CollectionViewSource ViewSource { get; private set; }
        public string Visibility
        {
            get
            {
                return StationManager.UserControl;
            }
        }

        public string UserButtons
        {
            get
            {
                return StationManager.UserButtons;
            }
        }

        public string AdminButtons
        {
            get
            {
                return StationManager.AdminButtons;
            }
        }

        internal ContractViewModel()
        {
            Contract = StationManager.DataStorage.GetContracts();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Contract;
        }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.MainManager);
                           }));
            }
        }
    }
}
