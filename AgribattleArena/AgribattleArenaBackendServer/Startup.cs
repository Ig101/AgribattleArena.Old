using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Models.Natives;
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
            services.AddTransient<IProfilesServiceSceneLink, ProfilesService>();
            services.AddSingleton<IEngineService, EngineService>();
            services.AddTransient<IBattlefieldService, BattlefieldService>();
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
                cfg.CreateMap<Contexts.NativesEntities.Actor, ActorNative>();
                cfg.CreateMap<Contexts.NativesEntities.Buff, BuffNative>();
                cfg.CreateMap<Contexts.NativesEntities.Decoration, DecorationNative>()
                    .ForMember(d => d.DefaultArmor, o => o.MapFrom(c => c.TagSynergies));
                cfg.CreateMap<Contexts.NativesEntities.RoleModel, RoleModelNative>()
                    .ForMember(d => d.Skills, o => o.MapFrom(c => c.RoleModelSkills))
                    .ForMember(d => d.Armor,o => o.MapFrom(c => c.TagSynergies));
                cfg.CreateMap<Contexts.NativesEntities.Skill, SkillNative>();
                cfg.CreateMap<Contexts.NativesEntities.RoleModelSkill, SkillNative>()
                    .ForMember(d => d, o => o.MapFrom(c => c.Skill));
                cfg.CreateMap<Contexts.NativesEntities.SpecEffect, EffectNative>();
                cfg.CreateMap<Contexts.NativesEntities.SceneAction, ActionNative>()
                    .ForMember(d => d.Name,o => o.MapFrom(c=>c.Id));
                cfg.CreateMap<Contexts.NativesEntities.Tile, TileNative>();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
