import { ISpecEffectNative } from 'src/app/share/models/natives';
import { BattleScene } from '../battle-scene';
import { ISyncSpecEffect } from 'src/app/share/models/synchronization';
import { INativesStore } from 'src/app/share/models/natives-store.model';
import { BattlePlayer } from './battle-player';

export class BattleSpecEffect {
    id: number;
    owner?: BattlePlayer;
    isAlive: boolean;
    x: number;
    y: number;
    z: number;
    duration?: number;
    mod: number;
    native: ISpecEffectNative;

    synchronize(sync: ISyncSpecEffect, natives: INativesStore) {
        if (!this.native || this.native.id !== sync.nativeId) {
            this.native = natives.specEffects.find(x => x.id === sync.nativeId);
        }
        if (!sync.ownerId && this.owner) {
            this.owner = null;
        } else if ((!this.owner && sync.ownerId) || (this.owner && this.owner.id !== sync.ownerId)) {
            this.owner = this.parent.players.find(x => x.id === sync.ownerId);
        }
        this.isAlive = sync.isAlive;
        this.x = sync.x;
        this.y = sync.y;
        this.z = sync.z;
        this.duration = sync.duration;
        this.mod = sync.mod;
    }

    constructor(sync: ISyncSpecEffect, natives: INativesStore, public parent: BattleScene) {
        this.id = sync.id;
        this.synchronize(sync, natives);
    }
}
