import { EventEmitter } from '@angular/core';
import { IParentActionComponent } from '../interfaces/parent-action-component.interface';

export class ParentEventEmitterHelper {
    static subscribe(emitter: EventEmitter<any>, component: IParentActionComponent) {
        emitter.subscribe((object: any) => {
            component.parentAction(object);
        });
    }
}
