using LiteDB.Issues.Tests.Common;
using Xunit;

namespace LiteDB.Issues.Tests.Mapping
{
    public class NestedObjectMappingTests : IClassFixture<LiteDatabaseFixture>
    {
        private readonly LiteDatabaseFixture _liteDatabaseFixture;

        public class Customer
        {
            public int Id { get; set; }
            public Order Order { get; set; }
        }
        public class Order
        {
            public Customer Customer { get; set; }
        }

        public NestedObjectMappingTests(LiteDatabaseFixture liteDatabaseFixture)
        {
            _liteDatabaseFixture = liteDatabaseFixture;
        }

        [Fact]
        public void PersistWithNestedObject_NestedObjectIgnoresParent_NestedObjectDoesntHaveRefToParent()
        {
            // Arrange
            var customer = new Customer();
            var order = new Order();
            customer.Order = order;
            order.Customer = customer;

            // Act
            BsonMapper.Global.Entity<Customer>().Ignore(x => x.Order.Customer);

            var liteDatabase = _liteDatabaseFixture.Instance;
            var customersCollection = liteDatabase.GetCollection<Customer>();

            customersCollection.Insert(customer);
            var persistedCustomer = customersCollection.FindById(customer.Id);

            // Assert
            Assert.Null(persistedCustomer.Order.Customer);
        }
    }
}
