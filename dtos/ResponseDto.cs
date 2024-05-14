namespace Dtos
{
    public class ResponseDto
    {
        public string FirstInstanceDateTime { get; set; }
        public string SecondInstanceDateTime { get; set; }
        public bool AreEqual 
        { 
            get
            {
                return FirstInstanceDateTime == SecondInstanceDateTime;
            }
        }
    }
}