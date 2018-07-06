using System;
using System.Linq;
using System.Linq.Expressions;

public static class ExpressionUtils
{
    public static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, string comparison, object value)
    {
        var parameter = Expression.Parameter(typeof(T));
        var left = propertyName.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
        var body = MakeComparison(left, comparison, value);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
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
                //TODO:validate string
                return Expression.Call(left, comparison, Type.EmptyTypes, constant);
            default:
                throw new NotSupportedException($"Invalid comparison operator '{comparison}'.");
        }
    }
}