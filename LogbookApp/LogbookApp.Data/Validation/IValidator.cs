using System.Diagnostics.Contracts;

namespace LogbookApp.Data.Validation
{
    public interface IValidator
    {
        bool Valid();
        string ValidationMessage();
    }

    
}