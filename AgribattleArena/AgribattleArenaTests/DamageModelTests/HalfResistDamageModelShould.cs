using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Helpers;
using NUnit.Framework;

namespace AgribattleArenaTests.DamageModelTests
{
    [TestFixture]
    public class HalfResistDamageModelShould
    {
        DamageModel sut;

        [SetUp]
        public void SetupDamageModel()
        {
            sut = new DamageModel(500, new TagSynergy[] { new TagSynergy(null, "damage", 0.5f) });
        }

        [Test]
        [TestCase(999, false, 0.5f, TestName = "NonLethalDamage")]
        [TestCase(1000,true,0, TestName = "LethalDamage")]
        [TestCase(1500,true,-250f, TestName = "OverkillDamage")]
        public void Damage(float amount, bool expectedState, float expectedHealth)
        {
            bool isDead = sut.Damage(amount, new string[] { "damage" });

            Assert.That(isDead, Is.EqualTo(expectedState), "Deal " + amount + " damage to half-resist 500 health Actor");
            Assert.That(sut.Health, Is.EqualTo(expectedHealth), "Amount of health after dealing " + amount + " damage to half-resist 500 health Actor");
        }
    }
}
