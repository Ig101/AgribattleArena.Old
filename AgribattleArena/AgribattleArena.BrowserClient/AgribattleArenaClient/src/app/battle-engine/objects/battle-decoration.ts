import { ITagSynergy } from '../../share/models/tag-synergy.model';
import { IDecorationNative } from 'src/app/share/models/natives/mapped';
import { BattleScene } from '../battle-scene';
import { ISyncDecoration } from 'src/app/share/models/synchronization';
import { BattleTile } from './battle-tile';
import { BattlePlayer } from './battle-player';
import { INativesStoreMapped } from 'src/app/share/models/natives-store-mapped.model';
import { BattleVisualObject } from './battle-visual-object';

export class BattleDecoration extends BattleVisualObject {
        id: number;
        native: IDecorationNative;
        mod: number;
        initiativePosition: number;
        health: number;
        owner?: BattlePlayer;
        isAlive: boolean;
        x: number;
        y: number;
        tempTile: BattleTile;
        z: number;
        maxHealth: number;
        armor: ITagSynergy[];

        synchronize(sync: ISyncDecoration, natives: INativesStoreMapped) {
                if (!this.native || this.native.id !== sync.nativeId) {
                        this.native = natives.decorations.find(x => x.id === sync.nativeId);
                }
                this.mod = sync.mod;
                this.initiativePosition = sync.initiativePosition;
                this.health = sync.health;
                if (!sync.ownerId && this.owner) {
                        this.owner = null;
                } else if ((!this.owner && sync.ownerId) || (this.owner && this.owner.id !== sync.ownerId)) {
                        this.owner = this.parent.players.find(x => x.id === sync.ownerId);
                }
                this.isAlive = sync.isAlive;
                if (!this.tempTile || this.x !== sync.x || this.y !== sync.y) {
                        this.tempTile = this.parent.tiles[sync.x][sync.y];
                }
                this.x = sync.x;
                this.y = sync.y;
                this.z = sync.z;
                this.maxHealth = sync.maxHealth;
                this.armor = sync.armor;
        }

        constructor(sync: ISyncDecoration, natives: INativesStoreMapped, public parent: BattleScene) {
                super();
                this.id = sync.id;
                this.synchronize(sync, natives);
                this.tempTile.tempActor = this;
        }

        cast() {
                // TODO
        }

        update(milliseconds: number) {
                super.update(milliseconds);
        }
}
