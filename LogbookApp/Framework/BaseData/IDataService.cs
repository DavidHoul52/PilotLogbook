using System.Threading.Tasks;

namespace BaseData
{
    public interface IDataService
    {
       
        Task Update<T>(T item)
        where T : IEntity;

        Task Insert<T>(T item)
            where T : IEntity;

        Task Delete<T>(T item)
              where T : IEntity;
    }
}