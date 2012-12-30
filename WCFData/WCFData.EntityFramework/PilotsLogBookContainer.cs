using System.Data.Objects;

namespace WCFData.EntityFramework
{
    public partial class PilotsLogBookContainer : ObjectContext
    {
         public PilotsLogBookContainer() : base("name=PilotsLogBookContainer", "PilotsLogBookContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            
        }
    }
}
