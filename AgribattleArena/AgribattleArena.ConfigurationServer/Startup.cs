using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArena.ConfigurationServer.Services;
using AgribattleArena.DBProvider.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgribattleArena.ConfigurationServer
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
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            if (!env.IsDevelopment())
                services.Configure<MvcOptions>(o =>
                o.Filters.Add(new RequireHttpsAttribute()));

            services.AddDbContext<NativesContext>(o => o.UseMySql(Configuration["ConnectionStrings:NativesDB"]));
            services.AddDbContext<ProfilesContext>(o => o.UseMySql(Configuration["ConnectionStrings:ProfilesDB"]));
            services.AddDbContext<StoreContext>(o => o.UseMySql(Configuration["ConnectionStrings:StoreDB"]));
            services.AddIdentity<DBProvider.Contexts.ProfileEntities.Profile, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                 .AddEntityFrameworkStores<ProfilesContext>()
                 .AddDefaultTokenProviders();
            services.AddTransient<IProfilesService, ProfilesService>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<INativesRepository, NativesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<string, DBProvider.Contexts.StoreEntities.Skill>()
                    .ConstructUsing(c => new DBProvider.Contexts.StoreEntities.Skill() { Native = c });
                cfg.CreateMap<Models.ActorToAddDto, DBProvider.Contexts.StoreEntities.Actor>();
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();

            CreateAdminUserIsNotExists(services.GetRequiredService<UserManager<DBProvider.Contexts.ProfileEntities.Profile>>(),
                services.GetRequiredService<RoleManager<IdentityRole>>(), Configuration["Global:AdminPassword"]).Wait();

            app.UseMvc();
        }
    }
}
