using System.Linq;
using Xunit;

namespace LiteDB.Issues.Tests
{

    public class LiteDatabaseEnumPropertyTests : IClassFixture<LiteDatabaseFixture>
    {
        private readonly LiteDatabaseFixture _liteDatabaseFixture;

        public enum CustomerType
        {
            Potential,
            New,
            Loyal
        }
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public CustomerType Type { get; set; }
        }

        public LiteDatabaseEnumPropertyTests(LiteDatabaseFixture liteDatabaseFixture)
        {
            _liteDatabaseFixture = liteDatabaseFixture;
        }

        [Fact]
        public void FindUsingQueryObject_ByEnumProperty_ReturnsAppropriateRecords()
        {
            var customersCollection = _liteDatabaseFixture.Instance.GetCollection<Customer>();

            var customer = new Customer
            {
                Name = "John Doe",
                Type = CustomerType.New
            };

            customersCollection.Insert(customer);

            var customers = customersCollection
                .Find(Query.EQ(nameof(Customer.Type), CustomerType.New.ToString()))
                .ToArray();

            Assert.Single(customers.Where(c => c.Id == customer.Id));
        }

        [Fact]
        public void FindUsingLinqExpression_ByEnumProperty_ReturnsAppropriateRecords()
        {
            var customersCollection = _liteDatabaseFixture.Instance.GetCollection<Customer>();

            var customer = new Customer
            {
                Name = "John Doe",
                Type = CustomerType.New
            };

            customersCollection.Insert(customer);

            var customers = customersCollection
                .Find(x => x.Type == CustomerType.New)
                .ToArray();

            Assert.Single(customers.Where(c => c.Id == customer.Id));
        }
    }
}
