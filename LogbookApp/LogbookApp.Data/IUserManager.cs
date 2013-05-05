using System;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IUserManager
    {
     

        string DisplayName { get; set; }

        Task GetUser(IFlightDataService flightDataService, DateTime now);

        User User { get; set; }
    }
}
