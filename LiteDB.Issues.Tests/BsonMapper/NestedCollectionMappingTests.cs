using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB.Issues.Tests.Common;
using Xunit;

namespace LiteDB.Issues.Tests
{
    public class NestedCollectionMappingTests : IClassFixture<LiteDatabaseFixture>
    {
        private readonly LiteDatabaseFixture _liteDatabaseFixture;

        public class Customer
        {
            public int Id { get; set; }
            public ICollection<Order> Orders { get; set; }
        }
        public class Order
        {
            public Customer Customer { get; set; }
        }

        public NestedCollectionMappingTests(LiteDatabaseFixture liteDatabaseFixture)
        {
            _liteDatabaseFixture = liteDatabaseFixture;
        }

        [Fact]
        public void PersistWithNestedCollection_NestedObjectIgnoresParent_DoesntHaveRefToParentAfterRead()
        {
            // Arrange
            var customer = new Customer
            {
                Orders = new List<Order>()
            };
            var order = new Order
            {
                Customer = customer
            };
            customer.Orders.Add(order);

            // Act
            BsonMapper.Global.Entity<Customer>().Ignore(x => x.Orders.First().Customer);

            var liteDatabase = _liteDatabaseFixture.Instance;
            var customersCollection = liteDatabase.GetCollection<Customer>();

            customersCollection.Insert(customer);
            var persistedCustomer = customersCollection.FindById(customer.Id);

            // Assert
            Assert.Null(persistedCustomer.Orders.First().Customer);
        }
    }
}
