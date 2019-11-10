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
    internal class ClientPhonesViewModel : BaseViewModel
    {
        #region Fields
        #region Commands
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _addPhone;
        private RelayCommand<object> _remove;
        #endregion
        private ObservableCollection<PhoneNumber> _phoneNumbers;
        #endregion

        #region Constructors
        internal ClientPhonesViewModel()
        {
            PhoneNumbers = new ObservableCollection<PhoneNumber>();
            ViewSource = new CollectionViewSource();
            ViewSource.Source = PhoneNumbers;
            Init();
        }
        #endregion
        #region Properties
        public CollectionViewSource ViewSource { get; private set; }
        public ObservableCollection<PhoneNumber> PhoneNumbers { get => _phoneNumbers; set => _phoneNumbers = value; }
        public PhoneNumber SelectedPhone { get; set; }
        #endregion

        private void Init()
        {
            try
            {
                string sql = $"SELECT phone_number FROM phonenumber WHERE client_code = {StationManager.SelectedClient.ClientCode}";
                MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection);
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var number = reader.GetString(0);
                        PhoneNumbers.Add(new PhoneNumber(number, StationManager.SelectedClient.ClientCode));
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
                               NavigationManager.Instance.Navigate(ViewType.AddPhoneView);
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
                               string sql = $"delete from phonenumber where phone_number = \"{SelectedPhone.Number}\"";
                               using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
                               {
                                   comm.ExecuteNonQuery();
                                   this.PhoneNumbers.Remove(SelectedPhone);
                               }
                           }));
            }
        }
        #endregion
    }
}
