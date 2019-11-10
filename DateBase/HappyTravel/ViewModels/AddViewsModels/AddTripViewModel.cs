using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddTripViewModel : BaseViewModel
    {
        private string _tripNumber;
        private string _name;
        private string _typeOfTransport;
        private string _placeOfDeparture;
        private string _placeOfArrival;
        private DateTime? _departureDate;
        private DateTime? _dateOfArrival;

        private RelayCommand<object> _okCommand;
        private RelayCommand<object> _cancelCommand;
        public ComboBoxItem SelectedTypeOfTransport { get; set; }

        public string TripNumber
        {
            get
            {
                return _tripNumber;
            }
            set
            {
                _tripNumber = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string TypeOfTransport
        {
            get
            {
                return _typeOfTransport;
            }
            set
            {
                _typeOfTransport = value;
                OnPropertyChanged();
            }
        }

        public string PlaceOfDeparture
        {
            get
            {
                return _placeOfDeparture;
            }
            set
            {
                _placeOfDeparture = value;
                OnPropertyChanged();
            }
        }

        public string PlaceOfArrival
        {
            get
            {
                return _placeOfArrival;
            }
            set
            {
                _placeOfArrival = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DepartureDate
        {
            get
            {
                return _departureDate;
            }
            set
            {
                _departureDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DateOfArrival
        {
            get
            {
                return _dateOfArrival;
            }
            set
            {
                _dateOfArrival = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand<object>(
                           o =>
                           {
                               AddTrip();
                           }));
            }
        }

        public RelayCommand<Object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.TripView)));
            }
        }

        private bool IsDepartureDateCorrect()
        {
            if (DateTime.Today.Date > DepartureDate.Value.Date)
                return false;
            return true;
        }

        private bool IsArrivalDateCorrect()
        {
            if (DateTime.Today.Date > DateOfArrival.Value.Date)
                return false;
            if (DepartureDate.Value.Date > DateOfArrival.Value.Date)
                return false;
            return true;
        }

        private bool AreFormsFilled()
        {
            if (string.IsNullOrWhiteSpace(TripNumber))
            {
                MessageBox.Show("Trip number is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Name is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace((string)SelectedTypeOfTransport.Content))
            {
                MessageBox.Show("Type of transport is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(PlaceOfDeparture))
            {
                MessageBox.Show("Place of departure is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(PlaceOfArrival))
            {
                MessageBox.Show("Place of arrival is empty!");
                return false;
            }
            if (!DepartureDate.HasValue)
            {
                MessageBox.Show("Departure date is empty!");
                return false;
            }
            if (!_dateOfArrival.HasValue)
            {
                MessageBox.Show("Date of arrival is empty!");
                return false;
            }
            return true;
        }

        private void AddTrip()
        {
            if (AreFormsFilled() && IsDepartureDateCorrect() && IsArrivalDateCorrect())
            {
                using (MySqlCommand comm = ConnectionManager.Connection.CreateCommand())
                {
                    comm.CommandText = "INSERT INTO trip(trip_number, name, type_of_transport, place_of_departure, place_of_arrival, " +
                        "departure_date, date_of_arrival) VALUES(?trip_number, ?name, ?type_of_transport, ?place_of_departure, ?place_of_arrival," +
                        " ?departure_date, ?date_of_arrival)";
                    comm.Parameters.Add("?trip_number", MySqlDbType.VarChar).Value = TripNumber;
                    comm.Parameters.Add("?name", MySqlDbType.VarChar).Value = Name;
                    comm.Parameters.Add("?type_of_transport", MySqlDbType.VarChar).Value = SelectedTypeOfTransport;
                    comm.Parameters.Add("?place_of_departure", MySqlDbType.VarChar).Value = PlaceOfDeparture;
                    comm.Parameters.Add("?place_of_arrival", MySqlDbType.VarChar).Value = PlaceOfArrival;
                    comm.Parameters.Add("?departure_date", MySqlDbType.DateTime).Value = DepartureDate;
                    comm.Parameters.Add("?date_of_arrival", MySqlDbType.DateTime).Value = DateOfArrival;
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Successful add trip!");
                    StationManager.DataStorage.AddTrip(new Trip(TripNumber, Name, (string)SelectedTypeOfTransport.Content, PlaceOfDeparture, PlaceOfArrival, DepartureDate.Value, DateOfArrival.Value));
                }
            }
        }
    }
}
