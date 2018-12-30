using System;
using System.IO;

namespace LiteDB.Issues.Tests.Common
{
    public sealed class LiteDatabaseFixture : IDisposable
    {
        private readonly MemoryStream _stream;
        public LiteDB.LiteDatabase Instance { get; }
        public LiteDatabaseFixture()
        {
            _stream = new MemoryStream();
            Instance = new LiteDB.LiteDatabase(_stream);
        }

        public void Dispose()
        {
            Instance.Dispose();
            _stream.Dispose();
        }
    }
}
