using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System.Windows;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddResortViewModel: BaseViewModel
    {
        private string _resortCode;
        private string _title;
        private string _country;

        private RelayCommand<object> _okCommand;
        private RelayCommand<object> _cancelCommand;

        public string ResortCode
        {
            get
            {
                return _resortCode;
            }
            set
            {
                _resortCode = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.ResortView)));
            }
        }

        public RelayCommand<object> OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand<object>(
                           o =>
                           {
                               AddResort();
                           }));
            }
        }

        private void AddResort()
        {
            if (AreFormsFilled())
            {
                using (MySqlCommand comm = ConnectionManager.Connection.CreateCommand())
                {
                    comm.CommandText = "INSERT INTO resort (resort_code, title, country) VALUES(?resort_code, ?title, ?country)";
                    comm.Parameters.Add("?resort_code", MySqlDbType.VarChar).Value = ResortCode;
                    comm.Parameters.Add("?title", MySqlDbType.VarChar).Value = Title;
                    comm.Parameters.Add("?country", MySqlDbType.VarChar).Value = Country;
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Successful add resort!");
                    StationManager.DataStorage.AddResort(new Resort(ResortCode, Title, Country));
                }
            }
        }

        private bool AreFormsFilled()
        {
            if (string.IsNullOrWhiteSpace(ResortCode))
            {
                MessageBox.Show("Resort code is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Title))
            {
                MessageBox.Show("Title is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Country))
            {
                MessageBox.Show("Country is empty!");
                return false;
            }
            return true;
        }
    }
}
