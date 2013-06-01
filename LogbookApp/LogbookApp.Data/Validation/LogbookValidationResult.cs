namespace LogbookApp.Data.Validation
{
    public class LogbookValidationResult
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
        public static LogbookValidationResult Ok
        {
            get
            {
                return new LogbookValidationResult { Valid = true };
            }
        }
    }
}