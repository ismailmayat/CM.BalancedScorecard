using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CM.BalancedScoreboard.Services
{
    public enum ComparisonOp
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }

    public enum LogicOp
    {
        AndAlso,
        OrElse    
    }

    public class WhereFilter
    {
        public string PropertyName { get; set; }
        public ComparisonOp Operation { get; set; }
        public object Value { get; set; }
    }

    public static class ExpressionBuilder
    {
        private static readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new [] { typeof(string) });
        private static readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new [] { typeof(string) });

        public static Expression<Func<T, bool>> GetExpression<T>(IList<WhereFilter> filters, LogicOp op)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            for (int index = 0; index < filters.Count; index++)
            {
                if (exp == null)
                {
                    exp = GetExpression<T>(param, filters[index]);
                }
                else
                {
                    switch (op)
                    {
                        case LogicOp.AndAlso:
                            exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[index]));
                            break;
                        case LogicOp.OrElse:
                            exp = Expression.OrElse(exp, GetExpression<T>(param, filters[index]));
                            break;
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, WhereFilter filter)
        {
            MemberExpression member = Expression.Property(param, filter.PropertyName);
            ConstantExpression constant = Expression.Constant(filter.Value);

            switch (filter.Operation)
            {
                case ComparisonOp.Equals:
                    return Expression.Equal(member, constant);

                case ComparisonOp.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case ComparisonOp.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case ComparisonOp.LessThan:
                    return Expression.LessThan(member, constant);

                case ComparisonOp.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case ComparisonOp.Contains:
                    return Expression.Call(member, containsMethod, constant);

                case ComparisonOp.StartsWith:
                    return Expression.Call(member, startsWithMethod, constant);

                case ComparisonOp.EndsWith:
                    return Expression.Call(member, endsWithMethod, constant);
            }

            return null;
        }

        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> property)
        {
            MemberExpression memberExpression = (MemberExpression)property.Body;

            //return memberExpression.Member.Name;
            return property.Body.ToString().Substring(property.Body.ToString().IndexOf(".") + 1);
        }
    }
}
