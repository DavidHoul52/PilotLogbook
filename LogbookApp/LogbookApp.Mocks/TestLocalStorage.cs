using System.Threading.Tasks;

namespace LogbookApp.Mocks
{
    public class TestLocalStorage : LocalStorageBase, ILocalStorage
    {
        

        public override async Task Save<T>(T data, string filename)
        {
            SavedFileName = filename;
            base.Save(data, filename);


        }

        public async Task<T> Restore<T>(string filename)
            where T:new()
        {
            if (Exists)
              return new T();
            else
            {
                return default(T);
            }
        }

     

        public string SavedFileName { get; set; }

        public void SetExists(bool exists)
        {
            Exists = exists;
        }
    }
}