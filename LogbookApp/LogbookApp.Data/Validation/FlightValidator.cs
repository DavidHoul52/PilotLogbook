using System;

namespace LogbookApp.Data.Validation
{
    public class FlightValidator : IValidator
    {
        private readonly Flight _flight;
        private string _validationMessage;

        public FlightValidator(Flight entity)
        {
            _flight = entity;
        }

        

        public LogbookValidationResult GetValidationResult()
        {
           if (_flight.Aircraft == null)
                   return new LogbookValidationResult { Valid = false, Message = "Please select an aircraft (or add a new one)" };
            if (_flight.Capacity == null)
                   return new LogbookValidationResult { Valid = false,
                                                        Message = "Please select the Captain's capacity"
                   };
            
            if (_flight.From == null)
                return new LogbookValidationResult { Valid = false, Message = "Please select a departure airfield (or add a new one)" };
            if (_flight.To == null)
                return new LogbookValidationResult { Valid = false, Message = "Please select an arrival airfield (or add a new one)" };
            if (_flight.Arrival<=_flight.Depart)
                return new LogbookValidationResult { Valid = false, Message = 
                    "Please select an arrival time later than the departure time" };

            
            return LogbookValidationResult.Ok;
        }
    }
}