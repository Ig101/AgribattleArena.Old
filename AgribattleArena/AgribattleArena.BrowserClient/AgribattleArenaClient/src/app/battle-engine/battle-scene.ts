import { BattlePlayer, BattleActor, BattleDecoration, BattleSpecEffect, BattleTile } from './objects';
import { INativesStore } from '../share/models/natives-store.model';
import { ISynchronizer } from '../share/models';
import { BattleEvent } from './battle-event';
import { IEventActionSignature } from './event-models/event-action-signature.model';
import { BattleChangeInstructionAction } from '../share/models/enums/battle-change-instruction-action.enum';
import { ENVIRONMENT } from '../environment';
import { IExternalProfile } from '../share/models/external-profile.model';

export class BattleScene {
    version: number;
    pause: boolean;
    tempPlayer: BattlePlayer;
    result?: boolean;

    events: BattleEvent[];
    tempEvent?: BattleEvent;

    tempActor: BattleDecoration | BattleActor;
    players: BattlePlayer[];
    actors: BattleActor[];
    decorations: BattleDecoration[];
    specEffects: BattleSpecEffect[];
    tiles: BattleTile[];

    private syncTimer: NodeJS.Timer;

    constructor(private natives: INativesStore, sync: ISynchronizer, profiles: IExternalProfile[]) {
        this.events = [];
        this.pause = true;
        this.result = null;
        this.syncTimer = setInterval (this.update, ENVIRONMENT.battleUpdateDelay, ENVIRONMENT.battleUpdateDelay);
    }

    private startNewEvent() {
        do {
            this.tempEvent = this.events.shift();
        } while (this.tempEvent.version <= this.version);
        if (this.tempEvent) {
            this.version = this.tempEvent.version;
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
                    this.endTurn();
                    break;
                case BattleChangeInstructionAction.EndGame:
                    this.endGame();
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

    private async endTurn() {
        // TODO
    }

    private async endGame() {
        // TODO
    }

    private update(milliseconds: number) {
        if (!this.pause) {
            if (this.tempEvent === null && this.events.length > 0) {
                this.startNewEvent();
            }
        }
    }

    addEventBySignature(signature: IEventActionSignature) {
        this.events.push(new BattleEvent(this, signature));
        if (!this.pause && this.tempEvent === null) {
            this.startNewEvent();
        }
    }

    private checkEvent(tempEvent: BattleEvent, sync: ISynchronizer, action: BattleChangeInstructionAction, index?: number): boolean {
        if (tempEvent.version) {
            if (tempEvent.version > sync.version) {
                if (index) {
                    const event = new BattleEvent(this);
                    event.uploadSynchronizer(sync, action);
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
                tempEvent.uploadSynchronizer(sync, action);
                return false;
        }
        return true;
    }

    addEventBySynchronizer(sync: ISynchronizer, action: BattleChangeInstructionAction) {
        for (const i in this.events) {
            if (!this.checkEvent(this.events[i], sync, action, this.events.indexOf(this.events[i]))) {
                return;
            }
        }
        if (this.tempEvent != null && !this.checkEvent(this.tempEvent, sync, action)) {
            return;
        }
        const event = new BattleEvent(this);
        event.uploadSynchronizer(sync, action);
        this.events.push(event);
        if (!this.pause && this.tempEvent === null) {
            this.startNewEvent();
        }
    }

    dispose() {
        clearInterval(this.syncTimer);
    }
}
