import { ISkillNative } from 'src/app/share/models/natives';
import { BattleActor } from './battle-actor';
import { ISyncSkill } from 'src/app/share/models/synchronization';
import { BattleScene } from '../battle-scene';
import { INativesStore } from 'src/app/share/models/natives-store.model';

export class BattleSkill {
    id: number;
    range: number;
    native: ISkillNative;
    cd: number;
    mod: number;
    cost: number;
    preparationTime: number;

    synchronize(sync: ISyncSkill, natives: INativesStore) {

    }

    constructor(sync: ISyncSkill, natives: INativesStore, public parent: BattleScene) {
        this.synchronize(sync, natives);
    }
}
