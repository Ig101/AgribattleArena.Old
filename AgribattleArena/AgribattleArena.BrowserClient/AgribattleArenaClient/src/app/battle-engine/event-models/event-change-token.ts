import { BattleActor, BattleDecoration, BattleSpecEffect, BattleTile } from '../objects';
import { OnChangeArg, OnRemoveArg, OnCreateArg } from '../delegates/synchronization-args';
import { IEventObjectToChange } from './event-object-to-change.model';

export interface IEventChangeToken {
    changeAll: boolean;

    objectToChange?: IEventObjectToChange;

    args: OnChangeArg[];
    onCreateArgs: OnCreateArg[];
    onRemoveArgs: OnRemoveArg[];
}
