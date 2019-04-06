using AgribattleArenaBackendServer.Engine.ActorModel;
using AgribattleArenaBackendServer.Engine.Helpers;
using NUnit.Framework;

namespace AgribattleArenaTests.DamageModelTests
{
    [TestFixture]
    public class DamageModelShould
    {
        DamageModel sut;

        [SetUp]
        public void SetupDamageModel()
        {
            sut = new DamageModel(500, null);
        }

        [Test]
        public void GetHealth()
        {
            Assert.AreEqual(sut.Health, 500, "Model has 500 health");
            Assert.AreEqual(sut.MaxHealth, 500, "Model has 500 max health");
        }

        [Test]
        [TestCase(499, false, 1f, TestName = "NonLethalDamage")]
        [TestCase(500, true, 0, TestName = "LethalDamage")]
        [TestCase(750, true, -250f, TestName = "OverkillDamage")]
        public void Damage(float amount, bool expectedState, float expectedHealth)
        {
            bool isDead = sut.Damage(amount, null);

            Assert.That(isDead, Is.EqualTo(expectedState), "Deal " + amount + " damage to 500 health Actor");
            Assert.That(sut.Health, Is.EqualTo(expectedHealth), "Amount of health after dealing " + amount + " damage to 500 health Actor");
        }
    }
}
