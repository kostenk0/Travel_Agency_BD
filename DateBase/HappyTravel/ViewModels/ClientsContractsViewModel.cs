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
    internal class ClientsContractsViewModel : BaseViewModel
    {
        #region Fields
        #region Commands
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _addPhone;
        #endregion
        private ObservableCollection<Contract> _contracts;
        #endregion

        #region Constructors
        internal ClientsContractsViewModel()
        {
            Contracts = new ObservableCollection<Contract>();
            ViewSource = new CollectionViewSource();
            ViewSource.Source = Contracts;
            Init();
        }
        #endregion
        #region Properties
        public CollectionViewSource ViewSource { get; private set; }
        public ObservableCollection<Contract> Contracts { get => _contracts; set => _contracts = value; }
        #endregion

        private void Init()
        {
            try
            {
                string sql = $"SELECT * FROM contract WHERE client_code = {StationManager.SelectedClient.ClientCode}";
                MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection);
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contract_number = reader.GetInt32(0);
                        var date_of_making = reader.GetDateTime(1);
                        Contracts.Add(new Contract(contract_number, date_of_making, StationManager.SelectedClient.ClientCode));
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
                               NavigationManager.Instance.Navigate(ViewType.ClientsView);
                           }));
            }
        }
        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addPhone ?? (_addPhone = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.AddContract);
                           }));
            }
        }
        #endregion
    }
}

