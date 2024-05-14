namespace Services
{
    public class ScopedService : IScopedService 
    { 
        private readonly DateTime _creationTime;

        public ScopedService()
        {
            _creationTime = DateTime.Now;
        }

        public DateTime GetDateTime() => _creationTime;
    }
}