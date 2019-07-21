import { BattleEvent } from './battle-event';
import { BattleScene } from './battle-scene';
import { ISynchronizer } from '../share/models';
import { BattleChangeInstructionAction } from '../share/models/enums/battle-change-instruction-action.enum';
import { IEventActionSignature } from './event-models/event-action-signature.model';
import { BattleActor, BattleDecoration, BattleSkill } from './objects';

export class BattleEventsManager {

    private events: BattleEvent[];
    private tempEvent?: BattleEvent;

    constructor(private scene: BattleScene) {
        this.events = [];
        this.tempEvent = null;
    }

    isEventsQueueEmpty() {
        return this.tempEvent === null && this.events.length <= 0;
    }

    clearQueue() {
        this.tempEvent = null;
        this.events = [];
    }

    startNewEventIfTempIsNull() {
        if (this.tempEvent === null && this.events.length > 0) {
            this.startNewEvent();
        }
    }

    private startNewEvent() {
        do {
            this.tempEvent = this.events.shift();
        } while (this.tempEvent.version <= this.scene.version);
        if (this.tempEvent) {
            this.scene.version = this.tempEvent.version;
            this.tempEvent.processor.subscribe(() => this.onEventEnd());
            const signature = this.tempEvent.actionSignature;
            switch (signature.action) {
                case BattleChangeInstructionAction.Move:
                    (signature.actor as BattleActor).move(signature.x, signature.y);
                    break;
                case BattleChangeInstructionAction.Attack:
                    (signature.actor as BattleActor).attack(signature.x, signature.y);
                    break;
                case BattleChangeInstructionAction.Cast:
                    (signature.actor as BattleActor).cast(signature.skill, signature.x, signature.y);
                    break;
                case BattleChangeInstructionAction.Decoration:
                    (signature.actor as BattleDecoration).cast();
                    break;
                case BattleChangeInstructionAction.Wait:
                    (signature.actor as BattleActor).wait();
                    break;
                case BattleChangeInstructionAction.EndTurn:
                    this.scene.endTurn();
                    break;
                case BattleChangeInstructionAction.EndGame:
                    this.scene.endGame();
                    break;
            }
        }
    }

    private onEventEnd() {
        this.tempEvent.processor.unsubscribe();
        this.tempEvent = null;
        if (this.events.length > 0) {
            this.startNewEvent();
        }
    }

    addEventBySignature(signature: IEventActionSignature) {
        this.events.push(new BattleEvent(this.scene, signature));
        if (!this.scene.pause && this.tempEvent === null) {
            this.startNewEvent();
        }
    }

    private checkEvent(tempEvent: BattleEvent, sync: ISynchronizer, action: BattleChangeInstructionAction,
                       isTempEvent: boolean, index?: number): boolean {
        if (tempEvent.version) {
            if (tempEvent.version > sync.version) {
                if (index) {
                    const event = new BattleEvent(this.scene);
                    event.uploadSynchronizer(sync, action, isTempEvent);
                    this.events.splice(index, 0, event);
                }
                return false;
            } else if (tempEvent.version === sync.version) {
                return false;
            }
        } else if (tempEvent.actionSignature.action === action &&
            ((tempEvent.actionSignature.actor === null && sync.actorId === null) ||
            tempEvent.actionSignature.actor.id === sync.actorId) &&
            tempEvent.actionSignature.x === sync.targetX &&
            tempEvent.actionSignature.y === sync.targetY &&
            ((tempEvent.actionSignature.skill === null && sync.skillActionId === null) ||
            tempEvent.actionSignature.skill.id === sync.skillActionId)) {
                tempEvent.uploadSynchronizer(sync, action, isTempEvent);
                return false;
        }
        return true;
    }

    addEventBySynchronizer(sync: ISynchronizer, action: BattleChangeInstructionAction) {
        console.log('adding event' + sync.version);
        for (const i in this.events) {
            if (!this.checkEvent(this.events[i], sync, action, false, this.events.indexOf(this.events[i]))) {
                return;
            }
        }
        if (this.tempEvent != null && !this.checkEvent(this.tempEvent, sync, action, true)) {
            return;
        }
        const event = new BattleEvent(this.scene);
        event.uploadSynchronizer(sync, action, false);
        this.events.push(event);
        if (!this.scene.pause && this.tempEvent === null) {
            this.startNewEvent();
        }
        console.log('stop adding event' + sync.version);
    }
}
