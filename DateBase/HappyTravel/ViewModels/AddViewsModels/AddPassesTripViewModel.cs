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
using System.Windows;
using System.Windows.Data;

namespace HappyTravel.ViewModels.AddViewsModels
{
    internal class AddPassesTripViewModel : BaseViewModel
    {
        #region Fields
        #region Commands
        private RelayCommand<object> _okCommand;
        private RelayCommand<object> _canselCommand;
        #endregion
        #endregion

        #region Properties
        public Trip SelectedTrip { get; set; }
        public ObservableCollection<Trip> Trips { get; private set; }
        public CollectionViewSource TripsViewSource { get; private set; }
        #endregion

        #region Constructors
        public AddPassesTripViewModel()
        {

            Trips = new ObservableCollection<Trip>();
            AddPassViewModel model = new AddPassViewModel();
            foreach (Trip trip in StationManager.DataStorage.GetTrips())
            {
                if (model.GetTicketsCountInTrip(trip) >= StationManager.SelectedPass.NumberOfPeople)
                {
                    Trips.Add(trip);
                }
            }
            this.TripsViewSource = new CollectionViewSource();
            TripsViewSource.Source = this.Trips;
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
                                   if (SelectedTrip != null)
                                   {
                                       AddPassViewModel add = new AddPassViewModel();
                                       add.AddTicketsToPass(SelectedTrip, StationManager.SelectedPass.PassNumber, StationManager.SelectedPass.NumberOfPeople);
                                       MessageBox.Show("Successful!");
                                   }
                                   else
                                   {
                                       MessageBox.Show("No trip selected!");
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
    }
}
