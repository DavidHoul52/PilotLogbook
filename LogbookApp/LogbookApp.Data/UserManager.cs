using System;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class UserManager 
    {
        

        public string DisplayName { get; set; }
        public async Task<User> GetUser(IFlightDataService flightDataService, DateTime now)
        {

            User = await flightDataService.GetUser(DisplayName);
            if (User == null)
            {
                User = new User {DisplayName = this.DisplayName, IsNew = true};
                await flightDataService.InsertUser(User);
                
            }
            

            if (User.TimeStamp == null)
                User.TimeStamp = now;
            return User;
        }

        public User User { get; set; }
    }
}