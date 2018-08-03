using System;
using Xunit;

namespace LiteDB.Issues.Tests
{
    public class LiteDatabaseInsertTests : IClassFixture<LiteDatabaseFixture>
    {
        private readonly LiteDatabaseFixture _liteDatabaseFixture;

        public class IntCustomer
        {
            public int? Id { get; set; }
            public string Name { get; set; }
        }

        public class GuidCustomer
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
        }

        public LiteDatabaseInsertTests(LiteDatabaseFixture liteDatabaseFixture)
        {
            _liteDatabaseFixture = liteDatabaseFixture;
        }

        [Fact]
        public void InsertRecord_WithNullableIntId_Succeed()
        {
            var customersCollection = _liteDatabaseFixture.Instance.GetCollection<IntCustomer>();

            var customer = new IntCustomer
            {
                Name = "John Doe",
            };

            customersCollection.Insert(customer);

            Assert.True(customersCollection.Exists(x => x.Id == customer.Id));
        }

        [Fact]
        public void InsertRecord_WithNullableGuidId_Succeed()
        {
            var customersCollection = _liteDatabaseFixture.Instance.GetCollection<GuidCustomer>();

            var customer = new GuidCustomer
            {
                Name = "John Doe",
            };

            customersCollection.Insert(customer);

            Assert.True(customersCollection.Exists(x => x.Id == customer.Id));
        }

    }
}
