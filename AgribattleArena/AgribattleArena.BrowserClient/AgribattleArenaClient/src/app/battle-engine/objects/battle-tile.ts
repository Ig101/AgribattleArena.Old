import { ITileNative } from 'src/app/share/models/natives';
import { BattleScene } from '../battle-scene';
import { ISyncTile } from 'src/app/share/models/synchronization';
import { INativesStore } from 'src/app/share/models/natives-store.model';

export class BattleTile {
    x: number;
    y: number;
    tempActorId?: number;
    height: number;
    native: ITileNative;
    ownerId?: string;

    synchronize(sync: ISyncTile, natives: INativesStore) {

    }

    constructor(sync: ISyncTile, natives: INativesStore, public parent: BattleScene) {
        this.synchronize(sync, natives);
    }
}
