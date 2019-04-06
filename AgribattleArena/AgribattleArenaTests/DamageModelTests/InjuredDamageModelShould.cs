using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Helpers;
using NUnit.Framework;

namespace AgribattleArenaTests.DamageModelTests
{
    [TestFixture]
    public class InjuredDamageModelShould
    {
        DamageModel sut;

        [SetUp]
        public void SetupInjuredDamageModel()
        {
            sut = new DamageModel(500, null)
            {
                Health = 250
            };
        }

        [Test]
        public void GetHealth()
        {
            Assert.AreEqual(sut.Health, 250, "Model has 250 health");
            Assert.AreEqual(sut.MaxHealth, 500, "Model has 500 max health");
        }

        [Test]
        [TestCase(100, false, 350f, TestName = "Heal")]
        [TestCase(1000, false, 500f, TestName = "Overheal")]
        public void HealToInjured(float amount, bool expectedState, float expectedHealth)
        {
            bool isDead = sut.Damage(-amount, null);

            Assert.That(isDead, Is.EqualTo(expectedState), "Heal 250 health Actor by " + amount);
            Assert.That(sut.Health, Is.EqualTo(expectedHealth), "Amount of health after healing 500 health Actor by " + amount);
        }
    }
}
