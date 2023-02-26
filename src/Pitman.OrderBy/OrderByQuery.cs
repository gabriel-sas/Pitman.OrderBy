using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Pitman.OrderBy
{
    public class OrderByQuery
    {
        public IList<(string Property, string Direction)> SortingProperties { get; } = new List<(string Property, string Direction)>();

        public OrderByQuery(string orderByData)
        {
            ParseOrderByData(orderByData);
        }

        private void ParseOrderByData(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var ordersBy = value.Split(',');
            foreach (var orderBy in ordersBy)
            {
                var data = orderBy.Split(':');
                
                var propertyName = data[0].FirstCharToUpper();
                var direction = GetOrderByDirection(data.Length == 2 ? data[1] : string.Empty);

                SortingProperties.Add((Property: propertyName, Direction: Enum.GetName(typeof(Direction), direction)));
            }

        }

        public IQueryable<T> ApplyOrder<T>(IQueryable<T> query)
        {
            foreach (var item in SortingProperties)
            {
                query = AddOrderBy(query, item.Property, GetOrderByDirection(item.Direction));
            }

            return query;
        }

        private static IQueryable<T> AddOrderBy<T>(IQueryable<T> source, string ordering, Direction direction)
        {
            var type = typeof(T);
            var property = GetPropertyInfo(type, ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var methodName = direction == Direction.DESC ? "OrderByDescending" : "OrderBy";

            var resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
            {
                throw new ArgumentException("The property name you are trying to order is not available on object.");
            }

            return matchedProperty;
        }

        public Direction GetOrderByDirection(string data)
        {
            if (Enum.TryParse<Direction>(data, true, out var direction))
            {
                return direction;
            }

            throw new ArgumentException("Order by direction is not valid, it should be either empty,'ASC' or 'DESC'");
        }
    }
}
