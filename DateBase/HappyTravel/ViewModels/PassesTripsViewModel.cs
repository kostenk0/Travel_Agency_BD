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
using System.Windows;
using System.Windows.Data;

namespace HappyTravel.ViewModels
{
    internal class PassesTripsViewModel : BaseViewModel
    {
        #region Fields
        #region Commands
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _addTrip;
        private RelayCommand<object> _remove;
        #endregion
        private ObservableCollection<Trip> _trips;
        #endregion

        #region Constructors
        internal PassesTripsViewModel()
        {
            Trips = new ObservableCollection<Trip>();
            ViewSource = new CollectionViewSource();
            ViewSource.Source = Trips;
            Init();
        }
        #endregion
        #region Properties
        public CollectionViewSource ViewSource { get; private set; }
        public ObservableCollection<Trip> Trips { get => _trips; private set => _trips = value; }
        public PhoneNumber SelectedTrip { get; set; }
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

        private void Init()
        {
            List<string> tripsNumbers = new List<string>();
            try
            {
                string sql = $"SELECT DISTINCT trip_number FROM ticket WHERE pass_number = {StationManager.SelectedPass.PassNumber}";
                MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection);
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tripsNumbers.Add(reader.GetString(0));
                    }
                }
                foreach(Trip trip in StationManager.DataStorage.GetTrips())
                {
                    if(tripsNumbers.Contains(trip.TripNumber))
                    {
                        Trips.Add(trip);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #region Commands
        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.PassView);
                           }));
            }
        }
        //public RelayCommand<object> AddCommand
        //{
        //    get
        //    {
        //        return _addPhone ?? (_addPhone = new RelayCommand<object>(
        //                   o =>
        //                   {
        //                       NavigationManager.Instance.Navigate(ViewType.AddPhoneView);
        //                   }));
        //    }
        //}
        //public RelayCommand<object> RemoveCommand
        //{
        //    get
        //    {
        //        return _remove ?? (_remove = new RelayCommand<object>(
        //                   o =>
        //                   {
        //                       string sql = $"delete from phonenumber where phone_number = \"{SelectedPhone.Number}\"";
        //                       using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
        //                       {
        //                           comm.ExecuteNonQuery();
        //                           this.PhoneNumbers.Remove(SelectedPhone);
        //                       }
        //                   }));
        //    }
        //}
        #endregion
    }
}

