using Microsoft.EntityFrameworkCore;

namespace ComosEntityFramework.Data
{
    public class CosmosDataContext : DbContext
    {
        public CosmosDataContext(DbContextOptions<CosmosDataContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultContainer("TodoItems");
            modelBuilder.Entity<TodoItem>().HasNoDiscriminator();
        }
    }
}
