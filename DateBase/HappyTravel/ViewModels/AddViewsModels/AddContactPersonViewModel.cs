using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddContactPersonViewModel: BaseViewModel
    {
        private int _contactPersonCode;
        private string _surname;
        private string _name;
        private string _fathersName;
        private string _email;
        private RelayCommand<object> _okCommand;
        private RelayCommand<object> _cancelCommand;


        public int ContactPersonCode
        {
            get
            {
                return _contactPersonCode;
            }
            set
            {
                _contactPersonCode = value;
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

        private bool AreFormsFilled()
        {
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

        public RelayCommand<object> OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand<object>(
                           o =>
                           {
                               AddContactPerson();
                           }));
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.ResortView)));
            }
        }

        private void AddContactPerson()
        {
            if (AreFormsFilled())
            {
                using (MySqlCommand comm = ConnectionManager.Connection.CreateCommand())
                {
                    comm.CommandText = "INSERT INTO contact_person(surname, name, fathers_name, email, resort_code) " +
                        "VALUES(?surname, ?name, ?fathers_name, ?email, ?resort_code)";
                    comm.Parameters.Add("?surname", MySqlDbType.VarChar).Value = Surname;
                    comm.Parameters.Add("?name", MySqlDbType.VarChar).Value = Name;
                    comm.Parameters.Add("?fathers_name", MySqlDbType.VarChar).Value = FathersName;
                    comm.Parameters.Add("?email", MySqlDbType.VarChar).Value = Email;
                    comm.Parameters.Add("?resort_code", MySqlDbType.VarChar).Value = StationManager.SelectedResort.ResortCode;
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Successful add contact person!");
                }
            }
        }
    }
}
