﻿using System;
using System.Linq;
using System.Linq.Expressions;

public static class ExpressionUtils
{
    public static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, string comparison, object value)
    {
        var parameter = Expression.Parameter(typeof(T));
        var left = AggregatePath(propertyName, parameter);
        if (left.Type != typeof(string) && value is string stringValue)
        {
            value = ConvertStringToType(stringValue, left.Type);
        }

        var body = MakeComparison(left, comparison, value);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    public static object ConvertStringToType(string value, Type type)
    {
        if (type == typeof(Guid))
        {
            return Guid.Parse(value);
        }

        var underlyingType = Nullable.GetUnderlyingType(type);
        if (underlyingType != null)
        {
            if (value == null)
            {
                return null;
            }
            type = underlyingType;
        }
        return Convert.ChangeType(value, type);
    }

    static Expression MakeComparison(Expression left, string comparison, object value)
    {
        var constant = Expression.Constant(value, left.Type);
        switch (comparison)
        {
            case "==":
                return Expression.MakeBinary(ExpressionType.Equal, left, constant);
            case "!=":
                return Expression.MakeBinary(ExpressionType.NotEqual, left, constant);
            case ">":
                return Expression.MakeBinary(ExpressionType.GreaterThan, left, constant);
            case ">=":
                return Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, left, constant);
            case "<":
                return Expression.MakeBinary(ExpressionType.LessThan, left, constant);
            case "<=":
                return Expression.MakeBinary(ExpressionType.LessThanOrEqual, left, constant);
            case "Contains":
            case "StartsWith":
            case "EndsWith":
                if (value is string)
                {
                    return Expression.Call(left, comparison, Type.EmptyTypes, constant);
                }

                throw new NotSupportedException($"Comparison operator '{comparison}' only supported on string.");
        }

        throw new NotSupportedException($"Invalid comparison operator '{comparison}'.");
    }

    static Expression AggregatePath(string propertyName, Expression parameter)
    {
        return propertyName.Split('.')
            .Aggregate(parameter, Expression.PropertyOrField);
    }
}