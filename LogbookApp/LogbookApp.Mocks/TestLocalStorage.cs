using System.Threading.Tasks;

namespace LogbookApp.Mocks
{
    public class TestLocalStorage : LocalStorageBase, ILocalStorage
    {
        

        public override async Task Save<T>(T data, string filename)
        {
            SavedFileName = filename;
            base.Save(data, filename);
            AllSaved = true;

        }

        public async Task<T> Restore<T>(string filename)
            where T:new()
        {

            AllSaved = false;
            if (Exists)
              return new T();
            else
            {
                return default(T);
            }
        }

     

        public string SavedFileName { get; set; }
        public bool AllSaved { get; set; }

        public void SetExists(bool exists)
        {
            Exists = exists;
        }
    }
}