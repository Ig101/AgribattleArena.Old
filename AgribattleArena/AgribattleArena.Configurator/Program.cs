using AgribattleArena.Configurator.Models;
using AgribattleArena.Configurator.Services;
using AgribattleArena.DBProvider.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AgribattleArena.Configurator
{
    class Program
    {
        static DbContextOptionsBuilder<NativesContext> _nativesOptions;
        static DbContextOptionsBuilder<ProfilesContext> _profilesOptions;
        static DbContextOptionsBuilder<StoreContext> _storeOptions;

        static void Main(string[] args)
        { 
            //Temporary parameter for tests
            args = new string[] { "--update", "Documents/InitialRevelationLevelDocument.json" };
            //
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.Development.json", false, true);
#else
                .AddJsonFile("appsettings.json", false, true);
#endif
            IConfiguration configuration = builder.Build();
            _nativesOptions = new DbContextOptionsBuilder<NativesContext>().UseMySql(configuration["ConnectionStrings:NativesDB"]);
            _profilesOptions = new DbContextOptionsBuilder<ProfilesContext>().UseMySql(configuration["ConnectionStrings:ProfilesDB"]);
            _storeOptions = new DbContextOptionsBuilder<StoreContext>().UseMySql(configuration["ConnectionStrings:StoreDB"]);

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<string, DBProvider.Contexts.StoreEntities.Skill>()
                    .ConstructUsing(c => new DBProvider.Contexts.StoreEntities.Skill() { Native = c });
                cfg.CreateMap<StoreActorDto, DBProvider.Contexts.StoreEntities.Actor>();
            });

            if (args.Length>1 && args[0] == "--update" && File.Exists(args[1]))
            {
                ChangingDocumentDto document;
                using (StreamReader file = File.OpenText(args[1]))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    document = (ChangingDocumentDto)serializer.Deserialize(file,typeof(ChangingDocumentDto));
                }
                ChangingDocumentProcessor processor = new ChangingDocumentProcessor(
                    new ProfilesRepository(new ProfilesContext(_profilesOptions.Options)),
                    new NativesRepository(new NativesContext(_nativesOptions.Options)),
                    new StoreRepository(new StoreContext(_storeOptions.Options))
                    );
                processor.Process(document).Wait();
            }
            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
