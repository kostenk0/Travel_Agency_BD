using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace HappyTravel.ViewModels
{
    internal class ClientsViewModel : BaseViewModel
    {
        #region Fields
        #region Commands
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _addPhone;
        private RelayCommand<object> _showPhones;
        private RelayCommand<object> _addContract;
        private RelayCommand<object> _remove;
        private RelayCommand<object> _showContracts;
        #endregion
        #endregion

        #region Properties
        public ObservableCollection<Client> Clients { get; private set; }
        public CollectionViewSource ViewSource { get; private set; }
        public Client SelectedClient { get; set; }
        #endregion

        #region Constructors
        internal ClientsViewModel()
        {
            Clients = StationManager.DataStorage.GetClients();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Clients;
        }
        #endregion

        #region Commands

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.AddClientView);
                           }));
            }
        }
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
        public RelayCommand<object> AddPhone
        {
            get
            {
                return _addPhone ?? (_addPhone = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.AddPhoneView);
                               StationManager.SelectedClient = SelectedClient;
                           }));
            }
        }
        public RelayCommand<object> ShowPhones
        {
            get
            {
                return _showPhones ?? (_showPhones = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedClient = SelectedClient;
                               NavigationManager.Instance.Navigate(ViewType.ClientsPhonesView);
                           }));
            }
        }

        public RelayCommand<object> AddContract
        {
            get
            {
                return _addContract ?? (_addContract = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedClient = SelectedClient;
                               NavigationManager.Instance.Navigate(ViewType.AddContract);
                           }));
            }
        }

        public RelayCommand<object> ShowContracts
        {
            get
            {
                return _showContracts ?? (_showContracts = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedClient = SelectedClient;
                               NavigationManager.Instance.Navigate(ViewType.ClientsContracts);
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
                               string sql = $"delete from client where client_code = {SelectedClient.ClientCode}";
                               using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
                               {
                                   comm.ExecuteNonQuery();
                                   StationManager.DataStorage.RemoveClient(SelectedClient); 
                               }
                           }));
            }
        }
        #endregion
    }
}