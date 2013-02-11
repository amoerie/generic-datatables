using System.Data.Entity;
using GenericDatatables.Web.Models;

namespace GenericDatatables.Web.Persistence
{
    public class DataContext: DbContext
    {
        public DataContext()
            : base(("name=DataContext"))
        {
            
        }

        public DbSet<Person> People { get; set; }
    }
}