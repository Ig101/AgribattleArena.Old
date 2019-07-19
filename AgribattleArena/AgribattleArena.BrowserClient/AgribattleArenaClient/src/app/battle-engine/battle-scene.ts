import { BattlePlayer, BattleActor, BattleDecoration, BattleSpecEffect, BattleTile, BattleVisualObject } from './objects';
import { ISynchronizer } from '../share/models';
import { BattleEvent } from './battle-event';
import { IEventActionSignature } from './event-models/event-action-signature.model';
import { BattleChangeInstructionAction } from '../share/models/enums/battle-change-instruction-action.enum';
import { ENVIRONMENT } from '../environment';
import { IExternalProfile } from '../share/models/external-profile.model';
import { IEventChangeToken } from './event-models/event-change-token';
import { ISyncActor, ISyncDecoration, ISyncSpecEffect, ISyncTile } from '../share/models/synchronization';
import { OnChangeArg, OnRemoveArg, OnCreateArg } from './delegates/synchronization-args';
import { INativesStoreMapped } from '../share/models/natives-store-mapped.model';

export class BattleScene {
    pause: boolean;
    tempPlayer: BattlePlayer;
    result?: boolean;

    version: number;
    timeUntilEndOfTurn: number;

    events: BattleEvent[];
    tempEvent?: BattleEvent;

    tempActor: BattleDecoration | BattleActor;
    players: BattlePlayer[];
    actors: BattleActor[];
    decorations: BattleDecoration[];
    specEffects: BattleSpecEffect[];
    tiles: BattleTile[];
    visualObjects: BattleVisualObject[];
    tilesetWidth: number;
    tilesetHeight: number;

    private syncTimer: NodeJS.Timer;

    fullSynchronizationWithoutPlayers(sync: ISynchronizer) {
        this.version = sync.version;
        if (Math.abs(this.timeUntilEndOfTurn - sync.turnTime) > 2000 || this.tempActor.id !== sync.tempActor.id) {
            this.timeUntilEndOfTurn = sync.turnTime * 1000;
        }
        this.tilesetWidth = sync.tilesetWidth;
        this.tilesetHeight = sync.tilesetHeight;
        this.tiles = [];
        for (const tile of sync.changedTiles) {
                this.tiles.push(new BattleTile(tile, this.natives, this));
        }
        this.actors = [];
        for (const actor of sync.changedActors) {
            this.actors.push(new BattleActor(actor, this.natives, this));
        }
        this.decorations = [];
        for (const decoration of sync.changedDecorations) {
            this.decorations.push(new BattleDecoration(decoration, this.natives, this));
        }
        this.specEffects = [];
        for (const specEffect of sync.changedEffects) {
            this.specEffects.push(new BattleSpecEffect(specEffect, this.natives, this));
        }
        if (sync.tempActor) {
            this.tempActor = this.actors.find(x => x.id === sync.tempActor.id);
        } else {
            this.tempActor = this.decorations.find(x => x.id === sync.tempDecoration.id);
        }
    }

    constructor(public natives: INativesStoreMapped, sync: ISynchronizer, profiles: IExternalProfile[]) {
        this.events = [];
        this.visualObjects = [];
        this.pause = true;
        this.result = null;
        this.fullSynchronizationWithoutPlayers(sync);
        this.players = [];
        for (const player of sync.players) {
            this.players.push(new BattlePlayer(player, profiles, this));
        }
        this.syncTimer = setInterval (this.update, ENVIRONMENT.battleUpdateDelay, ENVIRONMENT.battleUpdateDelay);
    }

    fullSynchronization(sync: ISynchronizer) {
        this.fullSynchronizationWithoutPlayers(sync);
        for (const player of sync.players) {
            this.players.find(x => x.id === player.id).synchronize(player);
        }
    }

    changeActor(actor: ISyncActor,
                args: OnChangeArg[], createArgs: OnCreateArg[]) {
        const tempActor = this.actors.find(x => x.id === actor.id);
        if (tempActor) {
            for (const arg of args) {
                arg(this, tempActor, actor);
            }
        } else {
            for (const arg of createArgs) {
                arg(this, actor, BattleActor);
            }
        }
    }

    removeActor(actor: ISyncActor,
                args: OnRemoveArg[]) {
        const index = this.actors.findIndex(x => x.id === actor.id);
        if (index >= 0) {
            for (const arg of args) {
                arg(this, index, BattleActor);
            }
        }
    }
    changeDecoration(decoration: ISyncDecoration,
                     args: OnChangeArg[], createArgs: OnCreateArg[]) {
        const tempDecoration = this.decorations.find(x => x.id === decoration.id);
        if (tempDecoration) {
            for (const arg of args) {
                    arg(this, tempDecoration, decoration);
            }
        } else {
            for (const arg of createArgs) {
                arg(this, decoration, BattleDecoration);
            }
        }
    }

    removeDecoration(decoration: ISyncDecoration,
                     args: OnRemoveArg[]) {
        const index = this.decorations.findIndex(x => x.id === decoration.id);
        if (index >= 0) {
            for (const arg of args) {
                arg(this, index, BattleDecoration);
            }
        }
    }

    changeSpecEffect(specEffect: ISyncSpecEffect,
                     args: OnChangeArg[], createArgs: OnCreateArg[]) {
        const tempSpecEffect = this.specEffects.find(x => x.id === specEffect.id);
        if (tempSpecEffect) {
            for (const arg of args) {
                arg(this, tempSpecEffect, specEffect);
            }
        } else {
            for (const arg of createArgs) {
                arg(this, specEffect, BattleSpecEffect);
            }
        }
    }

