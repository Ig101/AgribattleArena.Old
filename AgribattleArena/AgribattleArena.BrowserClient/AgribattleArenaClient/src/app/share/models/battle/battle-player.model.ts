import { BattlePlayerStatusEnum } from '../enums/battle-player-status.enum';
import { IBattleActor } from '.';

export interface IBattlePlayer {
    id: string;
    team?: number;
    keyActorsSync: IBattleActor[];
    turnsSkipped: number;
    status: BattlePlayerStatusEnum;
}
