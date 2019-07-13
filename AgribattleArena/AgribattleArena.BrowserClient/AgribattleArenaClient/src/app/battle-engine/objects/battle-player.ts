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

    }

    constructor(sync: ISyncPlayer, profiles: IExternalProfile[], public parent: BattleScene) {
        this.synchronize(sync);
        const tempProfile = profiles.find(x => x.id === this.id);
        this.userName = tempProfile.userName;
        this.revelationLevel = tempProfile.revelationLevel;
    }
}
