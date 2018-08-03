using Xunit;

namespace LiteDB.Issues.Tests
{

    public class LiteRepositoryQueryTests : IClassFixture<LiteRepositoryFixture>
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
            public CustomerType? Type { get; set; }
        }
        private readonly LiteRepositoryFixture _liteRepositoryFixture;

        public LiteRepositoryQueryTests(LiteRepositoryFixture liteRepositoryFixture)
        {
            _liteRepositoryFixture = liteRepositoryFixture;
        }

        [Fact]
        public void QueryRecord_ByNullableEnumProperty_ReturnsAllStoredRecords()
        {
            var customer = new Customer
            {
                Name = "John Doe",
                Type = CustomerType.New
            };

            _liteRepositoryFixture.Instance.Insert(customer);

            var results = _liteRepositoryFixture.Instance.Query<Customer>()
                .Where(x => x.Type == CustomerType.New)
                .ToArray();

            Assert.Single(results);
        }

        [Fact]
        public void QueryRecord_ByNullableEnumPropertyValue_ReturnsAllStoredRecords()
        {
            var customer = new Customer
            {
                Name = "John Doe",
                Type = CustomerType.New
            };

            _liteRepositoryFixture.Instance.Insert(customer);

            var results = _liteRepositoryFixture.Instance.Query<Customer>()
                .Where(x => x.Type.Value == CustomerType.New)
                .ToArray();

            Assert.Single(results);
        }
    }
}
