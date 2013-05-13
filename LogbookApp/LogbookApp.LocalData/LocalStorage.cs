using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation.Metadata;
using Windows.Media.Devices;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;
using Windows.UI.Text;
using Windows.UI.Xaml.Automation;
using LogbookApp.Data;

namespace LogbookApp
{
    public class LocalStorage : LocalStorageBase,  ILocalStorage
    {
      
        public override async Task Save<T>(T data, string filename)
        {
            var file = await GetFileAsync(filename);
            if (file==null)
                file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            if (file!=null)
            {
                await Windows.System.Threading.ThreadPool.RunAsync((sender) =>
                    SaveAsync<T>(data, filename).Wait(), Windows.System.Threading.WorkItemPriority.Normal);
            }
            
            base.Save(data, filename);
        }

        async public Task<T> Restore<T>(string filename)
                where T : new()
        {
            var file = await GetFileAsync(filename);
            T result = default(T);
            if (file!=null)
            {
                try
                {
                    result = await RestoreAsync<T>(filename);
                    //await Windows.System.Threading.ThreadPool.RunAsync((sender) =>
                    //{
                      
                    //},
                    //Windows.System.Threading.WorkItemPriority.Normal);
                }
                catch (Exception)
                {

                    return result;
                }
                
            }

            return result;
        }

        public async Task<User> RestoreUser(string filename)
        {
            var user = await Restore<User>(filename);
            return user;
        }


        static async Task<StorageFile> GetFileAsync(string fileName)
        {
            try
            {
                return await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                
            }
            catch
            {
                return null;
            }
        }

         async private Task SaveAsync<T>(T data, string filename)
        {
            StorageFile sessionFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream sessionRandomAccess = await sessionFile.OpenAsync(FileAccessMode.ReadWrite);
            IOutputStream sessionOutputStream = sessionRandomAccess.GetOutputStreamAt(0);
            //var serializer = new XmlSerializer(typeof(List<object>), new Type[] { typeof(T) });
            var serializer = new XmlSerializer(typeof(T));

            //Using DataContractSerializer , look at the cat-class
            var sessionSerializer = new DataContractSerializer(typeof(T));
            sessionSerializer.WriteObject(sessionOutputStream.AsStreamForWrite(), data);

            //Using XmlSerializer , look at the Dog-class
            //serializer.Serialize(sessionOutputStream.AsStreamForWrite(), data);
            sessionRandomAccess.Dispose();
            await sessionOutputStream.FlushAsync();
            sessionOutputStream.Dispose();
        }

         static async private Task<T> RestoreAsync<T>( string filename)
             
         {
             
             StorageFile sessionFile = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
             if (sessionFile == null)
             {
                 return default(T);
             }
             IInputStream sessionInputStream = await sessionFile.OpenReadAsync();

             //Using DataContractSerializer , look at the cat-class
             var sessionSerializer = new DataContractSerializer(typeof (T),
                 new DataContractSerializerSettings
                  {});
             var data = (T)sessionSerializer.ReadObject(sessionInputStream.AsStreamForRead());

             //Using XmlSerializer , look at the Dog-class
             //var serializer = new XmlSerializer(typeof(List<object>), new Type[] { typeof(T) });
             //_data = (List<object>)serializer.Deserialize(sessionInputStream.AsStreamForRead());
             sessionInputStream.Dispose();
             return data;
         }
    }
}
