import { ISpecEffectNative } from 'src/app/share/models/natives';
import { BattleScene } from '../battle-scene';
import { ISyncSpecEffect } from 'src/app/share/models/synchronization';
import { INativesStore } from 'src/app/share/models/natives-store.model';

export class BattleSpecEffect {
    id: number;
    ownerId?: string;
    isAlive: boolean;
    x: number;
    y: number;
    z: number;
    duration?: number;
    mod: number;
    native: ISpecEffectNative;

    synchronize(sync: ISyncSpecEffect, natives: INativesStore) {

    }

    constructor(sync: ISyncSpecEffect, natives: INativesStore, public parent: BattleScene) {
        this.synchronize(sync, natives);
    }
}
