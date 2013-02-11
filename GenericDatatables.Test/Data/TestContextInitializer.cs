using System.Data.Entity;

namespace GenericDatatables.Test.Data
{
    public class TestContextInitializer : DropCreateDatabaseAlways<TestContext>
    {
    }
}