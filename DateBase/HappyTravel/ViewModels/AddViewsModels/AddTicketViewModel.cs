using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddTicketViewModel : BaseViewModel
    {
        private string _numberOfTicket;
        private int _seat;
        private string _typeOfSeat;
        private int _car;
        private int _row;

        private RelayCommand<object> _okCommand;
        private RelayCommand<object> _cancelCommand;

        public string NumberOfTicket
        {
            get
            {
                return _numberOfTicket;
            }
            set
            {
                _numberOfTicket = value;
                OnPropertyChanged();
            }
        }

        public int Seat
        {
            get
            {
                return _seat;
            }
            set
            {
                _seat = value;
                OnPropertyChanged();
            }
        }

        public string TypeOfSeat
        {
            get
            {
                return _typeOfSeat;
            }
            set
            {
                _typeOfSeat = value;
                OnPropertyChanged();
            }
        }

        public int Car
        {
            get
            {
                return _car;
            }
            set
            {
                _car = value;
                OnPropertyChanged();
            }
        }

        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.TripView)));
            }
        }

        public RelayCommand<object> OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand<object>(
                           o =>
                           {
                               try
                               {
                                   using (MySqlCommand comm = ConnectionManager.Connection.CreateCommand())
                                   {
                                       comm.CommandText = "INSERT INTO ticket(number_of_ticket, seat, type_of_seat, car, row, trip_number) VALUES(?number_of_ticket, ?seat, ?type_of_seat, ?car, ?row, ?trip_number)";
                                       comm.Parameters.Add("?number_of_ticket", MySqlDbType.VarChar).Value = NumberOfTicket;
                                       comm.Parameters.Add("?seat", MySqlDbType.Int32).Value = Seat;
                                       comm.Parameters.Add("?type_of_seat", MySqlDbType.VarChar).Value = TypeOfSeat;
                                       comm.Parameters.Add("?car", MySqlDbType.Int32).Value = Car;
                                       comm.Parameters.Add("?row", MySqlDbType.Int32).Value = Row;
                                       comm.Parameters.Add("?trip_number", MySqlDbType.Int32).Value = StationManager.SelectedTrip.TripNumber;
                                       comm.ExecuteNonQuery(); 
                                   }
                               }
                               catch (Exception e)
                               {
                                   MessageBox.Show(e.Message);
                               }
                           }));
            }
        }
    }
}
