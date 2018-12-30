using System;
using LiteDB.Issues.Tests.Common;
using Xunit;

namespace LiteDB.Issues.Tests
{
    public class TypedCollectionModelPropertyTypeChangeTests : IDisposable
    {
        private readonly LiteDatabaseFixture _liteDatabaseFixture;
        private const string CustomerCollectionName = "Customer";

        public class NullableIntCustomer
        {
            public int Id { get; set; }
            public int? Sum { get; set; }
        }

        public class NullableDecimalCustomer
        {
            public int Id { get; set; }
            public decimal? Sum { get; set; }
        }

        public class IntCustomer
        {
            public int Id { get; set; }
            public int Sum { get; set; }
        }

        public class DecimalCustomer
        {
            public int Id { get; set; }
            public decimal Sum { get; set; }
        }

        public TypedCollectionModelPropertyTypeChangeTests()
        {
            _liteDatabaseFixture = new LiteDatabaseFixture();
        }

        [Fact]
        public void ChangeNullableIntToNullableDecimal_WithoutMapping_Succeeded()
        {
            // Arrange
            var customer = new NullableIntCustomer() { Sum = 100 };

            var liteDatabase = _liteDatabaseFixture.Instance;

            // Act
            var customersOldCollection = liteDatabase.GetCollection<NullableIntCustomer>(CustomerCollectionName);
            customersOldCollection.Insert(customer);

            var customersNewCollection = liteDatabase.GetCollection<NullableDecimalCustomer>(CustomerCollectionName);
            var customerNew = customersNewCollection.FindById(customer.Id);

            // Assert
            Assert.Equal(100, customerNew.Sum);
        }

        [Fact]
        public void ChangeNullableIntToNullableDecimal_WithMapping_Succeeded()
        {
            // Arrange
            var customer = new NullableIntCustomer() {Sum = 100};

            BsonMapper.Global.RegisterType<decimal?>
            (
                decimalValue => Convert.ToInt32(decimalValue),
                bson => bson.AsDecimal
            );

            BsonMapper.Global.RegisterType<decimal?>
            (
                decimalValue => Convert.ToInt32(decimalValue),
                bson => bson.AsDecimal
            );

            var liteDatabase = _liteDatabaseFixture.Instance;

            // Act
            var customersOldCollection = liteDatabase.GetCollection<NullableIntCustomer>(CustomerCollectionName);
            customersOldCollection.Insert(customer);

            var customersNewCollection = liteDatabase.GetCollection<NullableDecimalCustomer>(CustomerCollectionName);
            var customerNew = customersNewCollection.FindById(customer.Id);

            // Assert
            Assert.Equal(100, customerNew.Sum);
        }

        [Fact]
        public void ChangeIntToDecimal_WithoutMapping_Succeeded()
        {
            // Arrange
            var customer = new IntCustomer() { Sum = 100 };

            var liteDatabase = _liteDatabaseFixture.Instance;

            // Act
            var customersOldCollection = liteDatabase.GetCollection<IntCustomer>(CustomerCollectionName);
            customersOldCollection.Insert(customer);

            var customersNewCollection = liteDatabase.GetCollection<DecimalCustomer>(CustomerCollectionName);
            var customerNew = customersNewCollection.FindById(customer.Id);

            // Assert
            Assert.Equal(100, customerNew.Sum);
        }

        [Fact]
        public void ChangeIntToDecimal_WithMapping_Succeeded()
        {
            // Arrange
            var customer = new IntCustomer() { Sum = 100 };

            BsonMapper.Global.RegisterType<decimal>
            (
                decimalValue => Convert.ToInt32(decimalValue),
                bson => bson.AsDecimal
            );

            BsonMapper.Global.RegisterType<decimal>
            (
                decimalValue => Convert.ToInt32(decimalValue),
                bson => bson.AsDecimal
            );

            var liteDatabase = _liteDatabaseFixture.Instance;

            // Act
            var customersOldCollection = liteDatabase.GetCollection<IntCustomer>(CustomerCollectionName);
            customersOldCollection.Insert(customer);

            var customersNewCollection = liteDatabase.GetCollection<DecimalCustomer>(CustomerCollectionName);
            var customerNew = customersNewCollection.FindById(customer.Id);

            // Assert
            Assert.Equal(100, customerNew.Sum);
        }

        [Fact]
        public void ChangeIntToNullableDecimal_WithoutMapping_Succeeded()
        {
            // Arrange
            var customer = new IntCustomer() { Sum = 100 };

            var liteDatabase = _liteDatabaseFixture.Instance;

            // Act
            var customersOldCollection = liteDatabase.GetCollection<IntCustomer>(CustomerCollectionName);
            customersOldCollection.Insert(customer);

            var customersNewCollection = liteDatabase.GetCollection<DecimalCustomer>(CustomerCollectionName);
            var customerNew = customersNewCollection.FindById(customer.Id);

            // Assert
            Assert.Equal(100, customerNew.Sum);
        }

        [Fact]
        public void ChangeIntToNullableDecimal_WithMapping_Succeeded()
        {
            // Arrange
            var customer = new IntCustomer() { Sum = 100 };

            BsonMapper.Global.RegisterType<decimal?>
            (
                decimalValue => Convert.ToInt32(decimalValue),
                bson => bson.AsDecimal
            );

            BsonMapper.Global.RegisterType<decimal?>
            (
                decimalValue => Convert.ToInt32(decimalValue),
                bson => bson.AsDecimal
            );

            var liteDatabase = _liteDatabaseFixture.Instance;

            // Act
            var customersOldCollection = liteDatabase.GetCollection<IntCustomer>(CustomerCollectionName);
            customersOldCollection.Insert(customer);

            var customersNewCollection = liteDatabase.GetCollection<DecimalCustomer>(CustomerCollectionName);
            var customerNew = customersNewCollection.FindById(customer.Id);

            // Assert
            Assert.Equal(100, customerNew.Sum);
        }

        public void Dispose()
        {
            this._liteDatabaseFixture.Dispose();
        }
    }
}
