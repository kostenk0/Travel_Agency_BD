using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddPassViewModel : BaseViewModel
    {
        #region Fields
        private int? _numberOfPeople;
        private decimal? _price;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private int _duration;
        private DateTime? _dateOfSettlement;
        private DateTime? _dateOfEvivction;

        #region Commands
        private RelayCommand<object> _okCommand;
        private RelayCommand<object> _canselCommand;
        #endregion
        #endregion

        #region Properties
        public Trip SelectedTrip { get; set; }
        public Hotel SelectedHotel { get; set; }
        public ComboBoxItem SelectedAppartment { get; set; }
        public ComboBoxItem SelectedEating { get; set; }
        public ObservableCollection<Trip> Trips { get; private set; }
        public ObservableCollection<Hotel> Hotels { get; private set; }
        public CollectionViewSource TripsViewSource { get; private set; }
        public CollectionViewSource HotelsViewSource { get; private set; }

        public int? NumberOfPeople
        {
            get
            {
                return _numberOfPeople;
            }
            set
            {
                _numberOfPeople = value;
                UpdateTrips(value.Value);
                OnPropertyChanged();
            }
        }
        public decimal? Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
        public DateTime? StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime? EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DateOfSettlement
        {
            get
            {
                return _dateOfSettlement;
            }
            set
            {
                _dateOfSettlement = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DateOfEviction
        {
            get
            {
                return _dateOfEvivction;
            }
            set
            {
                _dateOfEvivction = value;
                OnPropertyChanged();
            }
        }

        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public AddPassViewModel()
        {
            Trips = StationManager.DataStorage.GetTrips();
            Hotels = StationManager.DataStorage.GetHotels();
            this.TripsViewSource = new CollectionViewSource();
            TripsViewSource.Source = this.Trips;
            this.HotelsViewSource = new CollectionViewSource();
            HotelsViewSource.Source = this.Hotels;
        }
        #endregion

        #region Commands

        public RelayCommand<object> OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand<object>(
                           o =>
                           {
                               try
                               {
                                   if (AreFormsFilled() && IsInputDataCorrect())
                                   {
                                       AddPass();
                                       AddTicketsToPass(SelectedTrip, GetLastPassCode(), NumberOfPeople.Value);
                                       AddHotelToPass(GetLastPassCode(), SelectedHotel, SelectedAppartment, SelectedEating, DateOfSettlement, DateOfEviction);
                                       MessageBox.Show("Successful!");
                                       NavigationManager.Instance.Navigate(ViewType.PassView);
                                   }
                               }
                               catch (Exception e)
                               {
                                   throw e;
                               }
                           }));
            }
        }

        public RelayCommand<Object> CanselCommand
        {
            get
            {
                return _canselCommand ?? (_canselCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.PassView)));
            }
        }
        #endregion

        private bool AreFormsFilled()
        {
            if(!NumberOfPeople.HasValue)
            {
                MessageBox.Show("Number of people doesnt exists!");
                return false;
            }
            if (!Price.HasValue)
            {
                MessageBox.Show("Price doesnt exists!");
                return false;
            }
            if (!StartDate.HasValue)
            {
                MessageBox.Show("Start date doesnt exists!");
                return false;
            }
            if (!EndDate.HasValue)
            {
                MessageBox.Show("End date doesnt exists!");
                return false;
            }
            if (!DateOfSettlement.HasValue)
            {
                MessageBox.Show("Date of settlement doesnt exists!");
                return false;
            }
            if (!DateOfEviction.HasValue)
            {
                MessageBox.Show("Date of eviction doesnt exists!");
                return false;
            }
            if (!DateOfEviction.HasValue)
            {
                MessageBox.Show("Date of eviction doesnt exists!");
                return false;
            }
            if (string.IsNullOrWhiteSpace((string)SelectedAppartment.Content))
            {
                MessageBox.Show("Type of appartment doesnt exists!");
                return false;
            }
            if (string.IsNullOrWhiteSpace((string)SelectedEating.Content))
            {
                MessageBox.Show("Type of eating doesnt exists!");
                return false;
            }
            if(SelectedHotel == null)
            {
                MessageBox.Show("No hotel selected!");
                return false;
            }
            if (SelectedTrip == null)
            {
                MessageBox.Show("No Trip selected!");
                return false;
            }
            return true;
        }

        private bool IsInputDataCorrect()
        {
            if(EndDate < StartDate)
            {
                MessageBox.Show("End date is smaller than start date");
                return false;
            }
            if(DateOfEviction < DateOfSettlement)
            {
                MessageBox.Show("Date of eviction is smaller than date of settlement");
                return false;
            }
            if(DateOfSettlement > EndDate)
            {
                MessageBox.Show("Date of settlement is bigger than end date!");
                return false;
            }
            if (DateOfSettlement < StartDate)
            {
                MessageBox.Show("Date of settlement is smaller than start date!");
                return false;
            }
            if (DateOfEviction > EndDate)
            {
                MessageBox.Show("Date of eviction is bigger than end date!");
                return false;
            }
            if(NumberOfPeople < 1)
            {
                MessageBox.Show("Number of people < 1!");
                return false;
            }
            CalcDuration();
            if(Duration < 1)
            {
                MessageBox.Show("Duration < 1!");
                return false;
            }
            return true;
        }

        private void CalcDuration()
        {
            Duration = (int)(EndDate.Value - StartDate.Value).TotalDays;
        }

        public int GetTicketsCountInTrip(Trip trip)
        {
            int count = 0;
            string sql =  $"SELECT number_of_ticket FROM ticket WHERE trip_number = {trip.TripNumber.ToString()} and pass_number is NULL";
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count++;
                    }

                }
            }
            return count;
        }

        private void UpdateTrips(int numberOfPeople)
        {
            Trips = new ObservableCollection<Trip>();
            TripsViewSource.Source = this.Trips;
            foreach (Trip trip in StationManager.DataStorage.GetTrips())
            {
                if(GetTicketsCountInTrip(trip) >= numberOfPeople)
                {
                    Trips.Add(trip);
                }
            }
        }

        public int GetLastPassCode()
        {
            int lastPassCode = 1;
            string sql = "SELECT MAX(pass_number) FROM pass";
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                MySqlDataReader dr = comand.ExecuteReader();
                if (dr.HasRows == true)
                {
                    dr.Read();
                    lastPassCode = dr.GetInt32(0);
                }
                return lastPassCode;
            }
        }

        private void AddPass()
        {
            using (MySqlCommand comm = ConnectionManager.Connection.CreateCommand())
            {
                comm.CommandText = "INSERT INTO pass(status, number_of_people, price, start_date, end_date, duration) VALUES(?status, ?number_of_people, ?price, ?start_date, ?end_date, ?duration)";
                comm.Parameters.Add("?status", MySqlDbType.VarChar).Value = "Відкрита";
                comm.Parameters.Add("?number_of_people", MySqlDbType.Int32).Value = NumberOfPeople.Value;
                comm.Parameters.Add("?price", MySqlDbType.Decimal).Value = Price.Value;
                comm.Parameters.Add("?start_date", MySqlDbType.Date).Value = StartDate.Value.Date;
                comm.Parameters.Add("?end_date", MySqlDbType.Date).Value = EndDate.Value.Date;
                comm.Parameters.Add("?duration", MySqlDbType.Int32).Value = Duration;
                comm.ExecuteNonQuery();
                StationManager.DataStorage.AddPass(new Pass(GetLastPassCode(), "Відкрита", NumberOfPeople.Value, Price.Value, StartDate.Value.Date, EndDate.Value.Date, Duration, null));
            }
        }

        public void AddTicketsToPass(Trip trip, int passCode, int numberOfPeople)
        {
            int count = 0;
            List<string> tickets = new List<string>();
            string sql = $"SELECT number_of_ticket FROM ticket WHERE trip_number = {trip.TripNumber} and pass_number is NULL";
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read() && count < numberOfPeople)
                    {
                        tickets.Add(reader.GetString(0));
                        count++;
                    }
                }
            }
            for (int i = 0; i < count; i++)
            {
                string sqlUpdate = $"UPDATE ticket SET pass_number = {passCode} WHERE number_of_ticket = {tickets[i]}";
                using (MySqlCommand comand = new MySqlCommand(sqlUpdate, ConnectionManager.Connection))
                {
                    comand.ExecuteNonQuery();
                }
            }
        }

        public void AddHotelToPass(int PassNumber, Hotel SelectedHotel, ComboBoxItem SelectedAppartment, ComboBoxItem SelectedEating, DateTime? DateOfSettlement, DateTime? DateOfEviction)
        {
            using (MySqlCommand comm = ConnectionManager.Connection.CreateCommand())
            {
                comm.CommandText = "INSERT INTO include(pass_number, hotel_code, type_of_appartment, type_of_eating, date_of_settlement, date_of_eviction) VALUES(?pass_number, ?hotel_code, ?type_of_appartment, ?type_of_eating, ?date_of_settlement, ?date_of_eviction)";
                comm.Parameters.Add("?pass_number", MySqlDbType.Int32).Value = PassNumber;
                comm.Parameters.Add("?hotel_code", MySqlDbType.String).Value = SelectedHotel.HotelCode;
                comm.Parameters.Add("?type_of_appartment", MySqlDbType.String).Value = (string)SelectedAppartment.Content;
                comm.Parameters.Add("?type_of_eating", MySqlDbType.String).Value = (string)SelectedEating.Content;
                comm.Parameters.Add("?date_of_settlement", MySqlDbType.DateTime).Value = DateOfSettlement.Value.Date;
                comm.Parameters.Add("?date_of_eviction", MySqlDbType.DateTime).Value = DateOfEviction.Value.Date;
                comm.ExecuteNonQuery();
            }
        }
    }
}
