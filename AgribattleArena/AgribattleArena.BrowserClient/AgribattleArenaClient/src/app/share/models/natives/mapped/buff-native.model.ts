import { BuffAction, BuffOnPurgeAction } from 'src/app/battle-engine/delegates/buff-delegates';

export interface IBuffNative {
    id: string;
    action: BuffAction;
    onPurgeAction: BuffOnPurgeAction;

    icon: string;
    name: string;
    description: string;
}
