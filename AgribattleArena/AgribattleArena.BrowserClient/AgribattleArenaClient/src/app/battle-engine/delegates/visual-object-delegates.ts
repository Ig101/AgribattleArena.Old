import { BattleVisualObject } from '../objects';

export type ObjectBehavior = (obj: BattleVisualObject, milliseconds: number) => void;
