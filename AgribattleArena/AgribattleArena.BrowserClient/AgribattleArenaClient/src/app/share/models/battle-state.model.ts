import { IBattleActor } from './battle';
import { IBattleDecoration } from './battle/battle-decoration.model';
import { IBattleSpecEffect } from './battle/battle-spec-effect.model';
import { IBattlePlayer } from './battle/battle-player.model';

export interface IBattleState {
    version: number;
    tempPlayer: IBattlePlayer;
    players: IBattlePlayer[];
    actors: IBattleActor[];
    decorations: IBattleDecoration[];
    specEffects: IBattleSpecEffect[];
}
