import { ITagSynergy } from '../tag-synergy.model';
import { IBattleSkill, IBattleBuff } from '.';

export interface IBattleActor {
    id: number;
    externalId?: number;
    nativeId: string;
    attackingSkill: IBattleSkill;
    strength: number;
    willpower: number;
    constitution: number;
    speed: number;
    skills: IBattleSkill[];
    actionPointsIncome: number;
    buffs: IBattleBuff[];
    initiativePosition: number;
    health: number;
    ownerId?: string;
    isAlive: boolean;
    x: number;
    y: number;
    z: number;
    maxHealth: number;
    actionPoints: number;
    skillPower: number;
    attackPower: number;
    initiative: number;
    armor: ITagSynergy[];
    attackModifiers: ITagSynergy[];
}
