import { BattlePlayerStatusEnum } from '../enums/battle-player-status.enum';
import { IBattleActor } from '.';

export interface IBattlePlayer {
    id: string;
    userName: string;
    revelationLevel: number;
    team?: number;
    keyActors: IBattleActor[];
    turnsSkipped: number;
    status: BattlePlayerStatusEnum;
}
