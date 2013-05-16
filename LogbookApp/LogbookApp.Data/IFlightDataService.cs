using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public enum DataType
    {
        None,
        OffLine,
        OnLine
    };


    public interface IFlightDataService
    {
        DataType DataType { get; }

        Task<Lookups> GetLookups(int userId);
        Task<User> GetUser(string displayName);
        Task<ObservableCollection<Flight>> GetFlights(int userId);
      
        Task InsertUser(User user);
        
        bool FlightsChanged { get; set; }

        Task<bool> Available(string displayName);
        Task UpdateUser(User user);
     
      
    }
}
