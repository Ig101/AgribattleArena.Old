import { BattleSkill, BattleActor, BattleDecoration } from '../objects';
import { BattleChangeInstructionAction } from 'src/app/share/models/enums/battle-change-instruction-action.enum';

export interface IEventActionSignature {
    action: BattleChangeInstructionAction;
    actor?: BattleActor | BattleDecoration;
    x?: number;
    y?: number;
    skill?: BattleSkill;
}
