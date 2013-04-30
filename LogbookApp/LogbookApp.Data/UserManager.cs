using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class UserManager : IUserManager
    {
        

        public string DisplayName { get; set; }
        public async Task GetUser(IFlightDataService flightDataService)
        {

            await flightDataService.GetUser(DisplayName);
            if (flightDataService.User == null)
            {
                await flightDataService.InsertUser(new User {DisplayName = this.DisplayName});
                User = flightDataService.User;
            }
            else
            {
                User = flightDataService.User;
            }
        }

        public User User { get; set; }
    }
}