    removeSpecEffect(specEffect: ISyncSpecEffect,
                     args: OnRemoveArg[]) {
        const index = this.specEffects.findIndex(x => x.id === specEffect.id);
        if (index >= 0) {
            for (const arg of args) {
                    arg(this, index, BattleSpecEffect);
            }
        }
    }

    changeTile(tile: ISyncTile,
               args: OnChangeArg[]) {
        const tempTile = this.tiles.find(x => x.x === tile.x && x.y === tile.y);
        if (tempTile) {
            for (const arg of args) {
                arg(this, tempTile, tile);
            }
        }
    }

    synchronize(sync: ISynchronizer, token: IEventChangeToken) {
        this.version = sync.version;
        if (token.changeAll) {
            for (const tile of sync.changedTiles) {
                this.changeTile(tile, token.args);
            }
            for (const actor of sync.changedActors) {
                this.changeActor(actor, token.args, token.onCreateArgs);
            }
            for (const decoration of sync.changedDecorations) {
                this.changeDecoration(decoration, token.args, token.onCreateArgs);
            }
            for (const effect of sync.changedEffects) {
                this.changeSpecEffect(effect, token.args, token.onCreateArgs);
            }
            for (const actor of sync.deletedActors) {
                this.removeActor(actor, token.onRemoveArgs);
            }
            for (const decoration of sync.deletedDecorations) {
                this.removeDecoration(decoration, token.onRemoveArgs);
            }
            for (const effect of sync.deletedEffects) {
                this.removeSpecEffect(effect, token.onRemoveArgs);
            }
            if (Math.abs(this.timeUntilEndOfTurn - sync.turnTime) > 2000 || this.tempActor.id !== sync.tempActor.id) {
                this.timeUntilEndOfTurn = sync.turnTime * 1000;
            }
            if (sync.tempActor) {
                this.tempActor = this.actors.find(x => x.id === sync.tempActor.id);
            } else {
                this.tempActor = this.decorations.find(x => x.id === sync.tempDecoration.id);
            }
            for (const player of sync.players) {
                this.players.find(x => x.id === player.id).synchronize(player);
            }
        } else {
            switch (token.objectToChange.type) {
                case BattleActor:
                    let syncActor = sync.deletedActors.find(x => x.id === token.objectToChange.id);
                    if (syncActor) {
                        this.removeActor(syncActor, token.onRemoveArgs);
                        sync.deletedActors.splice(sync.deletedActors.indexOf(syncActor), 1);
                        return;
                    }
                    syncActor = sync.changedActors.find(x => x.id === token.objectToChange.id);
                    if (syncActor) {
                        this.changeActor(syncActor, token.args, token.onCreateArgs);
                        sync.changedActors.splice(sync.changedActors.indexOf(syncActor), 1);
                        return;
                    }
                    break;
                case BattleDecoration:
                    let syncDecoration = sync.deletedDecorations.find(x => x.id === token.objectToChange.id);
                    if (syncDecoration) {
                        this.removeDecoration(syncDecoration, token.onRemoveArgs);
                        sync.deletedDecorations.splice(sync.deletedDecorations.indexOf(syncDecoration), 1);
                        return;
                    }
                    syncDecoration = sync.changedDecorations.find(x => x.id === token.objectToChange.id);
                    if (syncDecoration) {
                        this.changeDecoration(syncDecoration, token.args, token.onCreateArgs);
                        sync.changedDecorations.splice(sync.changedDecorations.indexOf(syncDecoration), 1);
                        return;
                    }
                    break;
                case BattleTile:
                    const syncTile = sync.changedTiles.find(x => x.x === token.objectToChange.x && x.y === token.objectToChange.y);
                    if (syncTile) {
                        this.changeTile(syncTile, token.args);
                        sync.changedTiles.splice(sync.changedTiles.indexOf(syncTile), 1);
                        return;
                    }
                    break;
                case BattleSpecEffect:
                    let syncEffect = sync.deletedEffects.find(x => x.id === token.objectToChange.id);
                    if (syncEffect) {
                        this.removeSpecEffect(syncEffect, token.onRemoveArgs);
                        sync.deletedEffects.splice(sync.deletedEffects.indexOf(syncEffect), 1);
                        return;
                    }
                    syncEffect = sync.changedEffects.find(x => x.id === token.objectToChange.id);
                    if (syncEffect) {
                        this.changeSpecEffect(syncEffect, token.args, token.onCreateArgs);
                        sync.changedEffects.splice(sync.changedEffects.indexOf(syncEffect), 1);
                        return;
                    }
                    break;
            }
        }
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

    private endTurn() {
        // TODO
    }

    private endGame() {
        // TODO
    }

    private update(milliseconds: number) {
        if (!this.pause) {
            if (this.tempEvent === null && this.events.length > 0) {
                this.startNewEvent();
            }
            this.timeUntilEndOfTurn -= milliseconds;
            for (const visualObject of this.visualObjects) {
                visualObject.update(milliseconds);
            }
            for (const tile of this.tiles) {
                tile.update(milliseconds);
            }
            for (const decoration of this.decorations) {
                decoration.update(milliseconds);
            }
            for (const actor of this.actors) {
                actor.update(milliseconds);
            }
            for (const specEffect of this.specEffects) {
                specEffect.update(milliseconds);
            }
            for (const player of this.players) {
                player.update(milliseconds);
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
