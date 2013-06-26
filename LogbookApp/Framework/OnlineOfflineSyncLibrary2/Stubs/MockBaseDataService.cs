﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace OnlineOfflineSyncLibrary2.Stubs
{
    public abstract class MockBaseDataService<TSyncableData,TUser> : DataService<TSyncableData,TUser>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>,new()
    {
        protected TSyncableData InternalData;
        protected readonly string UserName;
        private bool _userDataExists;

        protected MockBaseDataService(string userName)
        {
            
            UserName = userName;
        }

        public async Task Update<T>(T item) where T : IEntity
        {

        }

        public abstract Task Insert<T>(T item) where T : IEntity;
        

        public async Task Delete<T>(T item) where T : IEntity
        {

        }

        public bool LoadUserDataCalled { get; set; }
        public bool CreateUserDataCalled { get; set; }

        public async Task<TUser> GetUser(string userName)
        {
            return InternalData.User;
        }

        protected async override Task<TSyncableData> InternalLoadUserData(string userName)
        {
            LoadUserDataCalled = true;
            return InternalData;
        }

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

     
    }
}
