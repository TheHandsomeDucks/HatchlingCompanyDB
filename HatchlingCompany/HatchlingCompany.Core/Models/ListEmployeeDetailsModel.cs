using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using HatchlingCompany.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListEmployeeDetailsModel : IMapFrom<Employee>, ICustomMapping
    {

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? Birthdate { get; set; }

        public DateTime? HireDate { get; set; }

        public string BankAccount { get; set; }

        public EmployeeStatus Status { get; set; }

        public decimal Salary { get; set; }

        public string JobTitle { get; set; }

        public Employee Manager { get; set; }

        public Department Department { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<Relationship> SentRelationships { get; set; }

        public ICollection<Relationship> ReceivedRelationships { get; set; }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Name: {this.FirstName} {this.LastName}");
            sb.AppendLine($"Email: {this.Email}");
            sb.AppendLine($"PhoneNumber: {this.PhoneNumber}");
            sb.AppendLine($"Birthdate: {this.Birthdate}");
            sb.AppendLine($"HireDate: {this.HireDate}");
            sb.AppendLine($"BankAccount: {this.BankAccount}");
            sb.AppendLine($"Status: {this.Status}");
            sb.AppendLine($"Salary: {this.Salary}");
            sb.AppendLine($"Job Title: {this.JobTitle}");
            sb.AppendLine($"Manager: {this.Manager}");
            sb.AppendLine($"Department: {this.Department}");
            sb.AppendLine(string.Join(" ", this.Projects));
            sb.AppendLine(string.Join(" ", this.SentRelationships));
            sb.AppendLine(string.Join(" ", this.ReceivedRelationships));

            return sb.ToString();
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Employee, ListEmployeeDetailsModel>();
        }
    }
}
