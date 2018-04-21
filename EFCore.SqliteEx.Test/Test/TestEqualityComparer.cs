using System;
using System.Collections.Generic;

namespace Bricelam.EntityFrameworkCore.Sqlite.Test
{
    class TestEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
            => typeof(T) == typeof(double)
                ? Math.Round((double)(object)x, 8) == Math.Round((double)(object)y, 8)
                : EqualityComparer<T>.Default.Equals(x, y);

        public int GetHashCode(T obj)
            => throw new NotImplementedException();
    }
}
