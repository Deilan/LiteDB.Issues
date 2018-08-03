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
            var col = _liteDatabaseFixture.Instance.GetCollection<IntCustomer>();

            var customer = new IntCustomer
            {
                Name = "John Doe",
            };

            col.Insert(customer);

            Assert.True(col.Exists(x => x.Id == customer.Id));
        }

        [Fact]
        public void InsertRecord_WithNullableGuidId_Succeed()
        {
            var col = _liteDatabaseFixture.Instance.GetCollection<GuidCustomer>();

            var customer = new GuidCustomer
            {
                Name = "John Doe",
            };

            col.Insert(customer);

            Assert.True(col.Exists(x => x.Id == customer.Id));
        }

    }
}
