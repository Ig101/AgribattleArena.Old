import { ISyncActor, ISyncDecoration, ISyncPlayer, ISyncSpecEffect, ISyncTile } from './synchronization';

export interface ISynchronizer {
    version: number;
    actorId?: number;
    skillActionId?: number;
    targetX?: number;
    targetY?: number;
    tilesetWidth: number;
    tilesetHeight: number;
    turnTime: number;
    tempActor?: ISyncActor;
    tempDecoration?: ISyncDecoration;
    players: ISyncPlayer[];
    changedActors: ISyncActor[];
    changedDecorations: ISyncDecoration[];
    changedEffects: ISyncSpecEffect[];
    deletedActors: ISyncActor[];
    deletedDecorations: ISyncDecoration[];
    deletedEffects: ISyncSpecEffect[];
    changedTiles: ISyncTile[];
}
