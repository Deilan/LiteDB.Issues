using System;
using LiteDB.Issues.Tests.Common;
using Xunit;

namespace LiteDB.Issues.Tests
{
    public class SchemelessCollectionModelPropertyTypeChangeTests : IDisposable
    {
        private const string CustomerCollectionName = "Customer";
        private readonly LiteDatabaseFixture _liteDatabaseFixture;

        public class NullableIntCustomer
        {
            public int Id { get; set; }
            public int? Sum { get; set; }
        }

        public class IntCustomer
        {
            public int Id { get; set; }
            public int Sum { get; set; }
        }

        public SchemelessCollectionModelPropertyTypeChangeTests()
        {
            _liteDatabaseFixture = new LiteDatabaseFixture();
        }

        [Fact]
        public void ChangeNullableIntToNullableDecimal_WithoutMapping_Succeeded()
        {
            // Arrange
            var customer = new NullableIntCustomer() {Sum = 100};

            var liteDatabase = _liteDatabaseFixture.Instance;

            // Act
            var customersOldCollection = liteDatabase.GetCollection(CustomerCollectionName);
            customersOldCollection.Insert(BsonMapper.Global.ToDocument(customer));

            var customersNewCollection = liteDatabase.GetCollection(CustomerCollectionName);
            var customerNew = customersNewCollection.FindById(customer.Id);

            // Assert
            Assert.Equal(100, customerNew.RawValue["Sum"].AsDecimal);
        }

        [Fact]
        public void ChangeIntToDecimal_WithoutMapping_Succeeded()
        {
            // Arrange
            var customer = new IntCustomer() { Sum = 100 };

            var liteDatabase = _liteDatabaseFixture.Instance;

            // Act
            var customersOldCollection = liteDatabase.GetCollection(CustomerCollectionName);
            customersOldCollection.Insert(BsonMapper.Global.ToDocument(customer));

            var customersNewCollection = liteDatabase.GetCollection(CustomerCollectionName);
            var customerNew = customersNewCollection.FindById(customer.Id);

            // Assert
            Assert.Equal(100, customerNew.RawValue["Sum"].AsDecimal);
        }

        public void Dispose()
        {
            this._liteDatabaseFixture.Dispose();
        }
    }
}
