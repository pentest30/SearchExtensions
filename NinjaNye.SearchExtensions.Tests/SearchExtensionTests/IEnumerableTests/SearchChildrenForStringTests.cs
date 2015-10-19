using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NinjaNye.SearchExtensions.Tests.SearchExtensionTests.IEnumerableTests
{
    [TestFixture]
    public class SearchChildrenForStringTests
    {
        private ParentTestData _parent;
        private List<ParentTestData> _testData;
        private TestData _dataOne;
        private TestData _dataFour;
        private TestData _dataTwo;
        private TestData _dataThree;
        private ParentTestData _otherParent;

        [SetUp]
        public void SetUp()
        {
            this._dataOne = new TestData {Name = "chris", Description = "child data", Number = 1, Age = 20};
            this._dataTwo = new TestData {Name = "fred", Description = "child data", Number = 6, Age = 30};
            this._dataThree = new TestData {Name = "teddy", Description = "child data", Number = 2, Age = 40};
            this._dataFour = new TestData {Name = "josh", Description = "child data", Number = 20, Age = 50};
            this._parent = new ParentTestData
            {
                Children = new List<TestData> {this._dataOne, this._dataTwo},
            };
            this._otherParent = new ParentTestData
            {
                Children = new List<TestData> {this._dataThree, this._dataFour},
            };
            this._testData = new List<ParentTestData> {this._parent, this._otherParent};
        }

        [Test]
        public void SearchChild_StringEquals_ReturnParentsWIthAnyChildThatMatches()
        {
            //Arrange
            
            //Act
            var result = _testData.Search(p => p.Children)
                                  .With(c => c.Name)
                                  .EqualTo("chris")
                                  .ToList();

            //Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result, Contains.Item(_parent));
        }

        [Test]
        public void SearchChild_StringEqualsMany_ReturnParentsWIthAnyChildThatMatches()
        {
            //Arrange
            
            //Act
            var result = _testData.Search(p => p.Children)
                                  .With(c => c.Name)
                                  .EqualTo("chris", "teddy")
                                  .ToList();

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Contains.Item(_parent));
            Assert.That(result, Contains.Item(_otherParent));
        }

        [Test]
        public void SearchChild_WithStringEqualToCaseInsensitive_ReturnParentsWithMatches()
        {
            //Arrange
            
            //Act
            var result = _testData.Search(p => p.Children)
                                  .With(c => c.Name)
                                  .SetCulture(StringComparison.OrdinalIgnoreCase)
                                  .EqualTo("CHRIS")
                                  .ToList();

            //Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result, Contains.Item(_parent));
        }
    }
}