using System;
using System.Threading;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Mocks
{
    public class TestLocalStorage : LocalStorageBase, ILocalStorage
    {
        private User _user;
        private DateTime? _timeStamp;


        public override async Task Save<T>(T data, string filename)
        {
            SavedFileName = filename;
            base.Save(data, filename);
            AllSaved = true;

        }

        public async Task<T> Restore<T>(string filename)
            where T: new()
        {

            AllSaved = false;
            if (Exists)
            {
                return new T ();
            }
            else
            {
                return default(T);
            }
        }

        public async Task<User> RestoreUser(string filename)
        {
            return await new Task<User>(()=> new User {TimeStamp = _timeStamp});
        }

        public bool Exists { get; set; }


        public string SavedFileName { get; set; }
        public bool AllSaved { get; set; }

        public void SetExists(bool exists)
        {
            Exists = exists;
        }

        public void SetTimeStamp(DateTime? timeStamp)

        {
            _timeStamp = timeStamp;

        }
    }
}