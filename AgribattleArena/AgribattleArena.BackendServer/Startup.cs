using AgribattleArena.BackendServer.Contexts;
using AgribattleArena.BackendServer.Services;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AgribattleArena.BackendServer
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
            services.Configure<ConstantsConfig>(Configuration.GetSection("Constants"));
            //DbContexts
            services.AddDbContext<NativesContext>(o => o.UseMySql(Configuration["ConnectionStrings:NativesDB"]));
            services.AddDbContext<ProfilesContext>(o => o.UseMySql(Configuration["ConnectionStrings:ProfilesDB"]));
            services.AddDbContext<StoreContext>(o => o.UseMySql(Configuration["ConnectionStrings:StoreDB"]));
            //
            services.AddSignalR();
            services.AddIdentity<Contexts.ProfileEntities.Profile, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                 .AddEntityFrameworkStores<ProfilesContext>()
                 .AddUserManager<ProfilesService>()
                 .AddDefaultTokenProviders();
            services.AddScoped<IProfilesService, ProfilesService>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddSingleton<Random>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Contexts.ProfileEntities.Profile, Models.Profile.ProfileDto>();
                cfg.CreateMap<Contexts.ProfileEntities.Profile, Models.Profile.ProfileWithActorsDto>();
                cfg.CreateMap<Contexts.ProfileEntities.Actor, Models.Profile.ActorDto>();
                cfg.CreateMap<Contexts.ProfileEntities.Skill, string>()
                    .ConvertUsing(c => c.Native);
                cfg.CreateMap<Contexts.StoreEntities.Offer, Models.Store.OfferDto>();
                cfg.CreateMap<Contexts.StoreEntities.Skill, string>()
                    .ConstructUsing(c => c.Native);
                cfg.CreateMap<Contexts.StoreEntities.OfferItem, Models.Store.ActorDto>()
                    .ConvertUsing(c => new Models.Store.ActorDto()
                    {
                        Id = c.Id,
                        ActionPointsIncome = c.Actor.ActionPointsIncome,
                        Strength = c.Actor.Strength,
                        Willpower = c.Actor.Willpower,
                        Constitution = c.Actor.Constitution,
                        Speed = c.Actor.Speed,
                        Cost = c.Actor.Cost,
                        ActorNative = c.Actor.ActorNative,
                        AttackingSkillNative = c.Actor.AttackingSkillNative,
                        Name = c.Actor.Name,
                        Skills = AutoMapper.Mapper.Map<List<string>>(c.Actor.Skills)
                    });
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
