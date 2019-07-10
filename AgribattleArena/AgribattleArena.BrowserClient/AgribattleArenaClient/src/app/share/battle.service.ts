import { Injectable } from '@angular/core';
import { IBattleState } from './models/battle-state.model';
import { BattleHubService, BATTLE_START_GAME, BATTLE_END_GAME, BATTLE_ATTACK, BATTLE_END_TURN, BATTLE_WAIT,
    BATTLE_CAST, BATTLE_MOVE, BATTLE_SKIP_TURN, BATTLE_DECORATION, BATTLE_NO_ACTORS_DRAW } from './battle-hub.service';
import { ISynchronizer } from './models';
import { IBattlePlayer, IBattleActor, IBattleSkill } from './models/battle';
import { ProfileService } from './profile.service';

@Injectable({
    providedIn: 'root'
})
export class BattleService {

    state?: IBattleState;
    // TODO Events processing

    constructor(private battleHubService: BattleHubService, private profileService: ProfileService) {
        battleHubService.addNewListener(BATTLE_START_GAME, (synchronizer: ISynchronizer) => this.setupBattle(synchronizer));
        battleHubService.addNewListener(BATTLE_END_GAME, (synchronizer: ISynchronizer) => this.endBattle(synchronizer));
        battleHubService.addNewListener(BATTLE_ATTACK, (synchronizer: ISynchronizer) => this.processAttackAction(synchronizer));
        battleHubService.addNewListener(BATTLE_MOVE, (synchronizer: ISynchronizer) => this.processMoveAction(synchronizer));
        battleHubService.addNewListener(BATTLE_CAST, (synchronizer: ISynchronizer) => this.processCastAction(synchronizer));
        battleHubService.addNewListener(BATTLE_WAIT, (synchronizer: ISynchronizer) => this.processWaitAction(synchronizer));
        battleHubService.addNewListener(BATTLE_END_TURN, (synchronizer: ISynchronizer) => this.processEndTurn(synchronizer));
        battleHubService.addNewListener(BATTLE_SKIP_TURN, (synchronizer: ISynchronizer) => this.processSkipTurn(synchronizer));
        battleHubService.addNewListener(BATTLE_DECORATION, (synchronizer: ISynchronizer) => this.processDecorationAction(synchronizer));
        battleHubService.addNewListener(BATTLE_NO_ACTORS_DRAW, (synchronizer: ISynchronizer) => this.processNoActorsDraw(synchronizer));
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

    }

    private endBattle(synchronizer: ISynchronizer) {

    }

    private processMoveAction(synchronizer: ISynchronizer) {

    }

    private processAttackAction(synchronizer: ISynchronizer) {

    }

    private processCastAction(synchronizer: ISynchronizer) {

    }

    private processWaitAction(synchronizer: ISynchronizer) {

    }

    private processDecorationAction(synchronizer: ISynchronizer) {

    }

    private processSkipTurn(synchronizer: ISynchronizer) {

    }

    private processNoActorsDraw(synchronizer: ISynchronizer) {

    }

    private processEndTurn(synchronizer: ISynchronizer) {

    }
}
