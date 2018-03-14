namespace HatchlingCompany.Models
{
    public class EmployeeDetail
    {
        public int Id { get; set; }

        public string Detail { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}