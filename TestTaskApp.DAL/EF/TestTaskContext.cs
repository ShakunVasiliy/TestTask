using System;
using System.Data.Entity;

using TestTaskApp.DAL.Entities;

namespace TestTaskApp.DAL.EF
{
    public class TestTaskContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        public TestTaskContext(string connectionString)
            : base(connectionString)
        { }
    }
}
