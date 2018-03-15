using HatchlingCompany.Data.Migrations;
using HatchlingCompany.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace HatchlingCompany.Data
{
    public class HatchlingCompanyDbContext : DbContext
    {
        public HatchlingCompanyDbContext() : base("HatchlingCompanyConnection")
        {
            var strategy = new MigrateDatabaseToLatestVersion<HatchlingCompanyDbContext, Configuration>();
            Database.SetInitializer(strategy);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Employee> Employees { get; set; }

        public IDbSet<EmployeeDetail> EmployeeDetails { get; set; }

        public IDbSet<Department> Departments { get; set; }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<Country> Countries { get; set; }

    }
}
