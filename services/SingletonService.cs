namespace Services
{
    public class SingletonService : ISingletonService 
    { 
        private readonly DateTime _creationTime;

        public SingletonService()
        {
            _creationTime = DateTime.Now;
        }

        public DateTime GetDateTime() => _creationTime;
    }
}