using System;
using System.Linq;
using GenericDatatables.Core;
using GenericDatatables.Core.Filter;
using GenericDatatables.Test.Models;
using NUnit.Framework;

namespace GenericDatatables.Test.Tests.Filter
{
    [TestFixture]
    public class DatatablePropertyFilterTest : DatatablesTest
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
                    new DatatableProperty<Person, DateTime>("Birthday", person => person.Birthday, "yyyy-MM-dd")
                };
        }

        private DatatablePropertyFilter<Person> _filter;

        private DatatableParam _param;

        private IDatatableProperty<Person>[] _properties;

        [Test]
        public void TestThatFilterOnNameAlexReturnsAlex()
        {
            _param.Search[1] = Alex.Name;
            _filter = new DatatablePropertyFilter<Person>(_param, _properties);
            IQueryable<Person> filtered = _filter.Filter(People);
            Assert.That(filtered, Is.EquivalentTo(new[] {Alex}));
        }

        [Test]
        public void TestThatFilterOnNameContainingAReturnsAlexAnnAndMatt()
        {
            _param.Search[1] = "a";
            _filter = new DatatablePropertyFilter<Person>(_param, _properties);
            IQueryable<Person> filtered = _filter.Filter(People);
            Assert.That(filtered, Is.EquivalentTo(new[] {Alex, Ann, Matt}));
        }

        [Test]
        public void TestThatNoFilterDoesntChangeTheEntities()
        {
            _filter = new DatatablePropertyFilter<Person>(_param, _properties);
            IQueryable<Person> filtered = _filter.Filter(People);
            Assert.That(filtered, Is.EquivalentTo(People));
        }
    }
}