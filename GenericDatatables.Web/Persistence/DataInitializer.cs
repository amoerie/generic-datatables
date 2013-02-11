using System;
using System.Data.Entity;
using System.Linq;
using GenericDatatables.Web.Models;

namespace GenericDatatables.Web.Persistence
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            foreach (var person in Enumerable.Range(1, 5000).Select(i => new Person()
                {
                    Id = i, 
                    Name = RandomString("Mr. ", 3),
                    Birthday = DateTime.Now.AddYears(_random.Next(20, 40)),
                    Time = new TimeSpan(_random.Next(0,24), _random.Next(0,60), _random.Next(0,60)),
                    Address = new Address {
                        Id = i,
                        Street = RandomString("Street. ", 3),
                        HouseNumber = _random.Next(99).ToString(),
                        City = RandomString("", 10),
                        PostalCode = _random.Next(1000, 9999).ToString()
                    }
                }))
            {
                context.People.Add(person);
            }
            context.SaveChanges();
        }

        private readonly Random _random = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private string RandomString(string prefix, int size)
        {
            var buffer = new char[size];

            for (int j = 0; j < size; j++)
            {
                buffer[j] = Chars[_random.Next(Chars.Length)];
            }
            return prefix + new string(buffer);
        }
    }
}