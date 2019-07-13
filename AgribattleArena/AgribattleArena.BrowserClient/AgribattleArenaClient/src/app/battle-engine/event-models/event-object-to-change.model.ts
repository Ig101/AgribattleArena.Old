import { BattleActor, BattleDecoration, BattleTile, BattleSpecEffect } from '../objects';

export interface IEventObjectToChange {
    id?: number;
    x?: number;
    y?: number;
    type: typeof BattleActor | typeof BattleDecoration | typeof BattleTile | typeof BattleSpecEffect;
}
