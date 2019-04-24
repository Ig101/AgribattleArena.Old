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

        int PrepareForMoveHeight (bool down)
        {
            _actor.ActionPoints = 6;
            _scene.ActorMove(_actor.Id, 17, 2);
            if (down)
            {
                _scene.ActorMove(_actor.Id, 16, 2);
                _scene.ActorMove(_actor.Id, 15, 2);
            }
            _syncMessages.Clear();
            return down ? 15 : 17;
        }

        [Test]
        [TestCase(true, false, TestName = "MoveToPoint(0=>9=>18=>27=>36 height)")]
        [TestCase(true, true, TestName = "MoveToPoint(0=>-9=>-18=>-27=>-36 height)")]
        [TestCase(false, false, TestName = "MoveToPoint(0=>36 height)")]
        [TestCase(false, true, TestName = "MoveToPoint(0=>-36 height)")]
        public void MoveHeight(bool availability, bool down)
        {
            _actor.ActionPoints = 6;
            _scene.ActorMove(_actor.Id, 17, 2);
            if (down)
            {
                _scene.ActorMove(_actor.Id, 16, 2);
                _scene.ActorMove(_actor.Id, 15, 2);
            }
            _syncMessages.Clear();
            int tempActorPositionX = down ? 15 : 17;

            if (availability)
            {
                Assert.That(_actor.TempTile.X, Is.EqualTo(tempActorPositionX), "Move targetX start position");
                Assert.That(_actor.TempTile.Y, Is.EqualTo(2), "Move targetY start position");
                _actor.ActionPoints = 2;
                Assert.That(_scene.ActorMove(_actor.Id, tempActorPositionX, 3), Is.True, "1 step availability");
                Assert.That(_actor.TempTile.X, Is.EqualTo(tempActorPositionX), "Move targetX 1 step");
                Assert.That(_actor.TempTile.Y, Is.EqualTo(3), "Move targetY 1 step");
                Assert.That(Math.Abs(_actor.TempTile.Height), Is.EqualTo(9));

                _actor.ActionPoints = 2;
                Assert.That(_scene.ActorMove(_actor.Id, tempActorPositionX, 4), Is.True, "1 step availability");
                Assert.That(_actor.TempTile.X, Is.EqualTo(tempActorPositionX), "Move targetX 2 step");
                Assert.That(_actor.TempTile.Y, Is.EqualTo(4), "Move targetY 2 step");
                Assert.That(Math.Abs(_actor.TempTile.Height), Is.EqualTo(18));

                _actor.ActionPoints = 2;
                Assert.That(_scene.ActorMove(_actor.Id, tempActorPositionX - 1, 4), Is.True, "1 step availability");
                Assert.That(_actor.TempTile.X, Is.EqualTo(tempActorPositionX - 1), "Move targetX 3 step");
                Assert.That(_actor.TempTile.Y, Is.EqualTo(4), "Move targetY 3 step");
                Assert.That(Math.Abs(_actor.TempTile.Height), Is.EqualTo(27));
            }
            else
            {
                _scene.ActorMove(_actor.Id, tempActorPositionX - 1, 2);
                Assert.That(_actor.TempTile.X, Is.EqualTo(tempActorPositionX - 1), "Move targetX start position");
                Assert.That(_actor.TempTile.Y, Is.EqualTo(2), "Move targetY start position");
            }

            _actor.ActionPoints = 2;
            Assert.That(_scene.ActorMove(_actor.Id, tempActorPositionX-1, 3), Is.EqualTo(availability), "last step availability");
            Assert.That(_actor.TempTile.X, Is.EqualTo(tempActorPositionX - 1), "Move targetX last step");
            Assert.That(_actor.TempTile.Y, Is.EqualTo(availability?3:2), "Move targetY last step");
            Assert.That(Math.Abs(_actor.TempTile.Height), Is.EqualTo(availability?36:0));
        }
    }
}
