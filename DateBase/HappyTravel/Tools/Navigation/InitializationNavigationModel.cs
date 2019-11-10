using HappyTravel.Views;
using HappyTravel.Views.AddWindows;
using HappyTravel.Views.Authentication;
using System;

namespace HappyTravel.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void ReinitializeView(ViewType viewType)
        {
            ViewsDictionary.Remove(viewType);
            InitializeView(viewType);
        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.SignIn:
                    ViewsDictionary.Add(viewType, new SignInView());
                    break;
                case ViewType.SignUp:
                    ViewsDictionary.Add(viewType, new SignUpView());
                    break;
                case ViewType.MainManager:
                    ViewsDictionary.Add(viewType, new MainViewManager());
                    break;
                case ViewType.ClientsView:
                    ViewsDictionary.Add(viewType, new ClientsView());
                    break;
                case ViewType.ContractView:
                    ViewsDictionary.Add(viewType, new ContractView());
                    break;
                case ViewType.PassView:
                    ViewsDictionary.Add(viewType, new PassView());
                    break;
                case ViewType.AddClientView:
                    ViewsDictionary.Add(viewType, new AddClientView());
                    break;
                case ViewType.AddPhoneView:
                    ViewsDictionary.Add(viewType, new AddPhoneView());
                    break;
                case ViewType.ClientsPhonesView:
                    ViewsDictionary.Add(viewType, new ClientsPhonesView());
                    break;
                case ViewType.AddContract:
                    ViewsDictionary.Add(viewType, new AddContractView());
                    break;
                case ViewType.ClientsContracts:
                    ViewsDictionary.Add(viewType, new ClientsContractsView());
                    break;
                case ViewType.TripView:
                    ViewsDictionary.Add(viewType, new TripView());
                    break;
                case ViewType.HotelView:
                    ViewsDictionary.Add(viewType, new HotelView());
                    break;
                case ViewType.ResortView:
                    ViewsDictionary.Add(viewType, new ResortView());
                    break;
                case ViewType.ContactPersonView:
                    ViewsDictionary.Add(viewType, new ContactPersonView());
                    break;
                case ViewType.AddPass:
                    ViewsDictionary.Add(viewType, new AddPass());
                    break;
                case ViewType.PassesHotels:
                    ViewsDictionary.Add(viewType, new PassesHotelsView());
                    break;
                case ViewType.PassesTrips:
                    ViewsDictionary.Add(viewType, new PassesTripsView());
                    break;
                case ViewType.AddPassesHotel:
                    ViewsDictionary.Add(viewType, new AddPassesHotelView());
                    break;
                case ViewType.AddPassesTrip:
                    ViewsDictionary.Add(viewType, new AddPassesTripView());
                    break;
                case ViewType.AddTripView:
                    ViewsDictionary.Add(viewType, new AddTripView());
                    break;
                case ViewType.AddTicketView:
                    ViewsDictionary.Add(viewType, new AddTicketView());
                    break;
                case ViewType.TripTicketsView:
                    ViewsDictionary.Add(viewType, new TripTicketsView());
                    break;
                case ViewType.AddResortView:
                    ViewsDictionary.Add(viewType, new AddResortView());
                    break;
                case ViewType.AddContactPersonView:
                    ViewsDictionary.Add(viewType, new AddContactPersonView());
                    break;
                case ViewType.ResortContactPersonsView:
                    ViewsDictionary.Add(viewType, new ResortContactPersonsView());
                    break;
                case ViewType.HotelDescribeView:
                    ViewsDictionary.Add(viewType, new HotelDescribeView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
