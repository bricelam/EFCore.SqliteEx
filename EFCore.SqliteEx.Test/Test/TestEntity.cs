namespace Bricelam.EntityFrameworkCore.Sqlite.Test
{
    class TestEntity<TProperty>
    {
        public int Id { get; set; }
        public TProperty Value { get; set; }
    }
}
