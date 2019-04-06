using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Helpers;
using NUnit.Framework;

namespace AgribattleArenaTests.DamageModelTests
{
    [TestFixture]
    public class ImmuneDamageModelShould
    {
        DamageModel sut;

        [SetUp]
        public void SetupImmuneDamageModel()
        {
            sut = new DamageModel(500, new TagSynergy[] { new TagSynergy(null, "damage", 0) })
            {
                Health = 250f
            };
        }

        [Test]
        [TestCase(100000, false, TestName = "ResistedDamage")]
        [TestCase(-100000, false, TestName = "ResistedHeal")]
        public void Damage(float amount, bool expectedState)
        {
            bool isDead = sut.Damage(amount, new string[] { "damage" });

            Assert.That(isDead, Is.EqualTo(expectedState), "Deal " + amount + " damage to immune 250/500 health Actor");
            Assert.That(sut.Health, Is.EqualTo(250), "Amount of health after dealing " + amount + " damage to immune 250/500 health Actor");
        }
    }
}
