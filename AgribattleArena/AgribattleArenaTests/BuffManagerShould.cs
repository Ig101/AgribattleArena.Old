using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.ActorModel.Buffs;
using NUnit.Framework;

namespace AgribattleArenaTests
{
    [TestFixture]
    [Category("BuffManager testing")]
    class BuffManagerShould
    {
        [Test]
        [Ignore("Test for learning")]
        public void NumberOfBuffs()
        {
            //RoleModel model;
            BuffManager manager = new BuffManager(null);
            //Buff buff;
            Assert.That(manager.Buffs, Has.Exactly(3).Items);
            //Assert.That(manager.Buffs, Has.Exactly(3).Property("Name").EqualTo("damage").And.Property("Action").EqualTo("SMTH"));
            //  Assert.That(manager.Buffs, Has.Exactly(3).Matches<Buff>(
            //     item => item.Duration == 10 && item.Id == 500));
            //Assert.That(manager.Buffs, Does.Contain(buff));
            //Assert.That(null, Throws.ArgumentNullException);
        }
    }
}
