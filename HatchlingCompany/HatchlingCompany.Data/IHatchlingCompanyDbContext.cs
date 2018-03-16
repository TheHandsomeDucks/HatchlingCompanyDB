using HatchlingCompany.Models;
using System.Data.Entity;

namespace HatchlingCompany.Data
{
    public interface IHatchlingCompanyDbContext
    {

        IDbSet<Employee> Employees { get; set; }

        IDbSet<EmployeeDetail> EmployeeDetails { get; set; }

        IDbSet<Department> Departments { get; set; }

        IDbSet<Town> Towns { get; set; }

        IDbSet<Country> Countries { get; set; }

        int SaveChanges();
    }
}
