import { BattlePlayer, BattleActor, BattleDecoration, BattleSpecEffect } from './objects';
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

    events: BattleEvent[];

    tempPlayer: BattlePlayer;
    players: BattlePlayer[];
    actors: BattleActor[];
    decorations: BattleDecoration[];
    specEffects: BattleSpecEffect[];

    private syncTimer: NodeJS.Timer;

    constructor(private natives: INativesStore, sync: ISynchronizer, profiles: IExternalProfile) {
        this.pause = true;
        this.syncTimer = setInterval (this.update, ENVIRONMENT.battleUpdateDelay, ENVIRONMENT.battleUpdateDelay);
    }

    private update(milliseconds: number) {
        if (!this.pause) {

        }
    }

    addEventBySignature(signature: IEventActionSignature) {
        this.events.push(new BattleEvent(this, signature));
    }

    addEventBySynchronizer(sync: ISynchronizer, action: BattleChangeInstructionAction) {
        const event = new BattleEvent(this);
        event.uploadSynchronizer(sync, action);
        this.events.push(event);
    }

    dispose() {
        clearInterval(this.syncTimer);
    }
}
