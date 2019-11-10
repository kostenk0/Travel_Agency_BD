namespace HappyTravel.Tools.Navigation
{
    internal enum ViewType
    {
        SignIn,
        SignUp,
        MainManager,
        ClientsView,
        ContractView,
        PassView,
        AddClientView,
        AddPhoneView,
        ClientsPhonesView,
        AddContract,
        ClientsContracts,
        TripView,
        HotelView,
        ResortView,
        ContactPersonView,
        AddPass,
        PassesHotels,
        PassesTrips,
        AddPassesHotel,
        AddPassesTrip,
        AddTripView,
        AddTicketView,
        TripTicketsView,
        AddResortView,
        AddContactPersonView,
        ResortContactPersonsView,
        HotelDescribeView
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
