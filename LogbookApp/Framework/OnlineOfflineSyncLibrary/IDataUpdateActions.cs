using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary
{
    public interface IDataUpdateActions
    {
        Task Update<T>(T item) where T : IEntity;
        Task Insert<T>(T item) where T : IEntity;
        Task Delete<T>(T item) where T : IEntity;
    }
}