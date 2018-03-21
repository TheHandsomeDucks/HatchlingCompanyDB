using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using HatchlingCompany.Models.Common;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListEmployeeDetailsModel : IMapFrom<Employee>, ICustomMapping
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public EmployeeStatus Status { get; set; }

        public decimal Salary { get; set; }

        public string JobTitle { get; set; }


        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Name: {this.FirstName} {this.LastName}");
            sb.AppendLine($"Email: {this.Email}");
            sb.AppendLine($"PhoneNumber: {this.PhoneNumber}");
            sb.AppendLine($"Status: {this.Status}");
            sb.AppendLine($"Salary: {this.Salary}");
            sb.AppendLine($"Job Title: {this.JobTitle}");

            return sb.ToString();
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Employee, ListEmployeeDetailsModel>();
        }
    }
}
