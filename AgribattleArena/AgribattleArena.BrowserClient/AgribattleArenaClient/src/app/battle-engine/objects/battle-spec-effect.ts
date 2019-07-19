import { ISpecEffectNative } from 'src/app/share/models/natives/mapped';
import { BattleScene } from '../battle-scene';
import { ISyncSpecEffect } from 'src/app/share/models/synchronization';
import { BattlePlayer } from './battle-player';
import { INativesStoreMapped } from 'src/app/share/models/natives-store-mapped.model';
import { Sprite } from '../sprite';
import { BattleVisualObject } from './battle-visual-object';

export class BattleSpecEffect extends BattleVisualObject {

    id: number;
    owner?: BattlePlayer;
    isAlive: boolean;
    x: number;
    y: number;
    z: number;
    duration?: number;
    mod: number;
    native: ISpecEffectNative;

    synchronize(sync: ISyncSpecEffect, natives: INativesStoreMapped) {
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

    constructor(sync: ISyncSpecEffect, natives: INativesStoreMapped, public parent: BattleScene) {
        super();
        this.id = sync.id;
        this.synchronize(sync, natives);
    }

    update(milliseconds: number) {
        super.update(milliseconds);
    }
}
