import { IActorNative, IDecorationNative, ISpecEffectNative, ISkillNative, IBuffNative, ITileNative,
    ISpriteNative } from './natives';

export interface INativesStore {
    sprites: ISpriteNative[];
    actors: IActorNative[];
    decorations: IDecorationNative[];
    specEffects: ISpecEffectNative[];
    skills: ISkillNative[];
    buffs: IBuffNative[];
    tiles: ITileNative[];
}
