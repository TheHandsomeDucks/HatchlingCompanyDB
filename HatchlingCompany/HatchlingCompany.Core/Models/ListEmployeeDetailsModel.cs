using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListEmployeeDetailsModel : IMapFrom<Employee>, ICustomMapping
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        //public DateTime? Birthdate { get; set; }

        //public DateTime? HireDate { get; set; }

        //public string BankAccount { get; set; }

        //public EmployeeStatus Status { get; set; }

        //public decimal Salary { get; set; }

        public string JobTitle { get; set; }

        //public virtual Employee Manager { get; set; }

        public virtual Department Department { get; set; }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Name: {this.FirstName} {this.LastName}");
            sb.AppendLine($"Email: {this.Email}");
            sb.AppendLine($"PhoneNumber: {this.PhoneNumber}");
            sb.AppendLine($"Job Title: {this.JobTitle}");
            sb.AppendLine($"Department: {this.Department}");

            return sb.ToString();
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Employee, ListEmployeeDetailsModel>();
        }
    }
}
