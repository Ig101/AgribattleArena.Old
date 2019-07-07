import { BattlePlayerStatusEnum } from '../enums/battle-player-status.enum';

export interface IBattlePlayer {
    id: string;
    team?: number;
    keyActorsSync: number[];
    turnsSkipped: number;
    status: BattlePlayerStatusEnum;
}
