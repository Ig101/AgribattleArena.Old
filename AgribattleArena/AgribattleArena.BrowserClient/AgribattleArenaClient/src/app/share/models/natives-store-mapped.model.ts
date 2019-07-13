import { IActorNative, IDecorationNative, ISpecEffectNative, ISkillNative, IBuffNative, ITileNative } from './natives/mapped';

export interface INativesStoreMapped {
    actors: IActorNative[];
    decorations: IDecorationNative[];
    specEffects: ISpecEffectNative[];
    skills: ISkillNative[];
    buffs: IBuffNative[];
    tiles: ITileNative[];
}
