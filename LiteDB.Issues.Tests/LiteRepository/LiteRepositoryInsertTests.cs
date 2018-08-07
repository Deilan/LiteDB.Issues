using System;
using LiteDB.Issues.Tests.Common;
using Xunit;

namespace LiteDB.Issues.Tests.LiteRepository
{

    public class LiteRepositoryInsertTests : IClassFixture<LiteRepositoryFixture>
    {
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

        private readonly LiteRepositoryFixture _liteRepositoryFixture;
        public LiteRepositoryInsertTests(LiteRepositoryFixture liteRepositoryFixture)
        {
            _liteRepositoryFixture = liteRepositoryFixture;
        }

        [Fact]
        public void InsertRecord_WithNullableIntId_Succeed()
        {
            var customer = new IntCustomer
            {
                Name = "John Doe",
            };

            _liteRepositoryFixture.Instance.Insert(customer);
        }

        [Fact]
        public void InsertRecord_WithNullableGuidId_Succeed()
        {
            var customer = new GuidCustomer
            {
                Name = "John Doe",
            };

            _liteRepositoryFixture.Instance.Insert(customer);
        }

    }
}
