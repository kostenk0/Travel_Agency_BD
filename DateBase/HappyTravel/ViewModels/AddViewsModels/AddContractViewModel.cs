using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddContractViewModel : BaseViewModel
    {
        #region Fields
        private int? _contractNumber;
        private DateTime? _dateOfMaking;
        public ObservableCollection<Pass> Contracts { get; private set; }
        public CollectionViewSource ViewSource { get; private set; }

        #region Commands
        private RelayCommand<IList> _okCommand;
        private RelayCommand<object> _canselCommand;
        #endregion
        #endregion

        #region Constructors
        internal AddContractViewModel()
        {
            Contracts = new ObservableCollection<Pass>();
            foreach (Pass pass in StationManager.DataStorage.GetPass())
            {
                if(!pass.ContractNumber.HasValue)
                {
                    Contracts.Add(pass);
                }
            }
            MessageBox.Show(Contracts.Count() + "");
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Contracts;
        }
        #endregion

        #region Properties
        public int? ContractNumber
        {
            get
            {
                return _contractNumber;
            }
            set
            {
                _contractNumber = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DateOfMaking
        {
            get
            {
                return _dateOfMaking;
            }
            set
            {
                _dateOfMaking = value;
                OnPropertyChanged();
            }
        }
       
        #endregion

        #region Commands

        public RelayCommand<IList> OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand<IList>(
                            items =>
                           {
                               try
                               {
                                   if(items.Count == 0)
                                   {
                                       throw new Exception("No passes selected!");
                                   }
                                   AddContract();
                                   foreach(Pass pass in items)
                                   {
                                       string sql = $"update pass set contract_number = {ContractNumber} where pass_number = {pass.PassNumber}";
                                       using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
                                       {
                                           comm.ExecuteNonQuery();
                                       }
                                       foreach (var item in StationManager.DataStorage.GetPass().Where(el => el.PassNumber == pass.PassNumber))
                                       {
                                           item.ContractNumber = ContractNumber;
                                       }
                                   }
                                   NavigationManager.Instance.Navigate(ViewType.ClientsView);
                               }
                               catch(Exception e)
                               {
                                   throw e;
                               }
                           }));
            }
        }

        private void AddContract()
        {
            if(!ContractNumber.HasValue)
            {
                throw new Exception("Contract number cant be empty!");
            }
            if (!DateOfMaking.HasValue)
            {
                throw new Exception("Contract number cant be empty!");
            }
            MessageBox.Show(DateOfMaking.Value.ToString());
            using (MySqlCommand comm = ConnectionManager.Connection.CreateCommand())
            {
                comm.CommandText = "INSERT INTO contract(contract_number, date_of_making, client_code) VALUES(?contract_number, ?date_of_making, ?client_code)";
                comm.Parameters.Add("?contract_number", MySqlDbType.VarChar).Value = ContractNumber;
                comm.Parameters.Add("?date_of_making", MySqlDbType.DateTime).Value = DateOfMaking.Value;
                comm.Parameters.Add("?client_code", MySqlDbType.VarChar).Value = StationManager.SelectedClient.ClientCode;
                comm.ExecuteNonQuery();
                MessageBox.Show("Successful!");
                StationManager.DataStorage.AddContract(new Contract(ContractNumber.Value, DateOfMaking.Value, StationManager.SelectedClient.ClientCode));
            }

        }

        public RelayCommand<Object> CanselCommand
        {
            get
            {
                return _canselCommand ?? (_canselCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.ClientsView)));
            }
        }
        #endregion
       
    }
}
