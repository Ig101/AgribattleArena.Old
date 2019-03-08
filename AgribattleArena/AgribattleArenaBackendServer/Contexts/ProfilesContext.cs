using AgribattleArenaBackendServer.Contexts.ProfilesEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts
{
    public class ProfilesContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Right> Rights { get; set; }

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
            modelBuilder.Entity<ProfileRole>()
                .HasKey(t => new { t.ProfileLogin, t.RoleId });
            modelBuilder.Entity<RoleRight>()
                .HasKey(t => new { t.RightId, t.RoleId });
            modelBuilder.Entity<ProfileRole>().HasIndex(x => x.ProfileLogin);
            modelBuilder.Entity<ProfileRole>().ToTable("profile_role");
            modelBuilder.Entity<RoleRight>().HasIndex(x => x.RoleId);
            modelBuilder.Entity<RoleRight>().ToTable("role_right");
            modelBuilder.Entity<Skill>().HasIndex(x => x.ActorId);
            modelBuilder.Entity<TagsArmor>().HasIndex(x => x.ActorId);
            modelBuilder.Entity<TagsArmor>().ToTable("tags_armor");
            base.OnModelCreating(modelBuilder);
        }
    }
}
