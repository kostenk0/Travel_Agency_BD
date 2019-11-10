using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddPhoneViewModel : BaseViewModel
    {
        #region Fields
        private string _number;
        #region Commands
        private RelayCommand<object> _okCommand;
        private RelayCommand<object> _canselCommand;
        #endregion
        #endregion
        #region Properties
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged();
            }
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
                               if(!AddClientViewModel.IsPhoneNumber(Number))
                               {
                                   MessageBox.Show("Invalid phone number!");
                                   return;
                               }
                               try
                               {
                                   MySqlCommand comm = ConnectionManager.Connection.CreateCommand();
                                   comm.CommandText = "INSERT INTO phonenumber(phone_number, client_code) VALUES(?phone_number, ?client_code)";
                                   comm.Parameters.Add("?phone_number", MySqlDbType.VarChar).Value = Number;
                                   comm.Parameters.Add("?client_code", MySqlDbType.Int32).Value = StationManager.SelectedClient.ClientCode;
                                   comm.ExecuteNonQuery();
                               }
                               catch (MySql.Data.MySqlClient.MySqlException)
                               {
                                   MessageBox.Show("Number is already exists!");
                               }
                           }));
            }
        }

        public RelayCommand<object> CanselCommand
        {
            get
            {
                return _canselCommand ?? (_canselCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.ClientsView)));
            }
        }

        #endregion
    }
}
