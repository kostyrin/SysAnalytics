using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using SysAnalytics.Model.Enums;

namespace SysAnalytics.Model.Expressions
{

    public static class LinqExtensions
    {
        /// <summary>Orders the sequence by specific column and direction.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sortColumn">The sort column.</param>
        /// <param name="ascending">if set to true [ascending].</param>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, string direction)
        {
            string methodName = string.Format("OrderBy{0}",
                direction.ToLower() == "asc" ? "" : "descending");

            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");

            MemberExpression memberAccess = null;
            foreach (var property in sortColumn.Split('.'))
                memberAccess = MemberExpression.Property
                   (memberAccess ?? (parameter as Expression), property);

            LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);

            MethodCallExpression result = Expression.Call(
                      typeof(Queryable),
                      methodName,
                      new[] { query.ElementType, memberAccess.Type },
                      query.Expression,
                      Expression.Quote(orderByLambda));

            return query.Provider.CreateQuery<T>(result);
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }


        public static IQueryable<T> Where<T>(this IQueryable<T> query,
            string column, object value, WhereOperation operation)
        {
            if (string.IsNullOrEmpty(column))
                return query;

            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");

            MemberExpression memberAccess = null;
            foreach (var property in column.Split(' '))
                memberAccess = MemberExpression.Property
                   (memberAccess ?? (parameter as Expression), property);

            Type type = memberAccess.Type;

            Expression condition = null;
            LambdaExpression lambda = null;
            //Expression[] lambdaArr = new Expression[2];

            if (string.IsNullOrEmpty(value.ToString()))
                value = GetDefault(type);
                //if (type == typeof (int)) value = 0;
            else
            {
                //change param value type
                //necessary to getting bool from string
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = memberAccess.Type.GetGenericArguments()[0];
                }

                if (type.IsEnum)
                    value = StringEnum.GetEnum(type, value.ToString());
                else if (type == typeof(DateTimeOffset))
                {
                    string time = string.Empty;
                    if (operation == WhereOperation.Less || operation == WhereOperation.GreaterOrEqual) time = " 00:00:01+00:00";
                    if (operation == WhereOperation.Greater || operation == WhereOperation.LessOrEqual) time = " 23:59:59+00:00";
                    
                    if (operation == WhereOperation.Equal)
                    {
                        var from = DateTimeOffset.Parse(String.Format("{0}{1}", value, " 00:00:01 +00:00"), CultureInfo.InvariantCulture);
                        ConstantExpression filterFrom = Expression.Constant(from, memberAccess.Type);
                        var to = DateTimeOffset.Parse(String.Format("{0}{1}", value, " 23:59:59 +00:00"), CultureInfo.InvariantCulture);
                        ConstantExpression filterTo = Expression.Constant(to, memberAccess.Type);
                        var condition1 = Expression.GreaterThan(memberAccess, filterFrom);
                        var condition2 = Expression.LessThan(memberAccess, filterTo);
                        var body = Expression.AndAlso(condition1, condition2);
                        lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
                    }
                    else
                    {
                        value = DateTimeOffset.Parse(String.Format("{0}{1}", value, time), CultureInfo.InvariantCulture);
                    }
                }
                else if (type == typeof(decimal))
                {
                    value = Convert.ChangeType(value, type);
                    if (operation == WhereOperation.Equal)
                    {
                        ConstantExpression filter1 = Expression.Constant((decimal)value - 0.01M, memberAccess.Type);
                        ConstantExpression filter2 = Expression.Constant((decimal)value + 0.01M, memberAccess.Type);
                        var condition1 = Expression.GreaterThan(memberAccess, filter1);
                        var condition2 = Expression.LessThan(memberAccess, filter2);
                        var body = Expression.AndAlso(condition1, condition2);
                        lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
                    }
                    else if (operation == WhereOperation.NotEqual)
                    {
                        ConstantExpression filter = Expression.Constant(value, memberAccess.Type);
                        var condition1 = Expression.LessThan(memberAccess, filter);
                        var condition2 = Expression.GreaterThan(memberAccess, filter);
                        var body = Expression.OrElse(condition1, condition2);
                        lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
                    }
                }
                else
                    value = Convert.ChangeType(value, type);
            }

            if (lambda == null)
            {
                //ConstantExpression filter = Expression.Constant(value == null ? value : Convert.ChangeType(value, memberAccess.Type));
                ConstantExpression filter = Expression.Constant(value, memberAccess.Type);
                //ConstantExpression filter = Expression.Constant(value);

                //switch operation

                switch (operation)
                {
                    //equal ==
                    case WhereOperation.Equal:
                    case WhereOperation.IsNull:
                        condition = Expression.Equal(memberAccess, filter);
                        lambda = Expression.Lambda(condition, parameter);
                        break;
                    //not equal !=
                    case WhereOperation.NotEqual:
                    case WhereOperation.IsNotNull:
                        condition = Expression.NotEqual(memberAccess, filter);
                        lambda = Expression.Lambda(condition, parameter);
                        break;
                    //string.Contains()
                    case WhereOperation.Contains:
                    case WhereOperation.BeginsWith:
                        condition = Expression.Call(memberAccess, typeof (string).GetMethod("Contains"),
                            Expression.Constant(value));
                        lambda = value is string
                            ? Expression.Lambda(condition, parameter)
                            : Expression.Lambda<Func<T, bool>>(condition, parameter);
                        break;
                    case WhereOperation.Greater:
                        condition = Expression.GreaterThan(memberAccess, filter);
                        lambda = Expression.Lambda(condition, parameter);
                        break;
                    case WhereOperation.GreaterOrEqual:
                        condition = Expression.GreaterThanOrEqual(memberAccess, filter);
                        lambda = Expression.Lambda(condition, parameter);
                        break;
                    case WhereOperation.Less:
                        condition = Expression.LessThan(memberAccess, filter);
                        lambda = Expression.Lambda(condition, parameter);
                        break;
                    case WhereOperation.LessOrEqual:
                        condition = Expression.LessThanOrEqual(memberAccess, filter);
                        lambda = Expression.Lambda(condition, parameter);
                        break;
                    default:
                        return query;
                }
            }

            MethodCallExpression result = Expression.Call(typeof(Queryable), "Where", new[] { query.ElementType }, query.Expression, lambda);

            return query.Provider.CreateQuery<T>(result);
        }
    }
}
