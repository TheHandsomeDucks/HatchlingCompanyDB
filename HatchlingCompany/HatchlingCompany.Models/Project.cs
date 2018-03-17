using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HatchlingCompany.Models
{
    public class Project
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Detail { get; set; }

        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}