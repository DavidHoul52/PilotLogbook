using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BaseData;

namespace LogbookApp.Data
{
  


    public interface IFlightDataService : IDataService
    {
       

        
        Task<bool> UserDataExists(string displayName);
        DateTime? LastUpdated { get; }


        Task<Lookups> GetLookups(int userId);

        Task<ObservableCollection<Flight>> GetFlights(int userId);
      
        Task CreateUserData(FlightData flightData, DateTime now);
        
        bool FlightsChanged { get; set; }
      


        Task UpdateUser(User user);


        Task<User> GetUser(string displayName);
    }
}
