import { BattleActor, BattleDecoration, BattleSpecEffect, BattleTile } from '../objects';
import { OnChangeArg, OnRemoveArg, OnCreateArg } from '../delegates/synchronization-args';

export interface IEventChangeToken {
    changeAll: boolean;
    objectToChange?: BattleActor | BattleDecoration | BattleSpecEffect | BattleTile;
    args: OnChangeArg[];
    onCreateArgs: OnCreateArg[];
    onRemoveArgs: OnRemoveArg[];
}
