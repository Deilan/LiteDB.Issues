using System;
using LiteDB.Issues.Tests.Common;
using Xunit;

namespace LiteDB.Issues.Tests.Mapping
{
    public class EntityMappingConfigTests : IDisposable
    {
        private readonly LiteDatabaseFixture _liteDatabaseFixture;

        public class Customer
        {
            public int Id { get; set; }
            public decimal Coefficient { get; set; }
            public decimal? Sum { get; set; }
            public decimal? AdjustedSum => Sum * Coefficient;
        }

        public EntityMappingConfigTests()
        {
            _liteDatabaseFixture = new LiteDatabaseFixture();
        }

        [Fact]
        public void IgnoreProperty_Twice_DoesntAddFieldToDocument()
        {
            // Arrange
            var customer = new Customer
            {
                Sum = 100m,
                Coefficient = 0.5m
            };

            var liteDatabase = _liteDatabaseFixture.Instance;

            // Act
            var mapper = BsonMapper.Global;
            mapper.Entity<Customer>()
                .Ignore(x => x.AdjustedSum);
            // repeatable ignore
            mapper.Entity<Customer>()
                .Ignore(x => x.AdjustedSum);

            var customersCollection = liteDatabase.GetCollection("Customer");

            customersCollection.Insert(mapper.ToDocument(customer));
            var persistedCustomer = customersCollection.FindById(customer.Id);

            // Assert
            Assert.False(persistedCustomer.TryGetValue("AdjustedSum", out _));
        }

        public void Dispose()
        {
            this._liteDatabaseFixture.Dispose();
        }
    }
}
