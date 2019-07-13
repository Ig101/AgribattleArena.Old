import { ITileNative } from 'src/app/share/models/natives';
import { BattleScene } from '../battle-scene';
import { ISyncTile } from 'src/app/share/models/synchronization';
import { INativesStore } from 'src/app/share/models/natives-store.model';
import { BattlePlayer } from './battle-player';
import { BattleDecoration } from './battle-decoration';
import { BattleActor } from './battle-actor';

export class BattleTile {
    x: number;
    y: number;
    tempActor?: BattleDecoration | BattleActor;
    height: number;
    native: ITileNative;
    owner?: BattlePlayer;

    synchronize(sync: ISyncTile, natives: INativesStore) {
        if (!this.native || this.native.id !== sync.nativeId) {
            this.native = natives.tiles.find(x => x.id === sync.nativeId);
        }
        if (!sync.ownerId && this.owner) {
            this.owner = null;
        } else if ((!this.owner && sync.ownerId) || (this.owner && this.owner.id !== sync.ownerId)) {
            this.owner = this.parent.players.find(x => x.id === sync.ownerId);
        }
        if (!sync.tempActorId && this.tempActor) {
            this.tempActor = null;
        } else if ((!this.tempActor && sync.tempActorId) || (this.tempActor && this.tempActor.id !== sync.tempActorId)) {
            this.tempActor = this.parent.actors.find(x => x.id === sync.tempActorId);
            if (this.tempActor === null) {
                this.tempActor = this.parent.decorations.find(x => x.id === sync.tempActorId);
            }
        }
        this.height = sync.height;
    }

    constructor(sync: ISyncTile, natives: INativesStore, public parent: BattleScene) {
        this.x = sync.x;
        this.y = sync.y;
        this.synchronize(sync, natives);
    }
}
