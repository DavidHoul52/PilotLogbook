using BaseData;

namespace OnlineOfflineSyncLibrary
{
    public interface IUser : IEntity
    {
        string DisplayName { get; set; }
    }
}