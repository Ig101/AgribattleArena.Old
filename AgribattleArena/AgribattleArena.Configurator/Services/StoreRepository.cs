using AgribattleArena.Configurator.Models;
using AgribattleArena.DBProvider.Contexts;
using AgribattleArena.DBProvider.Contexts.StoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgribattleArena.Configurator.Services
{
    class StoreRepository: IRepository<StoreActorDto>
    {
        StoreContext _context;

        public StoreRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Response> Add(StoreActorDto actor)
        {
            if (_context.Actor.Where(x => x.Name == actor.Name).Count() == 0)
            {
                if (actor.ActionPointsIncome != null &&
                    actor.ActorNative != null &&
                    actor.AttackingSkillNative != null &&
                    actor.Constitution != null &&
                    actor.Cost != null &&
                    actor.Name != null &&
                    actor.Speed != null &&
                    actor.Strength != null &&
                    actor.Willpower != null &&
                    actor.Skills != null
                    )
                {
                    await _context.Actor.AddAsync(AutoMapper.Mapper.Map<Actor>(actor));
                    await _context.SaveChangesAsync();
                    return Response.Success;
                }
                else
                {
                    return Response.Error;
                }
            }
            return Response.NoChanges;
        }

        public async Task<Response> Update(StoreActorDto actor)
        {
            IEnumerable<Actor> actorsToChange;
            if ((actorsToChange = _context.Actor.Where(x => x.Name == actor.Name)).Count() > 0)
            {
                var actorToChange = actorsToChange.First();
                if (actor.ActionPointsIncome != null) actorToChange.ActionPointsIncome = actor.ActionPointsIncome.Value;
                if (actor.ActorNative != null) actorToChange.ActorNative = actor.ActorNative;
                if (actor.AttackingSkillNative != null) actorToChange.AttackingSkillNative = actor.AttackingSkillNative;
                if (actor.Constitution != null) actorToChange.Constitution = actor.Constitution.Value;
                if (actor.Willpower != null) actorToChange.Willpower = actor.Willpower.Value;
                if (actor.Strength != null) actorToChange.Strength = actor.Strength.Value;
                if (actor.Speed != null) actorToChange.Speed = actor.Speed.Value;
                if (actor.Cost != null) actorToChange.Cost = actor.Cost.Value;
                if (actor.Skills != null)
                {
                    actorToChange.Skills = AutoMapper.Mapper.Map<List<Skill>>(actor.Skills);
                }
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }

        public async Task<Response> Delete(StoreActorDto actor)
        {
            if (actor == null) return Response.Error;
            IEnumerable<Actor> actorsToDelete;
            if ((actorsToDelete = _context.Actor.Where(x => x.Name == actor.Name)).Count() > 0)
            {
                _context.Actor.RemoveRange(actorsToDelete);
                await _context.SaveChangesAsync();
                return Response.Success;
            }
            return Response.NoChanges;
        }
    }
}
