using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LiteDB.Issues.Tests
{
    public sealed class LiteDatabaseFixture : IDisposable
    {
        private readonly string _fileName;
        public LiteDatabase Instance { get; }
        public LiteDatabaseFixture()
        {
            _fileName = Path.GetTempFileName();
            Instance = new LiteDatabase(_fileName);
        }

        public void Dispose()
        {
            Instance.Dispose();
            File.Delete(_fileName);
        }
    }
}
