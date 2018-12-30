using System;
using System.Linq;
using LiteDB.Issues.Tests.Common;
using Xunit;

namespace LiteDB.Issues.Tests.LiteRepository
{
    public class LiteRepositoryEnumPropertyTests : IDisposable
    {
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
        private readonly LiteRepositoryFixture _liteRepositoryFixture;

        public LiteRepositoryEnumPropertyTests()
        {
            _liteRepositoryFixture = new LiteRepositoryFixture();
        }

        [Fact]
        public void QueryUsingQueryObject_ByEnumPropertyName_ReturnsAppropriateRecords()
        {
            var liteRepository = _liteRepositoryFixture.Instance;
            var customer = new Customer
            {
                Name = "John Doe",
                Type = CustomerType.New
            };

            liteRepository.Insert(customer);

            var customers = liteRepository.Query<Customer>()
                .Where(Query.EQ(nameof(Customer.Type), CustomerType.New.ToString()))
                .ToArray();

            Assert.Single(customers.Where(c => c.Id == customer.Id));
        }

        [Fact]
        public void QueryUsingLinqExpression_ByEnumProperty_ReturnsAllStoredRecords()
        {
            var liteRepository = _liteRepositoryFixture.Instance;
            var customer = new Customer
            {
                Name = "John Doe",
                Type = CustomerType.New
            };

            liteRepository.Insert(customer);

            var customers =liteRepository.Query<Customer>()
                .Where(x => x.Type == CustomerType.New)
                .ToArray();

            Assert.Single(customers.Where(c => c.Id == customer.Id));
        }

        public void Dispose()
        {
            this._liteRepositoryFixture.Dispose();
        }
    }
}
