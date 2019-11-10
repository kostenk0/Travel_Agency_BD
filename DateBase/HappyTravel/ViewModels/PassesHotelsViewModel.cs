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
    internal class PassesHotelsViewModel : BaseViewModel
    {
        #region Fields
        #region Commands
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _addHotel;
        private RelayCommand<object> _remove;
        #endregion
        private ObservableCollection<Hotel> _hotels;
        #endregion

        #region Constructors
        internal PassesHotelsViewModel()
        {
            Hotels = new ObservableCollection<Hotel>();
            ViewSource = new CollectionViewSource();
            ViewSource.Source = Hotels;
            Init();
        }
        #endregion
        #region Properties
        public CollectionViewSource ViewSource { get; private set; }
        public ObservableCollection<Hotel> Hotels { get => _hotels; private set => _hotels = value; }
        public PhoneNumber SelectedHotel { get; set; }
        #endregion
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
                string sql = $"SELECT hotel.hotel_code, hotel.title, hotel.classification, hotel.describe, hotel.resort_code" +
                    $" FROM hotel INNER JOIN include ON hotel.hotel_code = include.hotel_code" +
                    $" WHERE include.pass_number = {StationManager.SelectedPass.PassNumber}";
                MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection);
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hotel_code = reader.GetString(0);
                        var title = reader.GetString(1);
                        var classification = reader.GetString(2);
                        var describe = reader.GetString(3);
                        var resort_code = reader.GetString(4);
                        Hotels.Add(new Hotel(hotel_code, title, classification, describe, resort_code));
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
                               NavigationManager.Instance.Navigate(ViewType.PassView);
                           }));
            }
        }
        //public RelayCommand<object> AddCommand
        //{
        //    get
        //    {
        //        return _addPhone ?? (_addPhone = new RelayCommand<object>(
        //                   o =>
        //                   {
        //                       NavigationManager.Instance.Navigate(ViewType.AddPhoneView);
        //                   }));
        //    }
        //}
        //public RelayCommand<object> RemoveCommand
        //{
        //    get
        //    {
        //        return _remove ?? (_remove = new RelayCommand<object>(
        //                   o =>
        //                   {
        //                       string sql = $"delete from phonenumber where phone_number = \"{SelectedPhone.Number}\"";
        //                       using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
        //                       {
        //                           comm.ExecuteNonQuery();
        //                           this.PhoneNumbers.Remove(SelectedPhone);
        //                       }
        //                   }));
        //    }
        //}
        #endregion
    }
}

