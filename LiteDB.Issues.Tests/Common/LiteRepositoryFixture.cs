using System;
using System.IO;

namespace LiteDB.Issues.Tests.Common
{
    public sealed class LiteRepositoryFixture : IDisposable
    {
        private readonly string _fileName;
        public LiteDB.LiteRepository Instance { get; }
        public LiteRepositoryFixture()
        {
            _fileName = Path.GetTempFileName();
            Instance = new LiteDB.LiteRepository(_fileName);
        }

        public void Dispose()
        {
            Instance.Dispose();
            File.Delete(_fileName);
        }
    }
}
