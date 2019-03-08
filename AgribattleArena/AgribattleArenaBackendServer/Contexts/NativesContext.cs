using AgribattleArenaBackendServer.Contexts.NativesEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArenaBackendServer.Contexts
{
    public class NativesContext: DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Buff> Buffs { get; set; }
        public DbSet<Decoration> Decorations { get; set; }
        public DbSet<RoleModel> RoleModes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SpecEffect> SpecEffects { get; set; }
        public DbSet<Tile> Tiles { get; set; }

        public NativesContext(DbContextOptions<NativesContext> options)
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
            modelBuilder.Entity<RoleModelSkill>()
                .HasKey(t => new { t.RoleModelId, t.SkillId });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoleModel>().ToTable("role_models");
            modelBuilder.Entity<RoleModelSkill>().ToTable("role_model_skill");
            modelBuilder.Entity<TagSynergy>().ToTable("tag_synergy");
        }
    }
}
