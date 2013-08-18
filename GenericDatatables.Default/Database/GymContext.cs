using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using GenericDatatables.Core.Domain.Models;

namespace GenericDatatables.Default.Database
{
    public class GymContext: DbContext
    {
        public GymContext(): this("GymContext")
        {
        }

        public GymContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<GymMember> GymMembers { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GymMember>().Map(m => m.MapInheritedProperties());
            modelBuilder.Entity<GymMember>()
                .Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Exercise>().Map(m => m.MapInheritedProperties());
            modelBuilder.Entity<Exercise>()
                .Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
