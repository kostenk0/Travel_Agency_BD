using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace HappyTravel.ViewModels
{
    class TripTicketsViewModel
    {
        private RelayCommand<object> _remove;
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _addTicket;

        private ObservableCollection<Ticket> _tickets;

        internal TripTicketsViewModel()
        {
            Tickets = new ObservableCollection<Ticket>();
            ViewSource = new CollectionViewSource();
            ViewSource.Source = Tickets;
            Init();
        }

        public CollectionViewSource ViewSource { get; private set; }
        public ObservableCollection<Ticket> Tickets { get => _tickets; set => _tickets = value; }
        public Ticket SelectedTicket { get; set; }
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

        private void Init()
        {
            try
            {
                string sql = $"SELECT * FROM ticket WHERE trip_number = {StationManager.SelectedTrip.TripNumber}";
                MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection);
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var number_of_ticket = reader.GetString(0);
                        var seat = reader.GetInt32(1);
                        var type_of_seat = reader.GetString(2);
                        var car = reader.GetInt32(3);
                        var row = reader.GetInt32(4);
                        string pass_number;
                        try
                        {
                            pass_number = reader.GetString(5);
                        }
                        catch (Exception e)
                        {
                            pass_number = null;
                        }
                        Tickets.Add(new Ticket(number_of_ticket, seat, type_of_seat, car, row, StationManager.SelectedTrip.TripNumber, pass_number));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.TripView);
                           }));
            }
        }

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addTicket ?? (_addTicket = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.AddTicketView);
                           }));
            }
        }

        public RelayCommand<object> RemoveCommand
        {
            get
            {
                return _remove ?? (_remove = new RelayCommand<object>(
                           o =>
                           {
                               string sql = $"delete from ticket where number_of_ticket = \"{SelectedTicket.NumberOfTicket}\"";
                               using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
                               {
                                   comm.ExecuteNonQuery();
                                   this.Tickets.Remove(SelectedTicket);
                               }
                           }));
            }
        }
    }
}
