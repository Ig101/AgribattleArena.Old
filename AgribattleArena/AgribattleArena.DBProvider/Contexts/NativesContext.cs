using AgribattleArena.DBProvider.Contexts.NativeEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgribattleArena.DBProvider.Contexts
{
    public class NativesContext: DbContext
    {
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Buff> Buff { get; set; }
        public DbSet<Decoration> Decoration { get; set; }
        public DbSet<RoleModel> RoleModel { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<SpecEffect> SpecEffect { get; set; }
        public DbSet<Tile> Tile { get; set; }
        public DbSet<SceneAction> Action { get; set; }

        public NativesContext(DbContextOptions<NativesContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Buff>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Decoration>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<RoleModel>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Skill>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<SpecEffect>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Tile>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<RoleModelSkill>()
                .HasKey(t => new { t.RoleModelId, t.SkillId });
            modelBuilder.Entity<RoleModel>().ToTable("role_models");
            modelBuilder.Entity<RoleModelSkill>().ToTable("role_model_skill");
            modelBuilder.Entity<RoleModelSkill>().HasIndex(x => x.RoleModelId);
            modelBuilder.Entity<TagSynergy>().ToTable("tag_synergy");
            modelBuilder.Entity<TagSynergy>().HasIndex(x => x.RoleModelId);
            modelBuilder.Entity<TagSynergy>().HasIndex(x => x.DecorationId);
            modelBuilder.Entity<Tag>().HasIndex(x => x.DecorationId);
            modelBuilder.Entity<Tag>().HasIndex(x => x.ActorId);
            modelBuilder.Entity<Tag>().HasIndex(x => x.BuffId);
            modelBuilder.Entity<Tag>().HasIndex(x => x.SkillId);
            modelBuilder.Entity<Tag>().HasIndex(x => x.TileId);
            modelBuilder.Entity<Tag>().HasIndex(x => x.SpecEffectId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
