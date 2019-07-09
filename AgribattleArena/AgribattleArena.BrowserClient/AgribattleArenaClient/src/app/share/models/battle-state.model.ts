import { IBattleActor } from './battle';
import { IBattleDecoration } from './battle/battle-decoration.model';
import { IBattleSpecEffect } from './battle/battle-spec-effect.model';

export interface IBattleState {
    version: number;
    actors: IBattleActor[];
    decorations: IBattleDecoration[];
    specEffects: IBattleSpecEffect[];
}
