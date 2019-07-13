import { IBuffNative } from 'src/app/share/models/natives';
import { BattleActor } from './battle-actor';
import { ISyncBuff } from 'src/app/share/models/synchronization';
import { INativesStoreMapped } from 'src/app/share/models/natives-store-mapped.model';

export class BattleBuff {
    id: number;
    native: IBuffNative;
    mod: number;
    duration?: number;

    synchronize(sync: ISyncBuff, natives: INativesStoreMapped) {
        if (!this.native || this.native.id !== sync.nativeId) {
            this.native = natives.buffs.find(x => x.id === sync.nativeId);
        }
        this.mod = sync.mod;
        this.duration = sync.duration;
    }

    constructor(sync: ISyncBuff, natives: INativesStoreMapped, public parent: BattleActor) {
        this.id = sync.id;
        this.synchronize(sync, natives);
    }

    update(milliseconds: number) {

    }
}
