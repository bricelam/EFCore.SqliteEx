using System;
using Microsoft.EntityFrameworkCore;

namespace Bricelam.EntityFrameworkCore.Sqlite.Test
{
    class TestContext : DbContext
    {
        public DbSet<TestEntity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseSqlite("Data Source=:memory:")
                .UseSqliteExtensions();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.Entity<TestEntity>()
                .HasData(new TestEntity { Id = 1, TimeSpan = new TimeSpan(20, 20, 8) });
    }
}
