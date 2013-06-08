using System;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class UserManager 
    {
        

        
        public User CreateUser(string displayName, DateTime now)
        {

            return new User { DisplayName = displayName, IsNew = true,TimeStamp = now};

              
        }

        
    }
}