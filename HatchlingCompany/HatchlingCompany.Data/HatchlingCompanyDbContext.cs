using HatchlingCompany.Models;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HatchlingCompany.Data
{
    public class HatchlingCompanyDbContext : DbContext, IDbContext
    {
        public HatchlingCompanyDbContext()
            : base("HatchlingCompanyConnection")
        {
        }

        public HatchlingCompanyDbContext(DbConnection connection)
            : base(connection, true)
        {
        }

        public virtual IDbSet<Employee> Employees { get; set; }

        public virtual IDbSet<Department> Departments { get; set; }

        public virtual IDbSet<Town> Towns { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<Project> Projects { get; set; }

        public virtual IDbSet<Relationship> Relationships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Employee>()
                .HasOptional(x => x.Department)
                .WithMany(x => x.Employees);

            modelBuilder.Entity<Employee>()
               .HasMany(x => x.Projects)
               .WithMany(x => x.Employees);

            modelBuilder.Entity<Employee>()
                .HasOptional(x => x.Manager);

            modelBuilder.Entity<Department>()
                .HasRequired(x => x.Manager);

            modelBuilder.Entity<Department>()
                .HasMany(x => x.Employees)
                .WithOptional(x => x.Department);

            modelBuilder.Entity<Project>()
              .HasRequired(x => x.Manager);

            modelBuilder.Entity<Project>()
                .HasMany(x => x.Employees)
                .WithMany(x => x.Projects);

            modelBuilder.Entity<Relationship>()
                .HasRequired(x => x.Author)
                .WithMany(x => x.SentRelationships);

            modelBuilder.Entity<Relationship>()
                .HasRequired(x => x.Recipient)
                .WithMany(x => x.ReceivedRelationships);

            base.OnModelCreating(modelBuilder);
        }
    }
}
