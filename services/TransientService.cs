namespace Services
{
    public class TransientService : ITransientService 
    { 
        private readonly DateTime _creationTime;

        public TransientService()
        {
            _creationTime = DateTime.Now;
        }

        public DateTime GetDateTime() => _creationTime;
    }
}