using GenericDatatables.Test.Models;

namespace GenericDatatables.Test.Data
{
    public class TestContext : DbContext
    {
        /// <summary>
        ///     Constructs a new context instance using the given string as the name or connection string for the
        ///     database to which a connection will be made.
        ///     See the class remarks for how this is used to create a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public TestContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}