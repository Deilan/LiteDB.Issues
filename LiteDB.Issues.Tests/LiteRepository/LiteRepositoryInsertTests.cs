using System;
using LiteDB.Issues.Tests.Common;
using Xunit;

namespace LiteDB.Issues.Tests.LiteRepository
{
    public class LiteRepositoryInsertTests : IDisposable
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
        public LiteRepositoryInsertTests()
        {
            _liteRepositoryFixture = new LiteRepositoryFixture();
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

        public void Dispose()
        {
            this._liteRepositoryFixture.Dispose();
        }
    }
}
