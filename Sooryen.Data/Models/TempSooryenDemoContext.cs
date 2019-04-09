using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Sooryen.Data.Models.Mapping;

namespace Sooryen.Data.Models
{
    public partial class TempSooryenDemoContext : DbContext
    {
        static TempSooryenDemoContext()
        {
            Database.SetInitializer<TempSooryenDemoContext>(null);
        }

        public TempSooryenDemoContext()
            : base("Name=TempSooryenDemoContext")
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NoteMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
