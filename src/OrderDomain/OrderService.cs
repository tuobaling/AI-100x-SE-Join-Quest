using System.Collections.Generic;
using System.Linq;

namespace OrderDomain
{
    public class OrderService
    {
        public int CalculateTotalAmount(IEnumerable<OrderItem> items)
        {
            return items.Sum(i => i.Quantity * i.UnitPrice);
        }

        public IEnumerable<OrderItem> GetReceivedItems(IEnumerable<OrderItem> items)
        {
            return items;
        }
    }

    public class OrderItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}
