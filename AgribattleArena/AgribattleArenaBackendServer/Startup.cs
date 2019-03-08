using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgribattleArenaBackendServer.Contexts;
using AgribattleArenaBackendServer.Engine.Generator;
using AgribattleArenaBackendServer.Engine.NativeManager;
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

            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
