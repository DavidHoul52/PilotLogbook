using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;

namespace LogbookApp
{
    class LocalStorage
    {
      

        public static StorageFile file { get; set; }

        
        public async Task Save<T>(T data, string filename)
        {
            if (await DoesFileExistAsync(filename))
            {
                await Windows.System.Threading.ThreadPool.RunAsync((sender) =>
                    SaveAsync<T>(data, filename).Wait(), Windows.System.Threading.WorkItemPriority.Normal);
            }
            else
            {
                file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            }
        }

        async public Task<T> Restore<T>(string filename)
        {
            if (await DoesFileExistAsync(filename))
            {
                await Windows.System.Threading.ThreadPool.RunAsync((sender) => RestoreAsync<T>(filename).Wait(),
                    Windows.System.Threading.WorkItemPriority.Normal);
            }
            else
            {
                file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename);
            }
            return default(T);
        }

        static async Task<bool> DoesFileExistAsync(string fileName)
        {
            try
            {
                await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
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

         static async private Task RestoreAsync<T>( string filename)
         {
             StorageFile sessionFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
             if (sessionFile == null)
             {
                 return;
             }
             IInputStream sessionInputStream = await sessionFile.OpenReadAsync();

             //Using DataContractSerializer , look at the cat-class
              var sessionSerializer = new DataContractSerializer(typeof(T));
             var data = sessionSerializer.ReadObject(sessionInputStream.AsStreamForRead());

             //Using XmlSerializer , look at the Dog-class
             //var serializer = new XmlSerializer(typeof(List<object>), new Type[] { typeof(T) });
             //_data = (List<object>)serializer.Deserialize(sessionInputStream.AsStreamForRead());
             sessionInputStream.Dispose();
         }
    }
}
