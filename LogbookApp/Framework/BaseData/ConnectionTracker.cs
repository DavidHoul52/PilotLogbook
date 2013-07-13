namespace BaseData
{
    public class ConnectionTracker
    {
        private bool _isConnected;
        

        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                if (!value)
                    Synced = false;
            }
        }

        public bool Synced { get; set; }

        public bool UpForSyncing
        {
            get
            {
                return _isConnected && !Synced;
            }
            
        }
    }
}