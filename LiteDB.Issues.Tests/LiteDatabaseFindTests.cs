using System.Linq;
using Xunit;

namespace LiteDB.Issues.Tests
{

    public class LiteDatabaseFindTests : IClassFixture<LiteDatabaseFixture>
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
            public CustomerType? Type { get; set; }
        }

        public LiteDatabaseFindTests(LiteDatabaseFixture liteDatabaseFixture)
        {
            _liteDatabaseFixture = liteDatabaseFixture;
        }

        [Fact]
        public void FindRecords_ByNullableEnumProperty_ReturnsAppropriateRecords()
        {
            var col = _liteDatabaseFixture.Instance.GetCollection<Customer>();

            var customer = new Customer
            {
                Name = "John Doe",
                Type = CustomerType.New
            };

            col.Insert(customer);

            var results = col.Find(x => x.Type == CustomerType.New);

            Assert.Single(results);
        }

        [Fact]
        public void FindRecords_ByNullableEnumPropertyValue_ReturnsAppropriateRecords()
        {
            var col = _liteDatabaseFixture.Instance.GetCollection<Customer>();

            var customer = new Customer
            {
                Name = "John Doe",
                Type = CustomerType.New
            };

            col.Insert(customer);

            var results = col.Find(x => x.Type.Value == CustomerType.New);

            Assert.Single(results);
        }
    }
}
