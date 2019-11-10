using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HappyTravel.ViewModels
{
    internal class MainViewManagerModel : BaseViewModel
    {
        #region Fields
        #region Commands
        private RelayCommand<object> _getClientsCommand;
        private RelayCommand<object> _getContractsCommand;
        private RelayCommand<object> _getPassesCommand;
        private RelayCommand<object> _getTripsCommand;
        private RelayCommand<object> _getHotelsCommand;
        private RelayCommand<object> _getResortsCommand;
        private RelayCommand<object> _getContactPersonsCommand;
        #endregion
        #endregion

        #region Properties
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
        #endregion

        #region Commands

        public RelayCommand<object> GetClientsCommand
        {
            get
            {
                return _getClientsCommand ?? (_getClientsCommand = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.ClientsView);
                           }));
            }
        }

        public RelayCommand<Object> GetContractsCommand
        {
            get
            {
                return _getContractsCommand ?? (_getContractsCommand = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.ContractView);
                           }));
            }
        }

        public RelayCommand<Object> GetPassesCommand
        {
            get
            {
                return _getPassesCommand ?? (_getPassesCommand = new RelayCommand<object>(o =>
                {
                    NavigationManager.Instance.Navigate(ViewType.PassView);
                }));
            }
        }

        public RelayCommand<Object> GetTripsCommand
        {
            get
            {
                return _getTripsCommand ?? (_getTripsCommand = new RelayCommand<object>(o =>
                {
                    NavigationManager.Instance.Navigate(ViewType.TripView);
                }));
            }
        }

        public RelayCommand<Object> GetHotelsCommand
        {
            get
            {
                return _getHotelsCommand ?? (_getHotelsCommand = new RelayCommand<object>(o =>
                {
                    NavigationManager.Instance.Navigate(ViewType.HotelView);
                }));
            }
        }

        public RelayCommand<Object> GetResortsCommand
        {
            get
            {
                return _getResortsCommand ?? (_getResortsCommand = new RelayCommand<object>(o =>
                {
                    NavigationManager.Instance.Navigate(ViewType.ResortView);
                }));
            }
        }

        public RelayCommand<Object> GetContactPersonsCommand
        {
            get
            {
                return _getContactPersonsCommand ?? (_getContactPersonsCommand = new RelayCommand<object>(o =>
                {
                    NavigationManager.Instance.Navigate(ViewType.ContactPersonView);
                }));
            }
        }

        #endregion
    }
}
