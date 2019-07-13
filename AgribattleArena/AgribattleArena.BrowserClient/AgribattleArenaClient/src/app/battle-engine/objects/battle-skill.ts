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
        this.range = sync.range;
        if (!this.native || this.native.id !== sync.nativeId) {
            this.native = natives.skills.find(x => x.id === sync.nativeId);
        }
        this.cd = sync.cd;
        this.mod = sync.mod;
        this.cost = sync.cost;
        this.preparationTime = sync.preparationTime;
    }

    constructor(sync: ISyncSkill, natives: INativesStore, public parent: BattleActor) {
        this.id = sync.id;
        this.synchronize(sync, natives);
    }
}
