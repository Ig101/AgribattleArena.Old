using AgribattleArena.Configurator.Models;
using AgribattleArena.Configurator.Models.Natives;
using AgribattleArena.Configurator.Services;
using AgribattleArena.DBProvider.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

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
            args = new string[] { "--update", "Documents/InitialNativesDocument.json" };
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
                cfg.CreateMap<TagSynergyDto, DBProvider.Contexts.NativeEntities.TagSynergy>();
                cfg.CreateMap<string, DBProvider.Contexts.NativeEntities.Tag>()
                    .ConstructUsing(c => new DBProvider.Contexts.NativeEntities.Tag() { Name = c });
                cfg.CreateMap<string, DBProvider.Contexts.NativeEntities.SceneAction>()
                    .ConstructUsing(c => new DBProvider.Contexts.NativeEntities.SceneAction() { Name = c });
                cfg.CreateMap<BackendActorDto, DBProvider.Contexts.NativeEntities.Actor>();
                cfg.CreateMap<BackendBuffDto, DBProvider.Contexts.NativeEntities.Buff>()
                    .AfterMap((origin, mapped) =>
                    {
                        if (origin.Mod == null) mapped.Mod = 1;
                    });
                cfg.CreateMap<BackendDecorationDto, DBProvider.Contexts.NativeEntities.Decoration>()
                    .AfterMap((origin, mapped) =>
                    {
                        if (origin.Mod == null) mapped.Mod = 1;
                    });
                cfg.CreateMap<BackendRoleModelDto, DBProvider.Contexts.NativeEntities.RoleModel>()
                    .ForMember(d => d.AttackingSkill, o => o.Ignore())
                    .ForMember(d => d.RoleModelSkills, o => o.Ignore());
                cfg.CreateMap<BackendSkillDto, DBProvider.Contexts.NativeEntities.Skill>()
                    .AfterMap((origin,mapped) =>
                    {
                        if (origin.Mod == null) mapped.Mod = 1;
                    });
                cfg.CreateMap<BackendSpecEffectDto, DBProvider.Contexts.NativeEntities.SpecEffect>()
                    .AfterMap((origin, mapped) =>
                    {
                        if (origin.Mod == null) mapped.Mod = 1;
                    });
                cfg.CreateMap<BackendTileDto, DBProvider.Contexts.NativeEntities.Tile>()
                    .AfterMap((origin, mapped) =>
                    {
                        if (origin.Mod == null) mapped.Mod = 1;
                    });
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
                Task.WaitAll(processor.Process(document).ToArray());
            }
            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
