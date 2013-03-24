using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IUserManager
    {
     

        string DisplayName { get; set; }

        Task GetUser(IFlightDataService flightDataService);

        User User { get; set; }
    }
}
