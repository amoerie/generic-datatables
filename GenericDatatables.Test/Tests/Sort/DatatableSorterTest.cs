using System;
using System.Collections.Generic;
using System.Linq;
using GenericDatatables.Test.Models;
using NUnit.Framework;

namespace GenericDatatables.Test.Tests.Sort
{
    [TestFixture]
    public class DatatableSorterTest : DatatablesTest
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
                    Searchable = new[] {false, true},
                };

            _properties = new IDatatableProperty<Person>[]
                {
                    new DatatableProperty<Person, string>("Id", person => person.Id),
                    new DatatableProperty<Person, int>("Name", person => person.Name),
                    new DatatableProperty<Person, DateTime>("Birthday", person => person.Birthday, "yyyy-MM-dd")
                };
        }

        private DatatableSorter<Person> _sorter;

        private DatatableParam _param;

        private IDatatableProperty<Person>[] _properties;

        [Test]
        public void TestThatSortingByBirthdayAscendingReturnsMattFirstAndAnnLast()
        {
            _param.SortingColumnsCount = 1;
            _param.SortingColumns = new[] {2};
            _param.SortDirections = new[] {"asc"};
            _sorter = new DatatableSorter<Person>(_param, _properties);
            List<Person> sorted = _sorter.Sort(People).ToList();
            Assert.That(sorted.First(), Is.EqualTo(Matt), "The first element should be Matt, he's the oldest");
            Assert.That(sorted.Last(), Is.EqualTo(Ann), "The last element should be Ann, she's the youngest");
        }

        [Test]
        public void TestThatSortingByNameAscendingReturnsAlexFirstAndMattLast()
        {
            _param.SortingColumnsCount = 1;
            _param.SortingColumns = new[] {1};
            _param.SortDirections = new[] {"asc"};
            _sorter = new DatatableSorter<Person>(_param, _properties);
            List<Person> sorted = _sorter.Sort(People).ToList();
            Assert.That(sorted.First(), Is.EqualTo(Alex), "The first element should be Alex");
            Assert.That(sorted.Last(), Is.EqualTo(Matt), "The last element should be Matt");
        }

        [Test]
        public void TestThatSortingByNameDescendingReturnsMattFirstAndAlexLast()
        {
            _param.SortingColumnsCount = 1;
            _param.SortingColumns = new[] {1};
            _param.SortDirections = new[] {"desc"};
            _sorter = new DatatableSorter<Person>(_param, _properties);
            List<Person> sorted = _sorter.Sort(People).ToList();
            Assert.That(sorted.First(), Is.EqualTo(Matt), "The first element should be Matt");
            Assert.That(sorted.Last(), Is.EqualTo(Alex), "The last element should be Alex");
        }

        [Test]
        public void TestThatSortingDoesntAffectTheNumberOfResults()
        {
            _param.SortingColumnsCount = 1;
            _param.SortingColumns = new[] {1};
            _param.SortDirections = new[] {"asc"};
            _sorter = new DatatableSorter<Person>(_param, _properties);
            List<Person> sorted = _sorter.Sort(People).ToList();
            Assert.That(sorted.Count(), Is.EqualTo(3), "Sorting should have no effect on the number of results");
        }
    }
}