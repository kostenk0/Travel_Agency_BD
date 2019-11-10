using HappyTravel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyTravel.DataStorage
{
    internal interface IDataStorage
    {
        ObservableCollection<Client> GetClients();
        ObservableCollection<Contract> GetContracts();
        ObservableCollection<Pass> GetPass();
        ObservableCollection<Trip> GetTrips();
        ObservableCollection<Hotel> GetHotels();
        ObservableCollection<Resort> GetResorts();
        ObservableCollection<ContactPerson> GetContactPersons();
        //bool UserExists(string login);

        //User GetUserByLogin(string login);

        void AddClient(Client client);
        void AddContract(Contract contract);
        void AddPass(Pass pass);
        void RemoveClient(Client client);
        void RemovePass(Pass pass);
        void AddTrip(Trip trip);
        void RemoveTrip(Trip trip);
        void AddResort(Resort resort);
        void RemoveResort(Resort resort);
    }
}
