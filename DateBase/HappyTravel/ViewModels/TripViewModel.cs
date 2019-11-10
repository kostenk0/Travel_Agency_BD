using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace HappyTravel.ViewModels
{
    internal class TripViewModel
    {
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _addTicket;
        private RelayCommand<object> _showTickets;
        private RelayCommand<object> _remove;
        private RelayCommand<object> _closeCommand;

        public ObservableCollection<Trip> Trips { get; private set; }
        public CollectionViewSource ViewSource { get; private set; }
        public Trip SelectedTrip { get; set; }
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
        internal TripViewModel()
        {
            Trips = StationManager.DataStorage.GetTrips();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Trips;
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
                               NavigationManager.Instance.Navigate(ViewType.AddTripView);
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
                               string sql = $"delete from trip where trip_number = {SelectedTrip.TripNumber}";
                               using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
                               {
                                   comm.ExecuteNonQuery();
                                   StationManager.DataStorage.RemoveTrip(SelectedTrip);
                               }
                           }));
            }
        }

        public RelayCommand<object> ShowTickets
        {
            get
            {
                return _showTickets ?? (_showTickets = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedTrip = SelectedTrip;
                               NavigationManager.Instance.Navigate(ViewType.TripTicketsView);
                           }));
            }
        }

        public RelayCommand<object> AddTicket
        {
            get
            {
                return _addTicket ?? (_addTicket = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.AddTicketView);
                               StationManager.SelectedTrip = SelectedTrip;
                           }));
            }
        }
    }
}
