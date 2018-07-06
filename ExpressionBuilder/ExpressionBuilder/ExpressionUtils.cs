using System;
using System.Linq;
using System.Linq.Expressions;

public static class ExpressionUtils
{
    public static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, string comparison, object value)
    {
        if (value is string s)
        {
            return BuildPredicate<T>(propertyName, comparison, s);
        }
        var parameter = Expression.Parameter(typeof(T));
        var left = AggregatePath(propertyName, parameter);
        var body = MakeComparison(left, comparison, value);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    public static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, string comparison, string value)
    {
        var parameter = Expression.Parameter(typeof(T));
        var left = AggregatePath(propertyName, parameter);

        var valueAsObject = ConvertStringToType(value, left.Type);
        var body = MakeComparison(left, comparison, valueAsObject);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    public static object ConvertStringToType(string value, Type type)
    {
        if (type == typeof(Guid))
        {
            return Guid.Parse(value);
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
            default:
                throw new NotSupportedException($"Invalid comparison operator '{comparison}'.");
        }
    }

    static Expression AggregatePath(string propertyName, ParameterExpression parameter)
    {
        return propertyName.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
    }

}