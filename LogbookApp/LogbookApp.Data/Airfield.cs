namespace LogbookApp.Data
{
    public class Airfield :Entity
    {

        public int Id { get; set; }

        public string ICAOCode { get; set; }

        public string Name { get; set; }
        
        public int? UserId { get; set; }

        public override bool Valid()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}
