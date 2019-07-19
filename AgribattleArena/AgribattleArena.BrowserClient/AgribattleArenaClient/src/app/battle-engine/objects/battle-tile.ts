import { ITileNative } from 'src/app/share/models/natives/mapped';
import { BattleScene } from '../battle-scene';
import { ISyncTile } from 'src/app/share/models/synchronization';

import { BattlePlayer } from './battle-player';
import { BattleDecoration } from './battle-decoration';
import { BattleActor } from './battle-actor';
import { INativesStoreMapped } from 'src/app/share/models/natives-store-mapped.model';

export class BattleTile {
    x: number;
    y: number;
    tempActor?: BattleDecoration | BattleActor;
    height: number;
    native: ITileNative;
    owner?: BattlePlayer;

    synchronize(sync: ISyncTile, natives: INativesStoreMapped) {
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

    constructor(sync: ISyncTile, natives: INativesStoreMapped, public parent: BattleScene) {
        this.x = sync.x;
        this.y = sync.y;
        this.synchronize(sync, natives);
    }

    update(milliseconds: number) {

    }
}
