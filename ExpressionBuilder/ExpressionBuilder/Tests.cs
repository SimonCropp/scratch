﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class Tests
{
    [Fact]
    public void Nested()
    {
        var list = new List<Target>
        {
            new Target
            {
                Member = "a"
            },
            new Target
            {
                Member = "bb"
            }
        };

        var result = list.AsQueryable()
            .Where(ExpressionUtils.BuildPredicate<Target>("Member.Length", "==", 2))
            .Single();
        Assert.Equal("bb", result.Member);
    }

    [Fact]
    public void Nullable()
    {
        var list = new List<TargetWithNullable>
        {
            new TargetWithNullable
            {
                Field = null
            },
            new TargetWithNullable
            {
                Field = 10
            }
        };

        var resultFromString = list.AsQueryable()
            .Where(ExpressionUtils.BuildPredicate<TargetWithNullable>("Field", "==", "10"))
            .Single();
        Assert.Equal(10, resultFromString.Field);
        var result = list.AsQueryable()
            .Where(ExpressionUtils.BuildPredicate<TargetWithNullable>("Field", "==", 10))
            .Single();
        Assert.Equal(10, result.Field);
        var nullResult = list.AsQueryable()
            .Where(ExpressionUtils.BuildPredicate<TargetWithNullable>("Field", "==", null))
            .Single();
        Assert.Null(nullResult.Field);
    }

    [Fact]
    public void Field()
    {
        var list = new List<TargetWithField>
        {
            new TargetWithField
            {
                Field = "Target1"
            },
            new TargetWithField
            {
                Field = "Target2"
            }
        };

        var result = list.AsQueryable()
            .Where(ExpressionUtils.BuildPredicate<TargetWithField>("Field", "==", "Target2"))
            .Single();
        Assert.Equal("Target2", result.Field);
    }


    [Theory]
    [InlineData("Name", "==", "Person 1", "Person 1")]
    [InlineData("Name", "!=", "Person 2", "Person 1")]
    [InlineData("Name", "Contains", "son 2", "Person 2")]
    [InlineData("Name", "StartsWith", "Person 2", "Person 2")]
    [InlineData("Name", "EndsWith", "son 2", "Person 2")]
    [InlineData("Age", "==", "13", "Person 2")]
    [InlineData("Age", "==", 13, "Person 2")]
    [InlineData("Age", ">", 12, "Person 2")]
    [InlineData("Age", "!=", 12, "Person 2")]
    [InlineData("Age", ">=", 13, "Person 2")]
    [InlineData("Age", "<", 13, "Person 1")]
    [InlineData("Age", "<=", 12, "Person 1")]
    public void Combos(string name, string expression, object value, string expectedName)
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

    [Theory]
    [InlineData(typeof(int), "12", 12)]
    [InlineData(typeof(int?), null, null)]
    public void ConvertStringToType(Type type, string value, object expected)
    {
        var result = ExpressionUtils.ConvertStringToType(value, type);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertStringToGuid()
    {
        var guid = Guid.NewGuid();
        var value = guid.ToString();
        var result = ExpressionUtils.ConvertStringToType(value, typeof(Guid));
        Assert.Equal(guid, result);
    }

    [Fact]
    public void ConvertStringToDatetime()
    {
        var dateTime = DateTime.Now.Date;
        var value = dateTime.ToString();
        var result = ExpressionUtils.ConvertStringToType(value, typeof(DateTime));
        Assert.Equal(dateTime, result);
    }
}