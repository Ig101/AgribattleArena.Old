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

namespace AgribattleArena.Tests.Engine
{
    [TestFixture]
    public class TempTileObjectShould: BasicEngineTester
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
        public void StartState ()
        {
            Assert.That(_actor.ExternalId, Is.EqualTo(2), "TempTileObject externalId");
            Assert.That(_actor.TempTile.X, Is.EqualTo(18), "X position");
            Assert.That(_actor.TempTile.Y, Is.EqualTo(2), "Y position");
            Assert.That(_actor.ActionPoints, Is.EqualTo(4), "Amount of actionPoints");
        }

        [Test]
        [TestCase(17,2, true, TestName = "MoveToPoint(One left)")]
        [TestCase(18,3, true, TestName = "MoveToPoint(One down)")]
        [TestCase(17,3, false, TestName = "MoveToPoint(One left and down)")]
        [TestCase(16,2, false, TestName = "MoveToPoint(Two left)")]
        [TestCase(18,4, false, TestName = "MoveToPoint(Two down)")]
        [TestCase(19,2, false, TestName = "MoveToPoint(One to the wall)")]
        public void MoveToPoint (int targetX, int targetY, bool available)
        {
            int tempX = _actor.TempTile.X;
            int tempY = _actor.TempTile.Y;
            Assert.That(_scene.ActorMove(_actor.Id, targetX, targetY), Is.EqualTo(available), "Actor can move");
            Assert.That(_syncMessages.Count, Is.EqualTo(available?1:0), "SyncMessages count");
            if (available)
            {
                Assert.That(_syncMessages[0].Action, Is.EqualTo(AgribattleArena.Engine.Helpers.Action.Move), "Move action");
                Assert.That(_syncMessages[0].TargetX, Is.EqualTo(targetX), "Move targetX");
                Assert.That(_syncMessages[0].TargetY, Is.EqualTo(targetY), "Move targetY");
            }
            Assert.That(_actor.TempTile.X, Is.EqualTo(available?targetX:tempX), "X position of Actor2");
            Assert.That(_actor.TempTile.Y, Is.EqualTo(available?targetY:tempY), "Y position of Actor2");
            Assert.That(_actor.ActionPoints, Is.EqualTo(available?3:4), "Amount of actionPoints");
        }
    }
}
