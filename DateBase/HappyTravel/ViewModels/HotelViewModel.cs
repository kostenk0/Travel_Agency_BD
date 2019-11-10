using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HappyTravel.ViewModels
{
    internal class HotelViewModel
    {
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _showDescribe;


        public ObservableCollection<Hotel> Hotels { get; private set; }
        public CollectionViewSource ViewSource { get; private set; }
        public Hotel SelectedHotel { get; set; }
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

        #region Constructors
        internal HotelViewModel()
        {
            Hotels = StationManager.DataStorage.GetHotels();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Hotels;
        }
        #endregion

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

        public RelayCommand<object> ShowDescribe
        {
            get
            {
                return _showDescribe ?? (_showDescribe = new RelayCommand<object>(
                           o =>
                           {
                               StationManager.SelectedHotel = SelectedHotel;
                               StationManager.Previous = ViewType.HotelView;
                               NavigationManager.Instance.Navigate(ViewType.HotelDescribeView);
                           }));
            }
        }
    }
}
