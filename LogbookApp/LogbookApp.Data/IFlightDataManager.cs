using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IFlightDataManager 
    {
        
    
        
        
    
        
        Task DeleteFlight(Flight flight, DateTime updateTime);
        Task SaveFlight(Flight flight, DateTime saveTime);
        Task InsertAircraft(Aircraft aircraft, DateTime upDateTime);
        Task InsertAirfield(Airfield @from, DateTime upDateTime);
        Task UpdateAircraft(Aircraft aircraft, DateTime upDateTime);
        Task DeleteAircraft(Aircraft aircraft, DateTime upDateTime);
        Task UpdateAirfield(Airfield airfield, DateTime upDateTime);
        Task DeleteAirfield(Airfield airfield, DateTime upDateTime);
        Task UpdateAcType(AcType acType, DateTime upDateTime);
        Task InsertAcType(AcType acType, DateTime upDateTime);
       
  
        
        

        Task DeleteAcType(AcType item, DateTime upDateTime);

        FlightData Data { get; }

        Task GetFlights();
        Task LoadData();
        Task GetLookups();
        Task Startup(string userName);
    }
}