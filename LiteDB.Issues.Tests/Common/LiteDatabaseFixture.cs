using System;
using System.IO;

namespace LiteDB.Issues.Tests.Common
{
    public sealed class LiteDatabaseFixture : IDisposable
    {
        private readonly string _fileName;
        public LiteDB.LiteDatabase Instance { get; }
        public LiteDatabaseFixture()
        {
            _fileName = Path.GetTempFileName();
            Instance = new LiteDB.LiteDatabase(_fileName);
        }

        public void Dispose()
        {
            Instance.Dispose();
            File.Delete(_fileName);
        }
    }
}
