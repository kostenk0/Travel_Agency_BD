using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddClientViewModel : BaseViewModel
    {
        #region Fields
        private string _pasportNumber;
        private string _surname;
        private string _name;
        private string _fathersName;
        private DateTime? _birthDate;
        private int _age;
        private string _email;
        private string _phoneNumber;

        #region Commands
        private RelayCommand<object> _okCommand;
        private RelayCommand<object> _canselCommand;
        #endregion
        #endregion

        #region Properties
        public string PasportNumber
        {
            get
            {
                return _pasportNumber;
            }
            set
            {
                _pasportNumber = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string FathersName
        {
            get
            {
                return _fathersName;
            }
            set
            {
                _fathersName = value;
                OnPropertyChanged();
            }
        }
        public DateTime? BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
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
                               try
                               {
                                   AddClient();
                               }
                               catch (Exception e)
                               {
                                   MessageBox.Show(e.Message);
                               }
                           }));
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
        private bool AreFormsFilled()
        {
            if (string.IsNullOrWhiteSpace(PasportNumber))
            {
                MessageBox.Show("PasportNumber is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Surname))
            {
                MessageBox.Show("Surname is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Name is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(FathersName))
            {
                MessageBox.Show("Fathers name is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("E-mail is empty!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                MessageBox.Show("Phone is empty!");
                return false;
            }
            if (!_birthDate.HasValue)
            {
                MessageBox.Show("Birth date is empty!");
                return false;
            }
            if(!IsPhoneNumber(PhoneNumber))
            {
                MessageBox.Show("Not valid phone number");
                return false;
            }
            try
            {
                MailAddress m = new MailAddress(Email);
            }
            catch (FormatException)
            {
                MessageBox.Show("Not valid email!");
                return false;
            }
            return true;
        }

        private void CalcAge()
        {
            if (DateTime.Today.Month <= BirthDate.Value.Month)
            {
                if (DateTime.Today.Day <= BirthDate.Value.Day)
                    _age = DateTime.Today.Year - BirthDate.Value.Year;
            }
            else
                _age = DateTime.Today.Year - BirthDate.Value.Year - 1;
        }

        private void AddPhone()
        {
            using (MySqlCommand comm = ConnectionManager.Connection.CreateCommand())
            {
                try
                {
                    comm.CommandText = "INSERT INTO phonenumber(phone_number, client_code) VALUES(?phone_number, ?client_code)";
                    comm.Parameters.Add("?phone_number", MySqlDbType.VarChar).Value = PhoneNumber;
                    comm.Parameters.Add("?client_code", MySqlDbType.Int32).Value = GetLastClientCode();
                    comm.ExecuteNonQuery();
                }
                catch (MySql.Data.MySqlClient.MySqlException)
                {

                    throw new Exception("Number is alredy exists!");
                }
            }
        }

        private int GetLastClientCode()
        {
            int lastClientCode = 1;
            string sql = "SELECT MAX(client_code) FROM client";
            using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
            {
                MySqlDataReader dr = comand.ExecuteReader();
                if (dr.HasRows == true)
                {
                    dr.Read();
                    lastClientCode = dr.GetInt32(0);
                }
                return lastClientCode; 
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(?<countryCode>[\+][1-9]{1}[0-9]{0,2}\s)?(?<areaCode>0?[1-9]\d{0,4})(?<number>\s[1-9][\d]{5,12})(?<extension>\sx\d{0,4})?$").Success;
        }

        private void AddClient()
        {
            if (AreFormsFilled())
            {
                CalcAge();
                if (_age < 18)
                {
                    MessageBox.Show(BirthDate.ToString());
                    return;
                }
                using (MySqlCommand comm = ConnectionManager.Connection.CreateCommand())
                {
                    comm.CommandText = "INSERT INTO client(pasport_number, surname, name, fathers_name, birth_date, age, email) VALUES(?pasport_number, ?surname, ?name, ?fathers_name, ?birth_date, ?age, ?email)";
                    comm.Parameters.Add("?pasport_number", MySqlDbType.VarChar).Value = PasportNumber;
                    comm.Parameters.Add("?surname", MySqlDbType.VarChar).Value = Surname;
                    comm.Parameters.Add("?name", MySqlDbType.VarChar).Value = Name;
                    comm.Parameters.Add("?fathers_name", MySqlDbType.VarChar).Value = FathersName;
                    comm.Parameters.Add("?birth_date", MySqlDbType.DateTime).Value = BirthDate;
                    comm.Parameters.Add("?age", MySqlDbType.Int32).Value = _age;
                    comm.Parameters.Add("?email", MySqlDbType.VarChar).Value = Email;
                    comm.ExecuteNonQuery();
                    AddPhone();
                    MessageBox.Show("Successful!");
                    StationManager.DataStorage.AddClient(new Client(GetLastClientCode(), PasportNumber, Surname, Name, FathersName, BirthDate.Value, _age, Email)); 
                }
            }
        }
    }
}