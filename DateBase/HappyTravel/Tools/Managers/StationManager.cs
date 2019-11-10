using HappyTravel.DataStorage;
using HappyTravel.Models;
using HappyTravel.Tools.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HappyTravel.Tools.Managers
{
    internal static class StationManager
    {
        private static IDataStorage _dataStorage;

        internal static User CurrentUser { get; set; }
        internal static Client SelectedClient { get; set; }
        internal static Pass SelectedPass { get; set; }
        internal static Trip SelectedTrip { get; set; }
        internal static Hotel SelectedHotel { get; set; }
        internal static Resort SelectedResort { get; set; }
        internal static ViewType Previous { get; set; }
        internal static string UserControl { get; set; }
        internal static string UserButtons { get; set; }
        internal static string AdminButtons { get; set; }


        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            ConnectionManager.Connection.Close();
            Environment.Exit(1);
        }
    }
}
