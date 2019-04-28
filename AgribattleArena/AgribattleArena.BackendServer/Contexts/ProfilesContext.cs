using AgribattleArena.BackendServer.Contexts.ProfileEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer.Contexts
{
    public class ProfilesContext: IdentityDbContext<Profile>
    {
        public DbSet<Actor> Actors { get; set; }

        public ProfilesContext(DbContextOptions<ProfilesContext> options)
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
            modelBuilder.Entity<Skill>().HasIndex(x => x.ActorId);
            modelBuilder.Entity<TagsArmor>().HasIndex(x => x.ActorId);
            modelBuilder.Entity<TagsArmor>().ToTable("tags_armor");
            base.OnModelCreating(modelBuilder);
        }
    }
}
