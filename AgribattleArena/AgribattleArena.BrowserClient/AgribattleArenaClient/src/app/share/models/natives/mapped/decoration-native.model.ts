import { ISpriteNative } from './sprite-native.model';
import { DecorationAction, DecorationOnDeathAction } from 'src/app/battle-engine/delegates/decoration-delegates';

export interface IDecorationNative {
    id: string;
    sprite: ISpriteNative;
    size: number;
    radius: number;
    shadowOpacity: number;
    action: DecorationAction;
    onDeathAction: DecorationOnDeathAction;

    name: string;
    description: string;
}
