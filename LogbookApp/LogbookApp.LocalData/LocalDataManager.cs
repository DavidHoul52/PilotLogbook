namespace LogbookApp.Storage
{
    public class LocalDataManager 
    {
        private readonly ILocalStorage _localStorage;

        public LocalDataManager(ILocalStorage localStorage)
        {
            _localStorage = localStorage;
        }
    }
}
