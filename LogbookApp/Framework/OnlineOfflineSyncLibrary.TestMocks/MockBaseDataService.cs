using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary.TestMocks
{
    public abstract class MockBaseDataService<TSyncableData,TUser> : DataService<TSyncableData,TUser>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>,new()
    {
      
        protected readonly string UserName;
        protected TSyncableData InternalData;
        private bool _userDataExists;

        protected MockBaseDataService(string userName)
        {
            
            UserName = userName;
        }

        public abstract Task Update<T>(T item) where T : IEntity;
      

        public abstract Task Insert<T>(T item) where T : IEntity;


        public abstract Task Delete<T>(T item) where T : IEntity;
      

        public bool LoadUserDataCalled { get; set; }
        public bool CreateUserDataCalled { get; set; }

        public async Task<TUser> GetUser(string userName)
        {
            return InternalData.User;
        }

        protected async override Task<TSyncableData> InternalLoadUserData(string userName)
        {
            LoadUserDataCalled = true;
            return CopyOfInternalData();
        }

        protected abstract TSyncableData CopyOfInternalData();

        protected async override Task InternalCreateUserData(string userName)
        {
            InternalData = new TSyncableData {};
            InternalData.User.DisplayName = userName;
            CreateUserDataCalled = true;
            
        }

       public async Task<bool> GetUserDataExists(string userName)
       {
           return InternalData != null;
       }

        public void SetUserDataExists(bool exists, DateTime? timeStamp)
        {
            if (exists)
               InternalData = InternalCreateUserData(timeStamp);
            else
            {
                InternalData = default(TSyncableData);
            }
        }

        private TSyncableData InternalCreateUserData(DateTime? timeStamp)
        {
            return new TSyncableData { User = new TUser { TimeStamp = timeStamp } };
        }

        public void UpdateMockInternalData(TSyncableData onlineData)
        {
            InternalData = onlineData;
        }

        protected ObservableCollection<T> Copy<T>(IEnumerable<T> items)
         where T : IEntity, new()
        {
            return new ObservableCollection<T>(items.Select(x => CreateCopy(x)));
        }



        protected T CreateCopy<T>(T entity)
            where T : IEntity, new()
        {
            var newEntity = new T();
            foreach (PropertyInfo prop in newEntity.GetType().GetTypeInfo().DeclaredProperties)
            {
                if (prop.CanWrite)
                    prop.SetValue(newEntity, prop.GetValue(entity, null), null);
            }

            return newEntity;
        }
     
    }
}
