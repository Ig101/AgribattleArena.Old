import { IBuffNative } from 'src/app/share/models/natives';
import { BattleActor } from './battle-actor';
import { ISyncBuff } from 'src/app/share/models/synchronization';
import { INativesStore } from 'src/app/share/models/natives-store.model';
import { BattleScene } from '../battle-scene';

export class BattleBuff {
    id: number;
    native: IBuffNative;
    mod: number;
    duration?: number;

    synchronize(sync: ISyncBuff, natives: INativesStore) {

    }

    constructor(sync: ISyncBuff, natives: INativesStore, public parent: BattleScene) {
        this.synchronize(sync, natives);
    }
}
