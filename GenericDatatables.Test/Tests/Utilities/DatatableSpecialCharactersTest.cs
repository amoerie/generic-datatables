using System.Linq;
using GenericDatatables.Core.Utilities;
using NUnit.Framework;

namespace GenericDatatables.Test.Tests.Utilities
{
    [TestFixture]
    public class DatatableSpecialCharactersTest
    {
        private static readonly string[] SpecialCharacters = {":"};

        [Test]
        public void TestThatAllSpecialCharactersAreEscaped()
        {
            string unescaped = SpecialCharacters.Aggregate("", (a, b) => a + b);
            string whatItShouldBe = SpecialCharacters.Aggregate("", (a, b) => a + "\\" + b);
            Assert.That(unescaped.Escape(), Is.EqualTo(whatItShouldBe));
        }

        [Test]
        public void TestThatCharacterColonIsEscaped()
        {
            const string Unescaped = "abc:abc";
            const string WhatItShouldBe = "abc\\:abc";
            Assert.That(Unescaped.Escape(), Is.EqualTo(WhatItShouldBe));
        }

        [Test]
        public void TestThatRegularStringsRemainUnchanged()
        {
            const string RegularString = "abcdefghijklmnopqrstuvwxyz0123456789";
            string escaped = RegularString.Escape();
            Assert.That(escaped, Is.EqualTo(RegularString), "There were no special characters in this string");
        }

        [Test]
        public void TestThatTwoSuccessiveColonsAreEscaped()
        {
            const string Unescaped = "abc::abc";
            const string WhatItShouldBe = "abc\\:\\:abc";
            Assert.That(Unescaped.Escape(), Is.EqualTo(WhatItShouldBe));
        }
    }
}