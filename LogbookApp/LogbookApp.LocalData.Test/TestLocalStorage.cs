using System.Threading.Tasks;
using Windows.Storage.Pickers.Provider;
using LogbookApp.Data;

namespace LogbookApp.LocalData.Test
{
    public class TestLocalStorage : ILocalStorage
    {
        public async Task Save<T>(T data, string filename)
        {
            SavedFileName = filename;
        }

        public async Task<T> Restore<T>(string filename)
            where T:new()
        {
            return new T();
        }

        public string SavedFileName { get; set; }
    }
}