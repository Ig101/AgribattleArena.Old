using AgribattleArena.DBProvider.Contexts.StoreEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts
{
    public class StoreContext: DbContext
    {
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<ActorTransaction> ActorTransaction { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<ActorTransaction>().ToTable("actor_transaction");
            modelBuilder.Entity<ActorTransaction>().HasIndex(x => x.ProfileId);
            modelBuilder.Entity<Offer>().HasIndex(x => x.ProfileId);
            modelBuilder.Entity<OfferItem>().HasIndex(x => x.OfferId);
            modelBuilder.Entity<Skill>().HasIndex(x => x.ActorId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
