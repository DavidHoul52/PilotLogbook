using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary
{
    public interface ISyncableData<TUser>
        where TUser : IUser
    {
        TUser User { get; set; }
    }
}
