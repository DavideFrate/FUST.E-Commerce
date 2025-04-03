namespace nonMudNonBlazor.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Extention { get; set; }
        public string Email { get; set; }
        public string OfficeCode { get; set; }
        public string? JobTitle { get; set; }
        public int? ReportsTo { get; set; }

        public Employee Boss { get; set; }
    }
}
