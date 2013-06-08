using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public enum DataType
    {
        OffLine,
        OnLine
    };


    public interface IFlightDataService
    {
        DataType DataType { get; }

        
        Task<bool> UserDataExists(string displayName);
        DateTime? LastUpdated { get; }
        

        Task<Lookups> GetLookups();
        //Task<User> GetUser();
        Task<ObservableCollection<Flight>> GetFlights();
      
        Task CreateUserData(FlightData flightData, DateTime now);
        
        bool FlightsChanged { get; set; }
        User User { get; }


        Task UpdateUser(User user);


        Task SetUserData(string displayName);
    }
}
