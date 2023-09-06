namespace GrpcService.Models
{
    public class Professor
    {
        public PersonalData PersonalData { get; set; } = new PersonalData();
        public string IdCode { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public DateTime FirstDayAtJob { get; set; } = DateTime.Now;
        public int Salary { get; set; } = 0;
    }
}
