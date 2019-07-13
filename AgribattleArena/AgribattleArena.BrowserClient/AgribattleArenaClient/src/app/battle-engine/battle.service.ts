import { Injectable } from '@angular/core';
import { BattleHubService, BATTLE_START_GAME, BATTLE_END_GAME, BATTLE_ATTACK, BATTLE_END_TURN, BATTLE_WAIT,
    BATTLE_CAST, BATTLE_MOVE, BATTLE_SKIP_TURN, BATTLE_DECORATION, BATTLE_NO_ACTORS_DRAW,
    BattleHubReturnMethod } from '../share/battle-hub.service';
import { ISynchronizer } from '../share/models';
import { ProfileService } from '../share/profile.service';
import { ENVIRONMENT } from '../environment';
import { BattleChangeInstructionAction } from '../share/models/enums/battle-change-instruction-action.enum';
import { BattleActor, BattleSkill } from './objects';
import { BattleScene } from './battle-scene';

@Injectable({
    providedIn: 'root'
})
export class BattleService {
    scene?: BattleScene;

    constructor(private battleHubService: BattleHubService, private profileService: ProfileService) {

    }

    private removeScene() {
        if (this.scene) {
            this.scene.dispose();
            this.scene = null;
        }
    }

    syncWithFullSynchronization() {

    }

    orderMove(actor: BattleActor, targetX: number, targetY: number) {
        // conditions
        this.battleHubService.orderMove(actor.id, targetX, targetY, 0.5);
    }

    orderAttack(actor: BattleActor, targetX: number, targetY: number) {
        // conditions
        this.battleHubService.orderAttack(actor.id, targetX, targetY, 0.5);
    }

    orderCast(actor: BattleActor, skill: BattleSkill, targetX: number, targetY: number) {
        // conditions
        this.battleHubService.orderCast(actor.id, skill.id, targetX, targetY, 0.5);
    }

    orderWait(actor: BattleActor) {
        // conditions
        this.battleHubService.orderWait(actor.id, 0.5);
    }

    setupBattle(synchronizer: ISynchronizer) {

    }

    leaveBattle() {
        this.removeScene();
    }
}
