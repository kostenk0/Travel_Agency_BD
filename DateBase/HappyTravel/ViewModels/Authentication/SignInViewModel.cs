using HappyTravel.DataStorage;
using HappyTravel.Models;
using HappyTravel.Tools;
using HappyTravel.Tools.Managers;
using HappyTravel.Tools.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HappyTravel.ViewModels
{
    internal class SignInViewModel : BaseViewModel
    {
        #region Fields
        private string _login;
        private string _password;

        #region Commands
        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _signUpCommand;
        private RelayCommand<object> _closeCommand;
        #endregion
        #endregion

        #region Properties
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value.Replace(" ", "Space");
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands

        public RelayCommand<object> SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(
                           o =>
                           {
                               if(string.IsNullOrWhiteSpace(_login))
                               {
                                   MessageBox.Show("Login is empty!");
                                   return;
                               }
                               if(string.IsNullOrWhiteSpace(_password))
                               {
                                   MessageBox.Show("Password is empty!");
                                   return;
                               }
                               string sql = "SELECT * FROM users WHERE login=@login and password=@password";
                               using (MySqlCommand comand = new MySqlCommand(sql, ConnectionManager.Connection))
                               {
                                   comand.Parameters.AddWithValue("@login", Login);
                                   comand.Parameters.AddWithValue("@password", Password);
                                   using (MySqlDataReader dr = comand.ExecuteReader())
                                   {
                                       if (dr.HasRows == true)
                                       {
                                           dr.Read();
                                           var login = dr.GetString(0);
                                           var password = dr.GetString(1);
                                           StationManager.CurrentUser = new User(login, password);
                                           NavigationManager.Instance.Navigate(ViewType.MainManager);
                                       }
                                       else
                                       {
                                           MessageBox.Show($"User with {Login} doesnt exist!");
                                       }  
                                   }
                                   if(StationManager.CurrentUser != null)
                                   {
                                       if(Login != "admin")
                                       {
                                           StationManager.UserControl = "Collapsed";
                                           StationManager.AdminButtons = "Collapsed";
                                           StationManager.UserButtons = "Visible";
                                       }
                                       else
                                       {
                                           StationManager.UserControl = "Visible";
                                           StationManager.AdminButtons = "Visible";
                                           StationManager.UserButtons = "Collapsed";
                                       }
                                       StationManager.Initialize(new SerializedDataStorage());
                                   }
                               }
                           }));
            }
        }

        public RelayCommand<Object> SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(
                           o =>
                           {
                               NavigationManager.Instance.Navigate(ViewType.SignUp);
                           }));
            }
        }

        public RelayCommand<Object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(o => StationManager.CloseApp()));
            }
        }

        #endregion
    }
}