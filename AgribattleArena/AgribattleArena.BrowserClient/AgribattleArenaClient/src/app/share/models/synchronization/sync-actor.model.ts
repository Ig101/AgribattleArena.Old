import { ISyncSkill, ISyncBuff } from '.';
import { ITagSynergy } from '../tag-synergy.model';

export interface ISyncActor {
    id: number;
    externalId?: number;
    nativeId: string;
    attackingSkill: ISyncSkill;
    strength: number;
    willpower: number;
    constitution: number;
    speed: number;
    skills: ISyncSkill[];
    actionPointsIncome: number;
    buffs: ISyncBuff[];
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
