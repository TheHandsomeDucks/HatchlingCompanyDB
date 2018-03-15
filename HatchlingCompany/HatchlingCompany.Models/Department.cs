using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HatchlingCompany.Models
{
    public class Department
    {
        private ICollection<Employee> employees;

        public Department()
        {
            this.employees = new HashSet<Employee>();
        }
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public int TownId { get; set; }
        public virtual Town Town { get; set; }

        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Employees
        {
            get => new HashSet<Employee>(this.employees);
            private set => this.employees = value; // TODO do we need setter
        }
    }
}