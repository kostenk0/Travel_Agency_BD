using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HappyTravel.ViewModels
{
    internal class ResortViewModel
    {
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _addHotel;
        private RelayCommand<object> _showHotel;
        private RelayCommand<object> _addContactPerson;
        private RelayCommand<object> _showContactPerson;
        private RelayCommand<object> _remove;
        private RelayCommand<object> _closeCommand;

        public ObservableCollection<Resort> Resorts { get; private set; }
        public CollectionViewSource ViewSource { get; private set; }
        public Resort SelectedResort { get; set; }
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
        internal ResortViewModel()
        {
            Resorts = StationManager.DataStorage.GetResorts();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Resorts;
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

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.AddResortView);
                           }));
            }
        }

        public RelayCommand<object> Remove
        {
            get
            {
                return _remove ?? (_remove = new RelayCommand<object>(
                           o =>
                           {
                               string sql = ($"delete from resort where resort_code = \"{SelectedResort.ResortCode}\"");
                               using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
                               {
                                   comm.ExecuteNonQuery();
                                   StationManager.DataStorage.RemoveResort(SelectedResort);
                               }
                           }));
            }
        }

        public RelayCommand<object> AddContactPerson
        {
            get
            {
                return _addContactPerson ?? (_addContactPerson = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.AddContactPersonView);
                               StationManager.SelectedResort = SelectedResort;
                           }));
            }
        }

        public RelayCommand<object> ShowContactPerson
        {
            get
            {
                return _showContactPerson ?? (_showContactPerson = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedResort = SelectedResort;
                               NavigationManager.Instance.Navigate(ViewType.ResortContactPersonsView);
                           }));
            }
        }
    }
}
