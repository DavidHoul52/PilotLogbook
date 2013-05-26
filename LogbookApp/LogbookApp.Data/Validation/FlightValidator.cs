using System;

namespace LogbookApp.Data.Validation
{
    public class FlightValidator : IValidator
    {
        private readonly IEntity _entity;

        public FlightValidator(IEntity entity)
        {
            _entity = entity;
        }

        public bool Valid()
        {
            return false;
        }

        public string ValidationMessage()
        {
            return "oops!";
        }
    }
}