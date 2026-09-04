namespace FullAPIController.Dtos
{
    public class StudentAddDto
    {
        public string StudentNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Birthplace { get; set; }
    }
}