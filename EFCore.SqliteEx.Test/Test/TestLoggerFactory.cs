using System;
using Microsoft.Extensions.Logging;

namespace Bricelam.EntityFrameworkCore.Sqlite.Test
{
    class TestLoggerFactory : ILoggerFactory
    {
        public TestLogger Logger { get; } = new TestLogger();

        public void AddProvider(ILoggerProvider provider)
            => throw new NotImplementedException();

        public ILogger CreateLogger(string categoryName)
            => Logger;

        public void Dispose()
        {
        }
    }
}
