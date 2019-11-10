using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using HappyTravel.Views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddPassesHotelViewModel : BaseViewModel
    {
        #region Fields
        DateTime? _dateOfSettlement;
        DateTime? _dateOfEvivction;
        #region Commands
        private RelayCommand<object> _okCommand;
        private RelayCommand<object> _canselCommand;
        #endregion
        #endregion

        #region Properties
        public Hotel SelectedHotel { get; set; }
        public ObservableCollection<Hotel> Hotels { get; private set; }
        public CollectionViewSource HotelsViewSource { get; private set; }
        public ComboBoxItem SelectedAppartment { get; set; }
        public ComboBoxItem SelectedEating { get; set; }
        public DateTime? DateOfSettlement
        {
            get
            {
                return _dateOfSettlement;
            }
            set
            {
                _dateOfSettlement = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DateOfEviction
        {
            get
            {
                return _dateOfEvivction;
            }
            set
            {
                _dateOfEvivction = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public AddPassesHotelViewModel()
        {
            Hotels = new ObservableCollection<Hotel>();
            this.HotelsViewSource = new CollectionViewSource();
            HotelsViewSource.Source = this.Hotels;
            InitHotels();
        }
        #endregion

        #region Commands

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand<object>(
                           o =>
                           {
                               try
                               {
                                   if(AreFormsFilled())
                                   {
                                       AddPassViewModel add = new AddPassViewModel();
                                       add.AddHotelToPass(StationManager.SelectedPass.PassNumber, SelectedHotel, SelectedAppartment, SelectedEating, DateOfSettlement, DateOfEviction);
                                       MessageBox.Show("Successful!");
                                   }
                               }
                               catch (Exception e)
                               {
                                   throw e;
                               }
                           }));
            }
        }

        public RelayCommand<Object> CanselCommand
        {
            get
            {
                return _canselCommand ?? (_canselCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.PassView)));
            }
        }
        #endregion

        private void InitHotels()
        {
            List<string> existingHotels = new List<string>();
            string sql = $"SELECT DISTINCT hotel_code FROM include WHERE pass_number = {StationManager.SelectedPass.PassNumber}";
            MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection);
            using (MySqlDataReader reader = comand.ExecuteReader())
            {
                while (reader.Read())
                {
                    existingHotels.Add(reader.GetString(0));
                }
            }
            foreach(Hotel hotel in StationManager.DataStorage.GetHotels())
            {
                if(!existingHotels.Contains(hotel.HotelCode))
                {
                    Hotels.Add(hotel);
                }
            }
        }

        private bool AreFormsFilled()
        {
            if (!DateOfEviction.HasValue)
            {
                MessageBox.Show("Date of eviction doesnt exists!");
                return false;
            }
            if (!DateOfEviction.HasValue)
            {
                MessageBox.Show("Date of eviction doesnt exists!");
                return false;
            }
            if (string.IsNullOrWhiteSpace((string)SelectedAppartment.Content))
            {
                MessageBox.Show("Type of appartment doesnt exists!");
                return false;
            }
            if (string.IsNullOrWhiteSpace((string)SelectedEating.Content))
            {
                MessageBox.Show("Type of eating doesnt exists!");
                return false;
            }
            if (SelectedHotel == null)
            {
                MessageBox.Show("No hotel selected!");
                return false;
            }
            return true;
        }
    }
}
