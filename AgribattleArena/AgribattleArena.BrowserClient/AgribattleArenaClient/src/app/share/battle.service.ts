import { Injectable } from '@angular/core';
import { IBattleState } from './models/battle-state.model';
import { BattleHubService, BATTLE_START_GAME, BATTLE_END_GAME } from './battle-hub.service';
import { ISynchronizer } from './models';
import { IBattlePlayer, IBattleActor, IBattleSkill } from './models/battle';

@Injectable({
    providedIn: 'root'
})
export class BattleService {

    state?: IBattleState;

    constructor(private battleHubService: BattleHubService) {
        battleHubService.addNewListener(BATTLE_START_GAME, (synchronizer: ISynchronizer) => this.setupBattle(synchronizer));
        battleHubService.addNewListener(BATTLE_END_GAME, (synchronizer: ISynchronizer) => this.endBattle(synchronizer));
    }
    
    syncWithFullSynchronization() {

    }

    orderMove(actor: IBattleActor, targetX: number, targetY: number) {
        this.battleHubService.orderMove(actor.id, targetX, targetY, 0.5);
    }

    orderAttack(actor: IBattleActor, targetX: number, targetY: number) {
        this.battleHubService.orderAttack(actor.id, targetX, targetY, 0.5);
    }

    orderCast(actor: IBattleActor, skill: IBattleSkill, targetX: number, targetY: number) {
        this.battleHubService.orderCast(actor.id, skill.id, targetX, targetY, 0.5);
    }

    orderWait(actor: IBattleActor) {
        this.battleHubService.orderWait(actor.id, 0.5);
    }

    private syncWithSynchronization(synchronizer: ISynchronizer) {

    }

    private setupBattle(synchronizer: ISynchronizer) {
        // TODO
        this.syncWithSynchronization(synchronizer);
    }

    private endBattle(synchronizer: ISynchronizer) {
        this.syncWithSynchronization(synchronizer);
        // TODO
    }
}
