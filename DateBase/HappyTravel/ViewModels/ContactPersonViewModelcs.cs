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
    internal class ContactPersonViewModel
    {
        private RelayCommand<object> _closeCommand;

        public ObservableCollection<ContactPerson> ContactPersons { get; private set; }
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

        #region Constructors
        internal ContactPersonViewModel()
        {
            ContactPersons = StationManager.DataStorage.GetContactPersons();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.ContactPersons;
        }
        #endregion

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
