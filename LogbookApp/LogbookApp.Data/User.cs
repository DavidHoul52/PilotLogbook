using System;
using System.Runtime.Serialization;
using BaseData;
using OnlineOfflineSyncLibrary;


namespace LogbookApp.Data
{
    public class User : IUser
    {
        private DateTime? _timeStamp;

        public User()
        {
            
        }

        public User(User otherUser)
        {
            this.DisplayName = otherUser.DisplayName;
            this.IsNew = otherUser.IsNew;
            this.TimeStamp = otherUser.TimeStamp;
            this.id = otherUser.id;
        }

        public string DisplayName { get; set; }

          [IgnoreDataMember]
        public bool IsNew { get; set; }
        public bool Valid()
        {
            return true;
        }

        public int id { get; set; }

        public DateTime? TimeStamp
        {
            get { return _timeStamp; }
            set
            {
                _timeStamp = value;
            }
        }
    }
}
