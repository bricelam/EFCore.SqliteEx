namespace Bricelam.EntityFrameworkCore.Sqlite.Test
{
    class TestEntity<TValue>
    {
        public int Id { get; set; }
        public TValue Value { get; set; }
    }

    static class TestEntity
    {
        public static TestEntity<TValue> Create<TValue>(TValue value)
            => new TestEntity<TValue> { Value = value };
    }
}
