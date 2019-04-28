using System.Collections.Generic;

namespace AgribattleArena.BackendServer.Models.Profile
{
    public class ActorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActorNative { get; set; }
        public string AttackingSkillNative { get; set; }
        public List<string> Skills { get; set; }
        public int Strength { get; set; }
        public int Willpower { get; set; }
        public int Constitution { get; set; }
        public int Speed { get; set; }
        public int ActionPointsIncome { get; set; }
        public bool InParty { get; set; }
    }
}