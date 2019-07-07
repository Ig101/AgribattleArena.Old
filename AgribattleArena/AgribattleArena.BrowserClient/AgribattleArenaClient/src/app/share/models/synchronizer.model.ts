import { IBattleActor, IBattleDecoration, IBattlePlayer, IBattleSpecEffect, IBattleTile } from './synchronization/battle-objects';

export interface ISynchronizer {
    version: number;
    actorId?: number;
    skillActionId?: number;
    targetX?: number;
    targetY?: number;
    tilesetWidth: number;
    tilesetHeight: number;
    turnTime: number;
    tempActor?: IBattleActor;
    tempDecoration?: IBattleDecoration;
    players: IBattlePlayer[];
    changedActors: IBattleActor[];
    changedDecorations: IBattleDecoration[];
    changedEffects: IBattleSpecEffect[];
    deletedActors: IBattleActor[];
    deletedDecoration: IBattleDecoration[];
    deletedEffects: IBattleSpecEffect[];
    changedTiles: IBattleTile[];
}
