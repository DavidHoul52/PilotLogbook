using System;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class UserManager 
    {
        

        public string DisplayName { get; set; }
        public async Task GetUser(IFlightDataService flightDataService, DateTime now)
        {

            User = await flightDataService.GetUser(DisplayName);
            if (User == null)
            {
                User = new User {DisplayName = this.DisplayName};
                await flightDataService.InsertUser(User);
                
            }
            

            if (User.TimeStamp == null)
                User.TimeStamp = now;
        }

        public User User { get; set; }
    }
}