using HappyTravel.Models;
using HappyTravel.Tools.Managers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace HappyTravel.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private ObservableCollection<Client> _clients;
        private ObservableCollection<Contract> _contracts;
        private ObservableCollection<Pass> _pass;
        private ObservableCollection<Trip> _trips;
        private ObservableCollection<Hotel> _hotels;
        private ObservableCollection<Resort> _resorts;
        private ObservableCollection<ContactPerson> _contactPersons;

        internal SerializedDataStorage()
        {
            _clients = new ObservableCollection<Client>();
            _contracts = new ObservableCollection<Contract>();
            _pass = new ObservableCollection<Pass>();
            _trips = new ObservableCollection<Trip>();
            _hotels = new ObservableCollection<Hotel>();
            _resorts= new ObservableCollection<Resort>();
            _contactPersons = new ObservableCollection<ContactPerson>();
            try
            {
                SerializeClients();
                SerializeContract();
                SerializePass();
                SerializeTrips();
                SerializeHotels();
                SerializeResorts();
                SerializeContactPersons();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw e;
            }
        }

        public ObservableCollection<Client> GetClients()
        {
            return _clients;
        }

        private void SerializeClients()
        {
            string sql = "SELECT * FROM client";
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var client_code = reader.GetInt32(0);
                        var passport_number = reader.GetString(1);
                        var surname = reader.GetString(2);
                        var name = reader.GetString(3);
                        var fathers_name = reader.GetString(4);
                        var birth_date = reader.GetDateTime(5);
                        var age = reader.GetInt32(6);
                        var email = reader.GetString(7);
                        _clients.Add(new Client(client_code, passport_number, surname, name, fathers_name, birth_date, age, email));
                    }
                } 
            }
        }

        public ObservableCollection<Contract> GetContracts()
        {
            return _contracts;
        }

        private void SerializeContract()
        {
            string sql;
            if (StationManager.CurrentUser.Login.Equals("admin"))
            {
                sql = "SELECT * FROM contract";
            }
            else
            {
                sql = $"SELECT * FROM contract WHERE client_code = {StationManager.CurrentUser.Login}";
            }
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contract_number = reader.GetInt32(0);
                        var date_of_making = reader.GetDateTime(1);
                        var client_code = reader.GetInt32(2);
                        _contracts.Add(new Contract(contract_number, date_of_making, client_code));
                    }
                } 
            }
        }

        public ObservableCollection<Pass> GetPass()
        {
            return _pass;
        }

        private void SerializePass()
        {
            string sql = "SELECT * FROM pass";
            List<Pass> tempList = new List<Pass>();
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pass_number = reader.GetInt32(0);
                        var status = reader.GetString(1);
                        var number_of_people = reader.GetInt32(2);
                        var price = reader.GetDecimal(3);
                        var start_date = reader.GetDateTime(4);
                        var end_date = reader.GetDateTime(5);
                        var duration = reader.GetInt32(6);
                        int? contract_number;
                        try
                        {
                            contract_number = reader.GetInt32(7);
                        }
                        catch (Exception)
                        {
                            contract_number = null;
                        }
                        if (StationManager.CurrentUser.Login == "admin")
                        {
                            _pass.Add(new Pass(pass_number, status, number_of_people, price, start_date, end_date, duration, contract_number));
                        }
                        else
                        {
                            tempList.Add(new Pass(pass_number, status, number_of_people, price, start_date, end_date, duration, contract_number));
                        }

                    }

                }
            }
            if (StationManager.CurrentUser.Login != "admin")
            {
                foreach (Contract contract in _contracts)
                {
                    foreach(Pass pass in tempList)
                    {
                        if(pass.ContractNumber == contract.ContractNumber || pass.ContractNumber == null && !_pass.Contains(pass))
                        {
                            _pass.Add(pass);
                        }
                    }
                }
            }
        }

        public void RemoveClient(Client client)
        {
            _clients.Remove(client);
        }

        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public void AddContract(Contract contract)
        {
            _contracts.Add(contract);
        }

        public void AddPass(Pass pass)
        {
            _pass.Add(pass);
        }

        public ObservableCollection<Trip> GetTrips()
        {
            return _trips;
        }

        public void RemovePass(Pass pass)
        {
            _pass.Remove(pass);
        }

        private void SerializeTrips()
        {
            string sql = "SELECT * FROM trip";
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var trip_number = reader.GetString(0);
                        var name = reader.GetString(1);
                        var type_of_transport = reader.GetString(2);
                        var place_of_departure = reader.GetString(3);
                        var place_of_arrival = reader.GetString(4);
                        var departure_date = reader.GetDateTime(5);
                        var date_of_arrival = reader.GetDateTime(6);
                        _trips.Add(new Trip(trip_number, name, type_of_transport, place_of_departure, place_of_arrival, departure_date, date_of_arrival));
                    }

                }
            }
        }

        public ObservableCollection<Hotel> GetHotels()
        {
            return _hotels;
        }

        private void SerializeHotels()
        {
            string sql = "SELECT * FROM hotel";
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hotel_code = reader.GetString(0);
                        var title = reader.GetString(1);
                        var classification = reader.GetString(2);
                        var describe = reader.GetString(3);
                        var resort_code = reader.GetString(4);
                        _hotels.Add(new Hotel(hotel_code, title, classification, describe, resort_code));
                    }

                }
            }
        }

        public ObservableCollection<Resort> GetResorts()
        {
            return _resorts;
        }

        private void SerializeResorts()
        {
            string sql = "SELECT * FROM resort";
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var resort_code = reader.GetString(0);
                        var title = reader.GetString(1);
                        var country = reader.GetString(2);
                        _resorts.Add(new Resort(resort_code, title, country));
                    }

                }
            }
        }

        public ObservableCollection<ContactPerson> GetContactPersons()
        {
            return _contactPersons;
        }

        private void SerializeContactPersons()
        {
            string sql = "SELECT * FROM contact_person";
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contact_person_code = reader.GetInt32(0);
                        var surname = reader.GetString(1);
                        var name = reader.GetString(2);
                        var fathers_name = reader.GetString(3);
                        var email = reader.GetString(4);
                        var resort_code = reader.GetString(5);
                        _contactPersons.Add(new ContactPerson(contact_person_code, surname, name, fathers_name, email, resort_code));
                    }
                }
            }
        }

        public void AddTrip(Trip trip)
        {
            _trips.Add(trip);
        }

        public void RemoveTrip(Trip trip)
        {
            _trips.Remove(trip);
        }

        public void AddResort(Resort resort)
        {
            _resorts.Add(resort);
        }

        public void RemoveResort(Resort resort)
        {
            _resorts.Remove(resort);
        }
    }
}
