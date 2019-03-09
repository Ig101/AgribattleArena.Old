using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Models.Natives;
using AgribattleArenaBackendServer.Models.Profiles;
using AgribattleArenaBackendServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AgribattleArenaBackendServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //Change it
            string connectionString = @"Server=localhost;Database=aa_natives;Uid=agribattleArena_bl;Pwd=Qdmin123/;";
            services.AddDbContext<NativesContext>(o => o.UseMySql(connectionString));
         //   connectionString = @"Server=localhost;Database=aa_profiles;Uid=agribattleArena_bl;Pwd=Qdmin123/;";
           // services.AddDbContext<ProfilesContext>(o => o.UseMySql(connectionString));
            //
            services.AddTransient<INativesService, NativesService>();
            services.AddTransient<INativesServiceSceneLink, NativesService>();
            services.AddTransient<IProfilesService, ProfilesService>();
            services.AddTransient<IProfilesServiceSceneLink, ProfilesMockService>();
            services.AddSingleton<IEngineService, EngineService>();
            services.AddTransient<IBattlefieldService, BattlefieldService>();
            services.AddSingleton<IQueueingService, QueueingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Contexts.NativesEntities.Actor, ActorNativeDto>();
                cfg.CreateMap<Contexts.NativesEntities.Buff, BuffNativeDto>();
                cfg.CreateMap<Contexts.NativesEntities.Decoration, DecorationNativeDto>()
                    .ForMember(d => d.DefaultArmor, o => o.MapFrom(c => c.TagSynergies));
                cfg.CreateMap<Contexts.NativesEntities.RoleModel, RoleModelNativeDto>()
                    .ForMember(d => d.Skills, o => o.MapFrom(c => c.RoleModelSkills))
                    .ForMember(d => d.Armor,o => o.MapFrom(c => c.TagSynergies));
                cfg.CreateMap<Contexts.NativesEntities.Skill, SkillNativeDto>();
                cfg.CreateMap<Contexts.NativesEntities.RoleModelSkill, SkillNativeDto>()
                    .ForMember(d => d, o => o.MapFrom(c => c.Skill));
                cfg.CreateMap<Contexts.NativesEntities.SpecEffect, EffectNativeDto>();
                cfg.CreateMap<Contexts.NativesEntities.SceneAction, ActionNativeDto>()
                    .ForMember(d => d.Name,o => o.MapFrom(c=>c.Id));
                cfg.CreateMap<Contexts.NativesEntities.Tile, TileNativeDto>();
                cfg.CreateMap<TagsArmorDto, TagSynergy>()
                    .ForMember(d => d.TargetTag, o => o.MapFrom(c => c.Tag));
                cfg.CreateMap<PlayerActorDto, RoleModelNativeToAddDto>()
                    .ForMember(d => d.AttackSkill, o => o.MapFrom(c => c.AttackingSkillNative));
                cfg.CreateMap<Contexts.ProfilesEntities.Skill, string>()
                    .ForMember(d => d, o => o.MapFrom(c => c.Native));
                cfg.CreateMap<Contexts.ProfilesEntities.TagsArmor, TagsArmorDto>();
                cfg.CreateMap<Contexts.ProfilesEntities.Actor, PlayerActorDto>();
                cfg.CreateMap<Contexts.ProfilesEntities.Player, PlayerDto>();
                cfg.CreateMap<Contexts.ProfilesEntities.Profile, ProfileDto>();
                cfg.CreateMap<Contexts.ProfilesEntities.Role, RoleDto>();
                cfg.CreateMap<Contexts.ProfilesEntities.Right, RightDto>();
                cfg.CreateMap<Contexts.ProfilesEntities.RoleRight, RightDto>()
                    .ForMember(d => d, o => o.MapFrom(c => c.Right));
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
