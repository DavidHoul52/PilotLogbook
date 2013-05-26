using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data.Validation
{
    public static class ValidationExtensions
    {
        public static bool Valid<T>(this T entity)
            where T : IEntity
        {

            var validator = GetValidator(entity);
            if (validator != null)
                return validator.Valid();
            return false;
        }

        private static IValidator GetValidator<T>(T entity)
            where T : IEntity
        {
            if (typeof (T) == typeof (Flight))
                return new FlightValidator(entity);
            return null;
        }

        public static string ValidationMessage<T>(this T entity)
            where T : IEntity
        {
            var validator = GetValidator(entity);
            if (validator != null)
                return validator.ValidationMessage();
            return "";
        }
    }
}
