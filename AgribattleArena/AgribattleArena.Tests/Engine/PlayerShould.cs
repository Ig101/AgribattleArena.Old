using AgribattleArena.Engine.ForExternalUse.Synchronization;
using AgribattleArena.Engine.Objects;
using AgribattleArena.Tests.Engine.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Tests.Engine
{
    [TestFixture]
    class PlayerShould: BasicEngineTester
    {
        [SetUp]
        public void Prepare()
        {
            _syncMessages = new List<ISyncEventArgs>();
            _scene = SceneSamples.CreateSimpleScene(this.EventHandler, true);
            _scene.Actors.Find(x => x.ExternalId == 1).ChangePosition(_scene.Tiles[17][2], true);
            _syncMessages.Clear();
        }

        [Test]
        public void StartState()
        {
            Assert.That(_scene.Tiles[1][2].TempObject, Is.Null, "Previous actor position");
            Assert.That(((Actor)_scene.Tiles[17][2].TempObject).ExternalId, Is.EqualTo(1), "Previous actor position");
        }

        [Test]
        [TestCase(false, TestName = "PlayerVictory(Second)")]
        [TestCase(true, TestName = "PlayerVictory(First)")]
        public void PlayerVictory(bool first)
        {
            if (first) _scene.ActorWait(_scene.TempTileObject.Id);
            _syncMessages.Clear();
            int tileX = first ? 18 : 17;
            Actor deadMan = (Actor)_scene.Tiles[tileX][2].TempObject;
            _scene.ActorAttack(_scene.TempTileObject.Id, tileX, 2);
            _scene.ActorAttack(_scene.TempTileObject.Id, tileX, 2);
            Assert.That(!deadMan.IsAlive, Is.True, "Actor killed");
            Assert.That(_syncMessages.Count, Is.EqualTo(3), "Count of syncMessages");
            Assert.That(_syncMessages[2].Action, Is.EqualTo(AgribattleArena.Engine.Helpers.Action.EndGame));
            Assert.That(_scene.Players.ToArray()[first ? 0 : 1].Status, Is.EqualTo(AgribattleArena.Engine.Helpers.PlayerStatus.Victorious));
            Assert.That(_scene.Players.ToArray()[first ? 1 : 0].Status, Is.EqualTo(AgribattleArena.Engine.Helpers.PlayerStatus.Defeated));
        }

        [Test]
        [TestCase(4, TestName = "SelfKillDefeat(Not end turn)")]
        [TestCase(1, TestName = "SelfKillDefeat(End turn)")]
        public void SelfKill(int actionPoints)
        {
            Actor actor = (Actor)_scene.TempTileObject;
            _scene.ActorAttack(actor.Id, 18, 2);
            Assert.That(actor.IsAlive, Is.False, "Actor is dead");
            Assert.That(_syncMessages.Count, Is.EqualTo(2), "SyncMessages count");
            Assert.That(_syncMessages[0].Action, Is.EqualTo(AgribattleArena.Engine.Helpers.Action.Attack));
            Assert.That(_syncMessages[1].Action, Is.EqualTo(AgribattleArena.Engine.Helpers.Action.EndGame));
            Assert.That(_scene.Players.ToArray()[0].Status, Is.EqualTo(AgribattleArena.Engine.Helpers.PlayerStatus.Victorious));
            Assert.That(_scene.Players.ToArray()[1].Status, Is.EqualTo(AgribattleArena.Engine.Helpers.PlayerStatus.Defeated));
        }
    }
}
