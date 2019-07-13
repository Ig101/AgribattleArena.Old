import { ITagSynergy } from 'src/app/share/models/tag-synergy.model';
import { BattleSkill, BattleBuff } from '.';
import { IActorNative } from 'src/app/share/models/natives';
import { BattleScene } from '../battle-scene';
import { ISyncActor } from 'src/app/share/models/synchronization';
import { BattleTile } from './battle-tile';

export class BattleActor {
    id: number;
    externalId?: number;
    native: IActorNative;
    attackingSkill: BattleSkill;
    strength: number;
    willpower: number;
    constitution: number;
    speed: number;
    skills: BattleSkill[];
    actionPointsIncome: number;
    buffs: BattleBuff[];
    initiativePosition: number;
    health: number;
    ownerId?: string;
    isAlive: boolean;
    x: number;
    y: number;
    tempTile: BattleTile
    z: number;
    maxHealth: number;
    actionPoints: number;
    skillPower: number;
    attackPower: number;
    initiative: number;
    armor: ITagSynergy[];
    attackModifiers: ITagSynergy[];

    synchronize(sync: ISyncActor, natives: INativesStore) {

    }

    constructor(sync: ISyncActor, natives: INativesStore, public parent: BattleScene) {
        this.synchronize(sync, natives);
    }
}
