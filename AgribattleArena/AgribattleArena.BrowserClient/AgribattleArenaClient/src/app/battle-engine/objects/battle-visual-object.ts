import { Sprite } from '../sprite';
import { ObjectBehavior } from '../delegates/visual-object-delegates';

export class BattleVisualObject {
    visualX: number;
    visualY: number;
    sprite: Sprite;
    behavior?: ObjectBehavior;

    update(milliseconds: number) {
        if (this.behavior) {
            this.behavior(this, milliseconds);
        }
    }
}
