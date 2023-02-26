using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Pitman.OrderBy
{
    public class OrderBy : IOrderBy
    {
        private string _orderByData;
        private OrderByQuery orderByQuery;

        [FromQuery(Name = "orderBy")]
        public string OrderByData
        {
            get => _orderByData;
            set
            {
                _orderByData = value;
            }
        }

        public OrderBy()
        {
            orderByQuery = new OrderByQuery(_orderByData);
        }

        public IQueryable<T> ApplyOrder<T>(IQueryable<T> query)
        {
            return orderByQuery.ApplyOrder(query);
        }
    }
}
