using System.Collections.Generic;

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

        public string Name { get; set; }

        public int TownId { get; set; }
        public virtual Town Town { get; set; }

        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Employees
        {
            get => new HashSet<Employee>(this.employees);
            //private set => employees = value; // TODO do we need setter?
        }
    }
}