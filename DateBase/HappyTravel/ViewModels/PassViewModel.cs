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
    internal class PassViewModel : BaseViewModel
    {
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _addHotelCommand;
        private RelayCommand<object> _addTripCommand;
        private RelayCommand<object> _showTripsCommand;
        private RelayCommand<object> _showHotelsCommand;
        private RelayCommand<object> _removeCommand;

        public ObservableCollection<Pass> Pass { get; private set; }
        public CollectionViewSource ViewSource { get; private set; }
        public Pass SelectedPass { get; set; }
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

        internal PassViewModel()
        {
            Pass = StationManager.DataStorage.GetPass();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Pass;
        }

        #region Commands
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
                               NavigationManager.Instance.Navigate(ViewType.AddPass);
                           }));
            }
        }

        public RelayCommand<object> ShowHotelsCommand
        {
            get
            {
                return _showHotelsCommand ?? (_showHotelsCommand = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedPass = SelectedPass;
                               NavigationManager.Instance.Navigate(ViewType.PassesHotels);
                           }));
            }
        }
        public RelayCommand<object> ShowTripsCommand
        {
            get
            {
                return _showTripsCommand ?? (_showTripsCommand = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedPass = SelectedPass;
                               NavigationManager.Instance.Navigate(ViewType.PassesTrips);
                           }));
            }
        }

        public RelayCommand<object> AddHotelCommand
        {
            get
            {
                return _addHotelCommand ?? (_addHotelCommand = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedPass = SelectedPass;
                               NavigationManager.Instance.Navigate(ViewType.AddPassesHotel);
                           }));
            }
        }

        public RelayCommand<object> AddTripCommand
        {
            get
            {
                return _addTripCommand ?? (_addTripCommand = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedPass = SelectedPass;
                               NavigationManager.Instance.Navigate(ViewType.AddPassesTrip);
                           }));
            }
        }

        public RelayCommand<object> RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new RelayCommand<object>(
                           o =>
                           {
                               string sql = $"delete from pass where pass_number = {SelectedPass.PassNumber}";
                               using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
                               {
                                   comm.ExecuteNonQuery();
                                   StationManager.DataStorage.RemovePass(SelectedPass);
                               }
                           }));
            }
        }
        #endregion
    }
}
