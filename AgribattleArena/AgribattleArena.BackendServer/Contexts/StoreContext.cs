using AgribattleArena.BackendServer.Contexts.StoreEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts
{
    public class StoreContext: DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<ActorTransaction> ActorTransactions { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorTransaction>().ToTable("actor_transaction");
            modelBuilder.Entity<ActorTransaction>().HasIndex(x => x.ProfileId);
            modelBuilder.Entity<Offer>().HasIndex(x => x.ProfileId);
            modelBuilder.Entity<OfferItem>().HasIndex(x => x.OfferId);
            modelBuilder.Entity<Skill>().HasIndex(x => x.ActorId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
