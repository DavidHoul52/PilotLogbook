using System.Data.Services;
using System.Data.Services.Common;
using WCFData.EntityFramework;

namespace WCFData.WCFService
{
    public class PilotsLogBookData : DataService<PilotsLogBookContainer>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            config.SetEntitySetAccessRule("Flights", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("AcTypes", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Airfields", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Capacities", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}
