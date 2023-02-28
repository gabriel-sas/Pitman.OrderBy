using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Pitman.OrderBy
{
    public class OrderBy : IOrderBy
    {
        private string _orderByData;

        [FromQuery(Name = "order")]
        public string OrderByData
        {
            get => _orderByData;
            set
            {
                _orderByData = value;
            }
        }

        public IQueryable<T> ApplyOrder<T>(IQueryable<T> query)
        {
            var orderByQuery = new OrderByQuery(_orderByData);
            return orderByQuery.ApplyOrder(query);
        }
    }
}
