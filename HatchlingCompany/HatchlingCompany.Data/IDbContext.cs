using HatchlingCompany.Models;
using System.Data.Entity;

namespace HatchlingCompany.Data
{
    public interface IDbContext
    {
        IDbSet<Employee> Employees { get; set; }

        IDbSet<Project> Projects { get; set; }

        IDbSet<Department> Departments { get; set; }

        IDbSet<Town> Towns { get; set; }

        IDbSet<Country> Countries { get; set; }

        int SaveChanges();
    }
}
