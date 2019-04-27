using AgribattleArena.Engine;
using AgribattleArena.Engine.ForExternalUse;
using AgribattleArena.Engine.ForExternalUse.EngineHelper;
using AgribattleArena.Engine.ForExternalUse.Generation.ObjectInterfaces;
using AgribattleArena.Engine.ForExternalUse.Synchronization;
using AgribattleArena.Engine.Helpers;
using AgribattleArena.Tests.Engine.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgribattleArena.Tests.Engine
{
    [TestFixture]
    public class SceneShould: BasicEngineTester
    {
        [SetUp]
        public void Prepare ()
        {
            _syncMessages = new List<ISyncEventArgs>();
        }

        [Test]
        public void CreateSimpleScene ()
        {
            _scene = SceneSamples.CreateSimpleScene(this.EventHandler, false);

            Assert.That(_scene, Is.Not.Null, "Check scene object existence");
            Assert.That(_syncMessages.Count, Is.EqualTo(2), "Check messages count");
            Assert.That(_syncMessages[0].Action, Is.EqualTo(AgribattleArena.Engine.Helpers.Action.StartGame), "Check StartGame message action");
            Assert.That(_syncMessages[0].Version, Is.EqualTo(0), "Check version of StartGame message");
            Assert.That(_syncMessages[0].SyncInfo.TempActor, Is.Null, "Check tempActor in StartGame message");
            Assert.That(_syncMessages[0].SyncInfo.Players.Count(), Is.EqualTo(2), "Check players count in StartGame message");
            Assert.That(_syncMessages[0].SyncInfo.ChangedActors.Count(), Is.EqualTo(2), "Check changedActors count in StartGame message");
            Assert.That(_syncMessages[0].SyncInfo.ChangedDecorations.Count(), Is.EqualTo(0), "Check changedDecorations count in StartGame message");
            Assert.That(_syncMessages[0].SyncInfo.ChangedEffects.Count(), Is.EqualTo(0), "Check changedEffects count in StartGame message");
            Assert.That(_syncMessages[0].SyncInfo.ChangedTiles.Count(), Is.EqualTo(400), "Check changedTiles count in StartGame message");
            Assert.That(_syncMessages[0].SyncInfo.DeletedActors.ToList().Count, Is.EqualTo(0), "Check deletedActors count in StartGame message");
            Assert.That(_syncMessages[1].Action, Is.EqualTo(AgribattleArena.Engine.Helpers.Action.EndTurn), "Check EndTurn message action");
            Assert.That(_syncMessages[1].Version, Is.EqualTo(1), "Check version of EndTurn message");
            Assert.That(_syncMessages[1].SyncInfo.TempActor, Is.Not.Null, "Check tempActor in EndTurn message");
            Assert.That(_syncMessages[1].SyncInfo.ChangedActors.Count(), Is.EqualTo(0), "Check changedActors count in EndTurn message");
            Assert.That(_syncMessages[1].SyncInfo.ChangedTiles.Count(), Is.EqualTo(0), "Check changedTiles count in EndTurn message");
            Assert.That(_syncMessages[1].SyncInfo.DeletedActors.Count(), Is.EqualTo(0), "Check deletedActors count in EndTurn message");
        }
    }
}
