using System.Threading.Tasks;

namespace LogbookApp
{
    public class LocalStorageBase
    {
         public virtual async Task Save<T>(T data, string filename)
         {
             Exists = true;
         }

         public bool Exists { get; protected set; }
    }
}