using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Contexts.ProfilesEntities;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.Helpers;
using AgribattleArenaBackendServer.Engine.NativeManager;
using AgribattleArenaBackendServer.Hubs;
using AgribattleArenaBackendServer.Models.Natives;
using AgribattleArenaBackendServer.Models.Profiles;
using AgribattleArenaBackendServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        IHostingEnvironment env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            if (!env.IsDevelopment())
                services.Configure<MvcOptions>(o =>
                o.Filters.Add(new RequireHttpsAttribute()));
            //Change it
            string connectionString = @"Server=localhost;Database=aa_natives;Uid=agribattleArena;Pwd=admin;";
            services.AddDbContext<NativesContext>(o => o.UseMySql(connectionString));
            string connectionString2 = @"Server=localhost;Database=aa_profiles;Uid=agribattleArena;Pwd=admin;";
            services.AddDbContext<ProfilesContext>(o => o.UseMySql(connectionString2));
            //

            services.AddIdentity<Profile, IdentityRole>(options =>
                options.Password.RequireDigit = false)
                .AddEntityFrameworkStores<ProfilesContext>()
                .AddUserManager<ProfilesService>()
                .AddDefaultTokenProviders();
            services.AddTransient<INativesService, NativesService>();
            services.AddTransient<INativesServiceSceneLink, NativesService>();
            services.AddSingleton<IEngineService, EngineService>();
            services.AddSingleton<IEngineServiceQueueLink, EngineService>();
            services.AddSingleton<IEngineServiceHubLink, EngineService>();
            services.AddTransient<IBattleServiceQueueLink, BattleService>();
            services.AddSingleton<IQueueingStorageService, QueueingStorageService>();
            services.AddSingleton<IQueueingStorageServiceHubLink, QueueingStorageService>();
            services.AddScoped<IProfilesService, ProfilesService>();
            services.AddHostedService<QueueService>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
                cfg.CreateMap<Contexts.ProfilesEntities.Actor, RoleModelNativeToAddDto>()
                    .ForMember(d => d.AttackSkill, o => o.MapFrom(c => c.AttackingSkillNative));
                cfg.CreateMap<Contexts.ProfilesEntities.Skill, string>()
                    .ForMember(d => d, o => o.MapFrom(c => c.Native));
                cfg.CreateMap<Contexts.ProfilesEntities.TagsArmor, TagsArmorDto>();
                cfg.CreateMap<Contexts.ProfilesEntities.Actor, PartyActorDto>();
                cfg.CreateMap<Contexts.ProfilesEntities.Party, PartyDto>();
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            { 
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSignalR(routes => {
                routes.MapHub<BattleHub>("/battle");
            });
            app.UseMvc();
        }
    }
}
