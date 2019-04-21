using AgribattleArena.Engine;
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.ForExternalUse.Synchronization;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Engine.Objects;
using AgribattleArena.Tests.Engine.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Tests.Engine.TempTileObjectShould
{
    [TestFixture]
    public class TempTileObjectMovingShould: BasicEngineTester
    {
        Actor _actor;

        [SetUp]
        public void Prepare ()
        {
            _syncMessages = new List<ISyncEventArgs>();
            _scene = SceneSamples.CreateSimpleScene(this.EventHandler);
            _syncMessages.Clear();
            _actor = (Actor)_scene.TempTileObject;
        }

        [Test]
        public void TempTileObjectStartState ()
        {
            Assert.That(_actor.ExternalId, Is.EqualTo(2), "TempTileObject externalId");
            Assert.That(_actor.TempTile.X, Is.EqualTo(17), "X position");
            Assert.That(_actor.TempTile.Y, Is.EqualTo(2), "Y position");
            Assert.That(_actor.ActionPoints, Is.EqualTo(4), "Amount of actionPoints");
        }

        [Test]
        public void MoveToNearestPoint ()
        {
            Assert.That(_scene.ActorMove(_actor.Id, 18, 2), Is.True, "Actor can move");
            Assert.That(_syncMessages.Count, Is.EqualTo(1), "SyncMessages count");
            Assert.That(_syncMessages[0].Action, Is.EqualTo(AgribattleArena.Engine.Helpers.Action.Move), "Move action");
            Assert.That(_syncMessages[0].TargetX + " " + _syncMessages[0].TargetY, Is.EqualTo("18 2"), "Move targetX and targetY");
            Assert.That(_actor.TempTile.X, Is.EqualTo(18), "X position of Actor2");
            Assert.That(_actor.TempTile.Y, Is.EqualTo(2), "Y position of Actor2");
            Assert.That(_actor.ActionPoints, Is.EqualTo(3), "Amount of actionPoints");
        }
    }
}
