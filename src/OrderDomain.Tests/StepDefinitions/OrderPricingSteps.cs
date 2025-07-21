using TechTalk.SpecFlow;
using Xunit;
using System.Collections.Generic;

namespace OrderDomain.Tests.StepDefinitions
{
    [Binding]
    public class OrderPricingSteps
    {
        private List<OrderDomain.OrderItem> orderItems;
        private int totalAmount;
        private List<OrderDomain.OrderItem> receivedItems;
        private OrderDomain.OrderService orderService = new OrderDomain.OrderService();

        [Given("no promotions are applied")]
        public void GivenNoPromotionsAreApplied()
        {
            // 無需設定，預設無優惠
        }

        [When("a customer places an order with:")]
        public void WhenACustomerPlacesAnOrderWith(Table table)
        {
            orderItems = new List<OrderDomain.OrderItem>();
            foreach (var row in table.Rows)
            {
                orderItems.Add(new OrderDomain.OrderItem {
                    ProductName = row["productName"],
                    Quantity = int.Parse(row["quantity"]),
                    UnitPrice = int.Parse(row["unitPrice"])
                });
            }
            totalAmount = orderService.CalculateTotalAmount(orderItems);
            receivedItems = orderService.GetReceivedItems(orderItems).ToList();
        }

        [Then("the order summary should be:")]
        public void ThenTheOrderSummaryShouldBe(Table table)
        {
            Assert.Equal(int.Parse(table.Rows[0]["totalAmount"]), totalAmount);
        }

        [Then("the customer should receive:")]
        public void ThenTheCustomerShouldReceive(Table table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Assert.Equal(table.Rows[i]["productName"], receivedItems[i].ProductName);
                Assert.Equal(int.Parse(table.Rows[i]["quantity"]), receivedItems[i].Quantity);
            }
        }
    }
}
