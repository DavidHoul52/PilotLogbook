using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    [DataContract]
    public class Lookups :  ILookups
    {
        public Lookups()
        {
            AcTypes = new ObservableCollection<AcType>();
            Capacity = new ObservableCollection<Capacity>();
            Airfields = new ObservableCollection<Airfield>();
            Aircraft = new ObservableCollection<Aircraft>();
        }

        [DataMember]
        public ObservableCollection<AcType> AcTypes { get; set; }
        [DataMember]
        public ObservableCollection<Capacity> Capacity { get; set; }
        [DataMember]
        public ObservableCollection<Airfield> Airfields { get; set; }
        [DataMember]
        public ObservableCollection<Aircraft> Aircraft { get; set; }

     
    }
}