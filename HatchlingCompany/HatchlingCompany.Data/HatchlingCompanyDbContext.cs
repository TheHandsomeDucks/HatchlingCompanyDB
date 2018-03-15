using HatchlingCompany.Data.Migrations;
using HatchlingCompany.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace HatchlingCompany.Data
{
    public class HatchlingCompanyDbContext : DbContext, IHatchlingCompanyDbContext
    {
        public HatchlingCompanyDbContext() : base("HatchlingCompanyConnection")
        {
            var strategy = new MigrateDatabaseToLatestVersion<HatchlingCompanyDbContext, Configuration>();
            Database.SetInitializer(strategy);
        }

        public virtual IDbSet<Employee> Employees { get; set; }

        public virtual IDbSet<EmployeeDetail> EmployeeDetails { get; set; }

        public virtual IDbSet<Department> Departments { get; set; }

        public virtual IDbSet<Town> Towns { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
