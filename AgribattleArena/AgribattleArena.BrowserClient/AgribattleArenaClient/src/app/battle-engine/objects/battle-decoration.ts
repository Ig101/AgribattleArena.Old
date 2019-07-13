import { ITagSynergy } from '../../share/models/tag-synergy.model';
import { IDecorationNative } from 'src/app/share/models/natives';
import { BattleScene } from '../battle-scene';
import { ISyncDecoration } from 'src/app/share/models/synchronization';
import { INativesStore } from 'src/app/share/models/natives-store.model';
import { BattleTile } from './battle-tile';

export class BattleDecoration {
        id: number;
        native: IDecorationNative;
        mod: number;
        initiativePosition: number;
        health: number;
        ownerId?: string;
        isAlive: boolean;
        x: number;
        y: number;
        tempTile: BattleTile;
        z: number;
        maxHealth: number;
        armor: ITagSynergy[];

        synchronize(sync: ISyncDecoration, natives: INativesStore) {

        }

        constructor(sync: ISyncDecoration, natives: INativesStore, public parent: BattleScene) {
                this.synchronize(sync, natives);
        }
}
