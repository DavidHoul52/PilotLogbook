using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IFlightDataManager
    {
        FlightData FlightData { get; set; }
    
        bool FlightsChanged { get; set; }
        Task LoadData();
        Task GetLookups();
        
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
       
  
        Task GetFlights();
        

        Task DeleteAcType(AcType item, DateTime upDateTime);
        Task StartUp(string displayName);
    }
}