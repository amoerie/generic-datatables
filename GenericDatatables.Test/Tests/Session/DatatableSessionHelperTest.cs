using System.Web;
using GenericDatatables.Core.Session;
using GenericDatatables.Test.Mocks;
using GenericDatatables.Test.Models;
using NUnit.Framework;

namespace GenericDatatables.Test.Tests.Session
{
    [TestFixture]
    public class DatatableSessionHelperTest
    {
        [SetUp]
        public void SetUp()
        {
            _session = new FakeHttpSessionStateBase();
        }

        private HttpSessionStateBase _session;

        [Test]
        public void TestThatNonExistingKeyThrowsArgumentException()
        {
            Assert.That(() => _session.GetDatatableProperties<Person>("invalid datatable id"), Throws.ArgumentException);
        }

        [Test]
        public void TestThatTheCorrectSessionObjectIsReturnedForDifferentKeys()
        {
            const string Key1 = "key1",
                         Key2 = "key2";

            DatatableSessionObject<Person>
                object1 = new DatatableSessionObject<Person> {HasLastColumn = true},
                object2 = new DatatableSessionObject<Person> {HasLastColumn = false};

            _session.AddDatatableProperties(Key1, object1);
            _session.AddDatatableProperties(Key2, object2);

            Assert.That(_session.GetDatatableProperties<Person>(Key1), Is.EqualTo(object1));
            Assert.That(_session.GetDatatableProperties<Person>(Key2), Is.EqualTo(object2));
        }
    }
}