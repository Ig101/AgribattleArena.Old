using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.BackendServer.Services;
using AgribattleArena.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgribattleArena.BackendServer
{
    public class Startup
    {
        IHostingEnvironment env;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

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
            services.AddIdentity<DBProvider.Contexts.ProfileEntities.Profile, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                 .AddEntityFrameworkStores<ProfilesContext>()
                 .AddUserManager<ProfilesService>()
                 .AddDefaultTokenProviders();
            services.AddSingleton<Random>();
            services.AddSingleton<IStoredInfoService, StoredInfoService>();
            services.AddSingleton<IStoredInfoServiceGenerator, StoredInfoService>();

            services.AddTransient<IProfilesService, ProfilesService>();
            services.AddTransient<IProfilesServiceAggregated, ProfilesService>();

            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<INativesRepository, NativesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {

            AutoMapper.Mapper.Initialize(cfg =>
            {
                //Profile
                cfg.CreateMap<DBProvider.Contexts.ProfileEntities.Profile, Models.Profile.ProfileDto>();
                cfg.CreateMap<DBProvider.Contexts.ProfileEntities.Profile, Models.Profile.ProfileInfoDto>();
                cfg.CreateMap<DBProvider.Contexts.ProfileEntities.Actor, Models.Profile.ActorDto>();
                cfg.CreateMap<DBProvider.Contexts.ProfileEntities.Skill, string>()
                    .ConvertUsing(c => c.Native);

                //Store
                cfg.CreateMap<DBProvider.Contexts.StoreEntities.Offer, Models.Store.OfferDto>();
                cfg.CreateMap<DBProvider.Contexts.StoreEntities.Skill, string>()
                    .ConstructUsing(c => c.Native);
                cfg.CreateMap<DBProvider.Contexts.StoreEntities.OfferItem, Models.Store.ActorDto>()
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

                cfg.CreateMap<string, DBProvider.Contexts.StoreEntities.Skill>()
                    .ConstructUsing(c => new DBProvider.Contexts.StoreEntities.Skill() { Native = c });
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();

            services.GetRequiredService<IStoredInfoServiceGenerator>().SetupNew(services);

            app.UseMvc();
        }
    }
}
