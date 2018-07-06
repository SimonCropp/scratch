using System.Collections.Generic;
using System.Linq;
using Xunit;

public class Tests
{
    [Theory]
    [InlineData("Name", "==", "Person 1", "Person 1")]
    [InlineData("Name", "!=", "Person 2", "Person 1")]
    [InlineData("Name", "Contains", "son 2", "Person 2")]
    [InlineData("Name", "StartsWith", "Person 2", "Person 2")]
    [InlineData("Name", "EndsWith", "son 2", "Person 2")]
    [InlineData("Age", "==", "13", "Person 2")]
    [InlineData("Age", ">", "12", "Person 2")]
    [InlineData("Age", "!=", "12", "Person 2")]
    [InlineData("Age", ">=", "13", "Person 2")]
    [InlineData("Age", "<", "13", "Person 1")]
    [InlineData("Age", "<=", "12", "Person 1")]
    public void Foo(string name, string expression, string value, string expectedName)
    {
        var people = new List<Person>
        {
            new Person
            {
                Name = "Person 1",
                Age = 12
            },
            new Person
            {
                Name = "Person 2",
                Age = 13
            }
        };

        var result = people.AsQueryable()
            .Where(ExpressionUtils.BuildPredicate<Person>(name, expression, value))
            .Single();
        Assert.Equal(expectedName, result.Name);
    }
}
