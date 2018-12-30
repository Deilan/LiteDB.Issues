using System;
using System.IO;

namespace LiteDB.Issues.Tests.Common
{
    public sealed class LiteRepositoryFixture : IDisposable
    {
        private readonly MemoryStream _stream;
        public LiteDB.LiteRepository Instance { get; }
        public LiteRepositoryFixture()
        {
            _stream = new MemoryStream();
            Instance = new LiteDB.LiteRepository(_stream);
        }

        public void Dispose()
        {
            Instance.Dispose();
            _stream.Dispose();
        }
    }
}
