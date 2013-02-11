using System;
using System.Linq;
using GenericDatatables.Test.Models;
using NUnit.Framework;
using TestContext = GenericDatatables.Test.Data.TestContext;

namespace GenericDatatables.Test
{
    [TestFixture]
    public abstract class DatatablesTest
    {
        private TestContext _dbContext;

        protected IQueryable<Person> People;

        protected Person Alex;

        protected Person Matt;

        protected Person Ann;

        [TestFixtureSetUp]
        public void SetupDatabase()
        {
            /*
             * Yes, I'm using a real database.
             * That's because the GenericDatatables library actually uses SqlFunctions in the background
             * and I've yet to devise a cunning scheme to mock those calls to an in-memory data store.
             * Once I do though, I'll just change this class and all tests will automatically use that.
             * 
             * So this solution isn't half-bad, right? Right?! God I'm a horrible developer.
             */
            _dbContext = new TestContext("TestContext");

            Alex = new Person
                {
                    Id = 1,
                    Name = "Alex",
                    Birthday = new DateTime(1990, 6, 2),
                    Time = new TimeSpan(12, 0, 0),
                    AddressId = 1,
                    Address =
                        new Address
                            {
                                Id = 1,
                                City = "London",
                                Street = "Baker Street",
                                HouseNumber = "1",
                                PostalCode = "1"
                            }
                };

            Matt = new Person
                {
                    Id = 2,
                    Name = "Matt",
                    Birthday = new DateTime(1987, 9, 7),
                    Time = new TimeSpan(6, 0, 0),
                    AddressId = 2,
                    Address =
                        new Address
                            {
                                Id = 2,
                                City = "Berlin",
                                Street = "Bäcker Straße",
                                HouseNumber = "1",
                                PostalCode = "2"
                            }
                };

            Ann = new Person
                {
                    Id = 3,
                    Name = "Ann",
                    Birthday = new DateTime(1997, 9, 1),
                    Time = new TimeSpan(0, 0, 0),
                    AddressId = 3,
                    Address =
                        new Address
                            {
                                Id = 3,
                                City = "Paris",
                                Street = "Rue des boulangers",
                                HouseNumber = "2",
                                PostalCode = "3"
                            }
                };

            if (!_dbContext.People.Any())
            {
                _dbContext.People.Add(Alex);
                _dbContext.People.Add(Matt);
                _dbContext.People.Add(Ann);
                _dbContext.SaveChanges();
            }

            Alex = _dbContext.People.Find(Alex.Id);
            Ann = _dbContext.People.Find(Ann.Id);
            Matt = _dbContext.People.Find(Matt.Id);

            People = _dbContext.People;
        }
    }
}