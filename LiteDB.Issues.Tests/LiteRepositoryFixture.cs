using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Text;

namespace LiteDB.Issues.Tests
{
    public sealed class LiteRepositoryFixture : IDisposable
    {
        private readonly string _fileName;
        public LiteRepository Instance { get; }
        public LiteRepositoryFixture()
        {
            _fileName = Path.GetTempFileName();
            Instance = new LiteRepository(_fileName);
        }

        public void Dispose()
        {
            Instance.Dispose();
            File.Delete(_fileName);
        }
    }
}
