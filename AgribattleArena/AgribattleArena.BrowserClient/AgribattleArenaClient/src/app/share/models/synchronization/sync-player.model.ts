import { BattlePlayerStatusEnum } from '../enums/battle-player-status.enum';

export interface ISyncPlayer {
    id: string;
    team?: number;
    keyActorsSync: number[];
    turnsSkipped: number;
    status: BattlePlayerStatusEnum;
}
