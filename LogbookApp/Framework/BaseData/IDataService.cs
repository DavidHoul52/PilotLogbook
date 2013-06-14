using System.Threading.Tasks;

namespace BaseData
{
    public enum DataType
    {
        OffLine,
        OnLine
    };


    public interface IDataService
    {

        DataType DataType { get; }
       
        Task Update<T>(T item)
        where T : IEntity;

        Task Insert<T>(T item)
            where T : IEntity;

        Task Delete<T>(T item)
              where T : IEntity;

        bool LoadUserData(string userName);

        void CreateUserData(string userName);
    }
}