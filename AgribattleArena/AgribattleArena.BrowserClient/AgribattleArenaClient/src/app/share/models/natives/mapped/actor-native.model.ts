import { ISpriteNative } from './sprite-native.model';

export interface IActorNative {
    id: string;
    sprite: ISpriteNative;
    size: number;
    radius: number;
    shadowOpacity: number;

    icon: string;
    name: string;
    description: string;
    fullDescription: string;
}
