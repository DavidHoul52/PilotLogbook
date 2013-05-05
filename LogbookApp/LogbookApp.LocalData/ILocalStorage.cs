using System.Threading.Tasks;

namespace LogbookApp
{
    public interface ILocalStorage
    {
        Task Save<T>(T data, string filename);

        Task<T> Restore<T>(string filename)
            where T : new();

        bool Exists { get;  }
    }
}