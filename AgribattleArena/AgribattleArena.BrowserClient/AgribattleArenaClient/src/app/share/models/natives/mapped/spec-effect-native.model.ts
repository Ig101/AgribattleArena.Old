import { ISpriteNative } from './sprite-native.model';
import { EffectAction, EffectOnDeathAction } from 'src/app/battle-engine/delegates/effect-delegates';

export interface ISpecEffectNative {
    id: string;
    sprite: ISpriteNative;
    size: number;
    action: EffectAction;
    onDeathAction: EffectOnDeathAction;

    name: string;
    description: string;
}
