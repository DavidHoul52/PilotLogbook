using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp
{
    public interface ILocalStorage
    {
        Task Save<T>(T data, string filename);

        Task<T> Restore<T>(string filename) where T: new();

        Task<User> RestoreUser(string filename);

      //  bool Exists { get;  }
    }
}