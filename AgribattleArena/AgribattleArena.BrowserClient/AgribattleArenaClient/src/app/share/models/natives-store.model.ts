import { IActorNative, IDecorationNative, ISpecEffectNative, ISkillNative, IBuffNative, ITileNative } from './natives';

export interface INativesStore {
    actors: IActorNative[];
    decorations: IDecorationNative[];
    specEffects: ISpecEffectNative[];
    skills: ISkillNative[];
    buffs: IBuffNative[];
    tiles: ITileNative[];
}
