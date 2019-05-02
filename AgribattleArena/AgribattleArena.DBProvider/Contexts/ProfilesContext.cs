using AgribattleArena.DBProvider.Contexts.ProfileEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts
{
    public class ProfilesContext: IdentityDbContext<Profile>
    {
        public DbSet<Actor> Actor { get; set; }
        public DbSet<RevelationLevel> RevelationLevel { get; set; }

        public ProfilesContext(DbContextOptions<ProfilesContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RevelationLevel>().HasKey(x => x.Level);
            modelBuilder.Entity<RevelationLevel>().Property(x => x.Level).ValueGeneratedNever();
            modelBuilder.Entity<Actor>().HasIndex(x => x.ProfileId);
            modelBuilder.Entity<VictoryStats>().HasIndex(x => x.ProfileId);
            modelBuilder.Entity<Skill>().HasIndex(x => x.ActorId);
            modelBuilder.Entity<VictoryStats>().ToTable("victory_stats");
            modelBuilder.Entity<RevelationLevel>().ToTable("revelation_level");
            base.OnModelCreating(modelBuilder);
        }
    }
}
