using System.ComponentModel.DataAnnotations;

namespace HatchlingCompany.Models
{
    public class EmployeeDetail
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        public string Detail { get; set; }

        public int EmployeeId { get; set; }
        [Required]
        public virtual Employee Employee { get; set; }
    }
}