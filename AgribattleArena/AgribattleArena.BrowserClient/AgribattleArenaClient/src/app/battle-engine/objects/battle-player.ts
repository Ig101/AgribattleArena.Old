import { BattlePlayerStatusEnum } from 'src/app/share/models/enums/battle-player-status.enum';
import { BattleActor } from '.';
import { BattleScene } from '../battle-scene';
import { ISyncPlayer } from 'src/app/share/models/synchronization';
import { INativesStore } from 'src/app/share/models/natives-store.model';
import { IExternalProfile } from 'src/app/share/models/external-profile.model';

export class BattlePlayer {
    id: string;
    userName: string;
    revelationLevel: number;
    team?: number;
    keyActors: BattleActor[];
    turnsSkipped: number;
    status: BattlePlayerStatusEnum;

    synchronize(sync: ISyncPlayer) {
        this.team = sync.team;
        if (!this.keyActors) {
            this.keyActors = [];
            for (const i in sync.keyActorsSync) {
                if (sync.keyActorsSync.hasOwnProperty(i)) {
                    if (!this.keyActors.find(x => x.id === sync.keyActorsSync[i])) {
                        this.keyActors.push(this.parent.actors.find(x => x.id === sync.keyActorsSync[i]));
                    }
                }
            }
        }
        this.turnsSkipped = sync.turnsSkipped;
        this.status = sync.status;
    }

    constructor(sync: ISyncPlayer, profiles: IExternalProfile[], public parent: BattleScene) {
        this.id = sync.id;
        this.synchronize(sync);
        const tempProfile = profiles.find(x => x.id === this.id);
        this.userName = tempProfile.userName;
        this.revelationLevel = tempProfile.revelationLevel;
    }
}
