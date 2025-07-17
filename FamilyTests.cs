//using Xunit;
using cosmosdbtest.Models;

namespace cosmosdbtest.Tests
{
    public class FamilyTests
    {
        //[Fact]
        public void CreateFamily_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var parents = new[] { new Parent { FirstName = "Bob" }, new Parent { FirstName = "Mary" } };
            var children = new[] { new Child { FirstName = "Jan" }, new Child { FirstName = "Bobby" }, new Child { FirstName = "Cindy" } };
            var address = new Address { State = "CA", County = "Santa Clara", City = "San Jose" };

            // Act
            var family = new Family
            {
                Id = "Brady.3",
                LastName = "Brady",
                IsRegistered = true,
                Parents = parents,
                Children = children,
                Address = address
            };

            // Assert
            //Assert.Equal("Brady.3", family.Id);
            //Assert.Equal("Brady", family.LastName);
            //Assert.True(family.IsRegistered);
            //Assert.Equal(2, family.Parents.Length);
            //Assert.Equal(3, family.Children.Length);
            //Assert.Equal("CA", family.Address.State);

            // UNIT TESTS. You can write this as a comment

            // TASK 12: Add an assert to check that the first name of first parent is Bob

            // TASK 13: Add an assert to check that the first name of second parent is Mary

            // TASK 14: Add an assert to check that the first name of first child is Jan

            // TASK 15: Add an assert to check that the country of family address is Santa Clara

        }
    }
}
