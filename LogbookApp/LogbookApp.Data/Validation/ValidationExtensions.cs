using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;

namespace LogbookApp.Data.Validation
{
    public static class ValidationExtensions
    {
        public static LogbookValidationResult ValidationResult<T>(this T entity)
            where T : IEntity
        {

            var validator = GetValidator(entity);
            if (validator != null)
                return validator.GetValidationResult();

            return LogbookValidationResult.Ok;
            


        }

        private static IValidator GetValidator<T>(T entity)
            where T : IEntity
        {
            if (typeof (T) == typeof (Flight))
                return new FlightValidator(entity as Flight);
            return null;
        }

    
    }
}
