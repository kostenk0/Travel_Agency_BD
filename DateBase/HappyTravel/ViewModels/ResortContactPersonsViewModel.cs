using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HappyTravel.ViewModels
{
    internal class ResortContactPersonsViewModel: BaseViewModel
    {
        private RelayCommand<object> _remove;
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _addContactPerson;

        private ObservableCollection<ContactPerson> _contactPersons;
        public CollectionViewSource ViewSource { get; private set; }
        public ObservableCollection<ContactPerson> ContactPersons { get => _contactPersons; set => _contactPersons = value; }
        public ContactPerson SelectedContactPerson { get; set; }
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

        internal ResortContactPersonsViewModel()
        {
            ContactPersons = new ObservableCollection<ContactPerson>();
            ViewSource = new CollectionViewSource();
            ViewSource.Source = ContactPersons;
            Init();
        }

        private void Init()
        {
            try
            {
                string sql = $"SELECT * FROM contact_person WHERE resort_code = {StationManager.SelectedResort.ResortCode}";
                MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection);
                using (MySqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contact_person_code = reader.GetInt32(0);
                        var surname = reader.GetString(1);
                        var name = reader.GetString(2);
                        var fathers_name = reader.GetString(3);
                        var email = reader.GetString(4);
                        ContactPersons.Add(new ContactPerson(contact_person_code, surname, name, fathers_name, email, StationManager.SelectedResort.ResortCode));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.ResortView);
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
                               string sql = $"delete from contact_person where contact_person_code = \"{SelectedContactPerson.ContactPersonCode}\"";
                               using (MySqlCommand comm = new MySqlCommand(sql, ConnectionManager.Connection))
                               {
                                   comm.ExecuteNonQuery();
                                   this.ContactPersons.Remove(SelectedContactPerson);
                               }
                           }));
            }
        }

        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addContactPerson ?? (_addContactPerson = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.AddContactPersonView);
                           }));
            }
        }
    }
}
