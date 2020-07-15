using DeepEqual.Syntax;

namespace RegexMapper.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using Model;

    [TestFixture]
    public class RegexMapTests
    {
        [Test]
        public void Matches_WithGroupNamesDefinedInRegex()
        {
            var mapper = new RegexMap<TestModel>(@"{Id:(?<Id>\d*),Name:""(?<Name>[^""]*)""}");

            var result =
                mapper.Matches(
                    @"{Id:1,Name:""Test1""},{Id:12,Name:""Test12""},{Id:123,Name:""Test123""},{Id:1234,Name:""Test1234""}");

            Assert.AreEqual(
                new[]
                {
                    new TestModel {Id = 1, Name = "Test1"},
                    new TestModel {Id = 12, Name = "Test12"},
                    new TestModel {Id = 123, Name = "Test123"},
                    new TestModel {Id = 1234, Name = "Test1234"}
                },
                result);
        }

        [Test]
        public void Match_TestSimpleObject()
        {
            var mapper =
                new RegexMap<TestSimpleModel>(
                    @"^(?<Id>\d+),(?<Name>.+),(?<Value>\d+(\.\d+)?),(?<Enabled>(true|false))$");
            var value = "123,Bonjour,3.1415,true";
            var expected = new TestSimpleModel
            {
                Id = 123,
                Name = "Bonjour",
                Value = 3.1415m,
                Enabled = true,
                ExternalIds = null,
            };
            var result = mapper.Match(value);
            expected.ShouldDeepEqual(result);
        }

        [Test]
        public void Match_TestSimpleObjectWithList()
        {
            var regex = @"^(?<Id>\d+)"
                        + @",(?<Name>.+)"
                        + @",(?<Value>\d+(\.\d+)?)"
                        + @",(?<Enabled>(true|1|false|0))"
                        + @"(,(?<ExternalIds>\d+))*"
                        + @"$";
            var mapper = new RegexMap<TestSimpleModel>(regex);
            var value = "123,Bonjour,3.1415,true,4,5,6";
            var expected = new TestSimpleModel
            {
                Id = 123,
                Name = "Bonjour",
                Value = 3.1415m,
                Enabled = true,
                ExternalIds = new List<int>
                {
                    4, 5, 6
                }
            };
            var result = mapper.Match(value);
            expected.ShouldDeepEqual(result);
        }
    }
}
