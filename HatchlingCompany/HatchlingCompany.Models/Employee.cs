using HatchlingCompany.Models.Common;
using System;

namespace HatchlingCompany.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Birthdate { get; set; }

        public DateTime HireDate { get; set; }

        public string BankAccount { get; set; }

        public EmployeeStatus Status { get; set; }

        public decimal Salary { get; set; }

        public string JobTitle { get; set; }

        public int EmployeeDetailsId { get; set; }
        public virtual EmployeeDetail EmployeeDetails { get; set; }

        public int ManagerId { get; set; }

        public virtual Employee Manager { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

    }
}
