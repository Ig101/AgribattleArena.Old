import { BattleActor, BattleDecoration, BattleSpecEffect, BattleTile } from '../objects';

export interface IEventChangeToken {
    changeAll: boolean;
    objectToChange?: BattleActor | BattleDecoration | BattleSpecEffect | BattleTile;
}
