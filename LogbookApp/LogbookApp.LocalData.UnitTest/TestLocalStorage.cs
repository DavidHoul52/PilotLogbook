

using System.Threading.Tasks;

namespace LogbookApp.LocalData.UnitTest
{
    public class TestLocalStorage : ILocalStorage
    {
        public Task Save<T>(T data, string filename)
        {
            return null;
        }

        public Task<T> Restore<T>(string filename)
        {
            return null;
        }
    }
}
