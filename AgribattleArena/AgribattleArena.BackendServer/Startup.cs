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

        async Task CreateAdminUserIsNotExists(UserManager<Contexts.ProfileEntities.Profile> userManager, RoleManager<IdentityRole> roleManager, 
            string adminPassword)
        {
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                var result = await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            var user = userManager.FindByNameAsync("admin").Result;
            if(user == null)
            {
                user = new Contexts.ProfileEntities.Profile()
                {
                    UserName = "admin",
                    Email = "admin@noemail.com",
                    Resources = 0,
                    DonationResources = 0,
                    Revelations = 0
                };
                var result = await userManager.CreateAsync(user,adminPassword);
            }
            if(!await userManager.IsInRoleAsync(user, "admin"))
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
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

            CreateAdminUserIsNotExists(services.GetRequiredService<UserManager<Contexts.ProfileEntities.Profile>>(),
                services.GetRequiredService<RoleManager<IdentityRole>>(), Configuration["Global:AdminPassword"]).Wait();
        }
    }
}
