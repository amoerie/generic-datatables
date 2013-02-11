using System;
using System.Linq;
using GenericDatatables.Core;
using GenericDatatables.Core.Filter;
using GenericDatatables.Test.Models;
using NUnit.Framework;

namespace GenericDatatables.Test.Tests.Filter
{
    [TestFixture]
    public class DatabaseFilterTest : DatatablesTest
    {
        [SetUp]
        public void SetUp()
        {
            _param = new DatatableParam
                {
                    ColumnsCount = 2,
                    DataProperties = new[] {"Id", "Name"},
                    DatatableId = "PeopleDatatable",
                    DisplayLength = 10,
                    DisplayStart = 0,
                    Echo = "abc",
                    GlobalRegex = false,
                    GlobalSearch = "",
                    Regex = new[] {false, false},
                    Search = new string[] {null, null},
                    Searchable = new[] {false, true}
                };

            _properties = new IDatatableProperty<Person>[]
                {
                    new DatatableProperty<Person, string>("Id", person => person.Id),
                    new DatatableProperty<Person, int>("Name", person => person.Name),
                    new DatatableProperty<Person, DateTime>("Birthday", person => person.Birthday, "dd/MM/yyyy")
                };
        }

        private DatatableFilter<Person> _filter;

        private DatatableParam _param;

        private IDatatableProperty<Person>[] _properties;

        [Test]
        public void TestThatFilterOnNameAlexReturnsAlex()
        {
            _param.GlobalSearch = Alex.Name;
            _filter = new DatatableFilter<Person>(_param, _properties);
            IQueryable<Person> filtered = _filter.Filter(People);
            Assert.That(filtered, Is.EquivalentTo(new[] {Alex}), "There's only one person with name Alex");
        }

        [Test]
        public void TestThatFilterOnNameContainingAReturnsAlexAnnAndMatt()
        {
            _param.GlobalSearch = "a";
            _filter = new DatatableFilter<Person>(_param, _properties);
            IQueryable<Person> filtered = _filter.Filter(People);
            Assert.That(filtered, Is.EquivalentTo(new[] {Alex, Ann, Matt}),
                        "Alex, Ann and Matt all have the letter 'a' in their name");
        }

        [Test]
        public void TestThatFilterOnSpecialCharactersDoesntFailAndReturnsNobody()
        {
            _param.GlobalSearch = "&é'(§è!çà)'$^µù=:;~`µ´][*¨\\";
            _filter = new DatatableFilter<Person>(_param, _properties);
            IQueryable<Person> filtered = _filter.Filter(People);
            Assert.That(filtered.Count(), Is.EqualTo(0), "There are no people with a special name like that.");
        }

        [Test]
        public void TestThatFilterOnZReturnsNobody()
        {
            _param.GlobalSearch = "z";
            _filter = new DatatableFilter<Person>(_param, _properties);
            IQueryable<Person> filtered = _filter.Filter(People);
            Assert.That(filtered.Count(), Is.EqualTo(0), "There are no people with the letter 'Z' in their name");
        }

        [Test]
        public void TestThatNoFilterDoesntChangeTheEntities()
        {
            _filter = new DatatableFilter<Person>(_param, _properties);
            IQueryable<Person> filtered = _filter.Filter(People);
            Assert.That(filtered, Is.EquivalentTo(People),
                        "The filter was empty, the list of people should be unchanged");
        }

        [Test]
        public void TestThatSearchingForBirthdayWithYear1990ReturnsAlex()
        {
            _param.GlobalSearch = "1990";
            _filter = new DatatableFilter<Person>(_param, _properties);
            IQueryable<Person> filtered = _filter.Filter(People);
            Assert.That(filtered, Is.EquivalentTo(new[] {Alex}));
        }
    }
}