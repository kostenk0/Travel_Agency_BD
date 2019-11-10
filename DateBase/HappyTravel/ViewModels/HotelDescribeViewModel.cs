using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;

namespace HappyTravel.ViewModels
{
    internal class HotelDescribeViewModel : BaseViewModel
    {
        private RelayCommand<object> _cancelCommand;

        public Hotel SelectedHotel { get; set; }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(StationManager.Previous)));
            }
        }

        internal HotelDescribeViewModel()
        {
            this.SelectedHotel = StationManager.SelectedHotel;
        }
    }
}