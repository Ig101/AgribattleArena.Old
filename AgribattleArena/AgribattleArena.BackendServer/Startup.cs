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
using AgribattleArena.BackendServer.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AgribattleArena.BackendServer.Models.Battle;
using AgribattleArena.BackendServer.Models.Battle.Synchronization;

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

        async Task CreateAdminUserIsNotExists(UserManager<DBProvider.Contexts.ProfileEntities.Profile> userManager, RoleManager<IdentityRole> roleManager,
            string adminPassword)
        {
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                var result = await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            var user = userManager.FindByNameAsync("admin").Result;
            if (user == null)
            {
                user = new DBProvider.Contexts.ProfileEntities.Profile()
                {
                    UserName = "admin",
                    Email = "admin@noemail.com",
                    Resources = 0,
                    DonationResources = 0,
                    Revelations = 0
                };
                var result = await userManager.CreateAsync(user, adminPassword);
            }
            if (!await userManager.IsInRoleAsync(user, "admin"))
            {
                var result = await userManager.AddToRoleAsync(user, "admin");            
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            if (!env.IsDevelopment())
                services.Configure<MvcOptions>(o =>
                o.Filters.Add(new RequireHttpsAttribute()));
            services.Configure<ConstantsConfig>(Configuration.GetSection("Constants"));
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:4200").AllowCredentials();
                }));
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

            services.AddHostedService<HostedBattleService>();
            services.AddSingleton<IBattleService, BattleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {

            AutoMapper.Mapper.Initialize(cfg =>
            {
                //Profile
                cfg.CreateMap<DBProvider.Contexts.ProfileEntities.Profile, Models.Profile.ProfileDto>()
                    .ForMember(d => d.RevelationLevel, o => o.UseDestinationValue());
                cfg.CreateMap<DBProvider.Contexts.ProfileEntities.Profile, Models.Profile.ProfileInfoDto>()
                    .ForMember(d => d.RevelationLevel, o => o.UseDestinationValue());
                cfg.CreateMap<DBProvider.Contexts.ProfileEntities.Profile, Models.Profile.ProfileCredentialsDto>();
                cfg.CreateMap<DBProvider.Contexts.ProfileEntities.Actor, Models.Profile.ActorDto>();
                cfg.CreateMap<DBProvider.Contexts.ProfileEntities.Skill, string>()
                    .ConvertUsing(c => c.Native);

                cfg.CreateMap<Models.Profile.ActorDto, DBProvider.Contexts.ProfileEntities.Actor>()
                .ConvertUsing(c => new DBProvider.Contexts.ProfileEntities.Actor()
                {
                    ActionPointsIncome = c.ActionPointsIncome,
                    ActorNative = c.ActorNative,
                    AttackingSkillNative = c.AttackingSkillNative,
                    Constitution = c.Constitution,
                    Name = c.Name,
                    Skills = AutoMapper.Mapper.Map<List<DBProvider.Contexts.ProfileEntities.Skill>>(c.Skills),
                    Speed = c.Speed,
                    Strength = c.Strength,
                    Willpower =c.Willpower,
                    InParty = c.InParty
                });
                cfg.CreateMap<string, DBProvider.Contexts.ProfileEntities.Skill>()
                    .ConvertUsing(c => new DBProvider.Contexts.ProfileEntities.Skill() { Native = c });

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

                //Battle
                cfg.CreateMap<Engine.ForExternalUse.Synchronization.ISyncEventArgs, SynchronizerDto>();
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseSignalR(configure =>
            {
                configure.MapHub<BattleHub>("/hubs/battle");
            });


            services.GetRequiredService<IStoredInfoServiceGenerator>().SetupNew(services);
            CreateAdminUserIsNotExists(services.GetRequiredService<UserManager<DBProvider.Contexts.ProfileEntities.Profile>>(),
                services.GetRequiredService<RoleManager<IdentityRole>>(), Configuration["Global:AdminPassword"]).Wait();

            app.UseMvc();
        }
    }
}
