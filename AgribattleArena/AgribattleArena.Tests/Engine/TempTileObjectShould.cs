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


        void EndTurnAssertion()
        {
            Assert.That(_syncMessages[_syncMessages.Count-1].Action, Is.EqualTo(AgribattleArena.Engine.Helpers.Action.EndTurn), "EndTurn message action");
            Assert.That(_scene.TempTileObject, Is.Not.EqualTo(_actor), "Another actor is acting");
            Assert.That(_actor.InitiativePosition, Is.GreaterThan(0), "Initiative of _actor");
            Assert.That(_scene.TempTileObject.InitiativePosition, Is.EqualTo(0), "Initiative of tempTileObject");
        }

        void MoveCloseToEnemy()
        {
            int step = 0;
            do
            {
                step++;
                _actor.ActionPoints = 4;
                Assert.That(_scene.ActorMove(_actor.Id, _actor.X - 1, 2), Is.True, "Move step " + step);
            } while (_actor.X > 2);
            Assert.That(_actor.X, Is.EqualTo(2), "Close to enemy X");
            Assert.That(_actor.Y, Is.EqualTo(2), "Close to enemy Y");
        }

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
            Assert.That(_actor.InitiativePosition, Is.EqualTo(0), "Initiative position");
        }

        [Test]
        [TestCase(2, false, false, TestName = "SpendActionPoints(2 points, 4 temp)")]
        [TestCase(4, false, true, TestName = "SpendActionPoints(4 points, 4 temp)")]
        [TestCase(6, true, false, TestName = "SpendActionPoints(6 points, 4 temp)")]
        public void SpendActionPoints(int points, bool exception, bool endTurn)
        {
            try
            {
                _actor.SpendActionPoints(points);
                Assert.That(_actor.CheckActionAvailability(), Is.EqualTo(!endTurn), "End turn condition");
                Assert.That(_actor.ActionPoints, Is.EqualTo(4 - points), "Amount of action points after spending");
            }
            catch (ArgumentException e)
            {
                if (exception)
                {
                    Assert.That(e.ParamName, Is.EqualTo("amount"), "Not enough action points condition");
                    Assert.That(_actor.ActionPoints, Is.EqualTo(4), "Amount of action points after exception");
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        #region EndTurn

        #endregion

        #region Moving
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
                Assert.That(_actor.X, Is.EqualTo(tempActorPositionX), "Move targetX start position");
                Assert.That(_actor.Y, Is.EqualTo(2), "Move targetY start position");
                _actor.ActionPoints = 2;
                Assert.That(_scene.ActorMove(_actor.Id, tempActorPositionX, 3), Is.True, "1 step availability");
                Assert.That(_actor.X, Is.EqualTo(tempActorPositionX), "Move targetX 1 step");
                Assert.That(_actor.Y, Is.EqualTo(3), "Move targetY 1 step");
                Assert.That(Math.Abs(_actor.TempTile.Height), Is.EqualTo(9));

                _actor.ActionPoints = 2;
                Assert.That(_scene.ActorMove(_actor.Id, tempActorPositionX, 4), Is.True, "1 step availability");
                Assert.That(_actor.X, Is.EqualTo(tempActorPositionX), "Move targetX 2 step");
                Assert.That(_actor.Y, Is.EqualTo(4), "Move targetY 2 step");
                Assert.That(Math.Abs(_actor.TempTile.Height), Is.EqualTo(18));

                _actor.ActionPoints = 2;
                Assert.That(_scene.ActorMove(_actor.Id, tempActorPositionX - 1, 4), Is.True, "1 step availability");
                Assert.That(_actor.X, Is.EqualTo(tempActorPositionX - 1), "Move targetX 3 step");
                Assert.That(_actor.Y, Is.EqualTo(4), "Move targetY 3 step");
                Assert.That(Math.Abs(_actor.TempTile.Height), Is.EqualTo(27));
            }
            else
            {
                _scene.ActorMove(_actor.Id, tempActorPositionX - 1, 2);
                Assert.That(_actor.X, Is.EqualTo(tempActorPositionX - 1), "Move targetX start position");
                Assert.That(_actor.Y, Is.EqualTo(2), "Move targetY start position");
            }

            _actor.ActionPoints = 2;
            Assert.That(_scene.ActorMove(_actor.Id, tempActorPositionX-1, 3), Is.EqualTo(availability), "last step availability");
            Assert.That(_actor.X, Is.EqualTo(tempActorPositionX - 1), "Move targetX last step");
            Assert.That(_actor.Y, Is.EqualTo(availability?3:2), "Move targetY last step");
            Assert.That(Math.Abs(_actor.TempTile.Height), Is.EqualTo(availability?36:0));
        }

        [Test]
        [TestCase(TestName = "MoveToPoint(TileObject)")]
        public void MoveToTileObject()
        {
            MoveCloseToEnemy();
            Assert.That(_scene.ActorMove(_actor.Id, _actor.X - 1, 2), Is.False, "Move to TileObject");
        }

        [Test]
        [TestCase(TestName ="MoveToPoint(Move until turn ends)")]
        public void EndTurnByMove()
        {
            for (int i = 0; i < 4; i++)
            {
                Assert.That(_scene.ActorMove(_actor.Id, _actor.X - 1, _actor.Y), Is.True, "Step " + i);
            }
            Assert.That(_actor.ActionPoints, Is.EqualTo(0), "Action points after spending");
            Assert.That(_syncMessages.Count, Is.EqualTo(5), "SyncMessages count after action points spending");
            EndTurnAssertion();
        }
        #endregion

        #region Attacking

        #endregion

        #region Casting

        #endregion

        #region Waiting
        [Test]
        [TestCase(2, TestName = "Wait(2 points)")]
        [TestCase(0, TestName = "Wait(4 points)")]
        [TestCase(-2, TestName = "Wait(6 points)")]
        public void Wait(int points)
        {
            _actor.SpendActionPoints(points);
            _scene.ActorWait(_actor.Id);
            Assert.That(_actor.ActionPoints, Is.EqualTo(4 - points), "Amount of action points after Wait");
            Assert.That(_syncMessages[0].Action, Is.EqualTo(AgribattleArena.Engine.Helpers.Action.Wait), "Action of Wait message");
            EndTurnAssertion();
        }
        #endregion
    }
}
