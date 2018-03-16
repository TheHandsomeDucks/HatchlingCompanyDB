using HatchlingCompany.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HatchlingCompany.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(30)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Birthdate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; }

        [MinLength(2)]
        [MaxLength(30)]
        public string BankAccount { get; set; }

        public EmployeeStatus Status { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [MinLength(2)]
        [MaxLength(30)]
        public string JobTitle { get; set; }

        public int? EmployeeDetailsId { get; set; }
        public virtual EmployeeDetail EmployeeDetails { get; set; }

        public int? ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
