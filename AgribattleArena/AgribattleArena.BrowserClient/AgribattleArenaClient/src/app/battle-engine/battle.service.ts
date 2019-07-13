import { Injectable } from '@angular/core';
import { BattleHubService, BATTLE_START_GAME, BATTLE_END_GAME, BATTLE_ATTACK, BATTLE_END_TURN, BATTLE_WAIT,
    BATTLE_CAST, BATTLE_MOVE, BATTLE_SKIP_TURN, BATTLE_DECORATION, BATTLE_NO_ACTORS_DRAW,
    BattleHubReturnMethod } from '../share/battle-hub.service';
import { ISynchronizer, IExternalWrapper, IProfileStatus } from '../share/models';
import { ProfileService } from '../share/profile.service';
import { ENVIRONMENT, STRINGS } from '../environment';
import { BattleChangeInstructionAction } from '../share/models/enums/battle-change-instruction-action.enum';
import { BattleActor, BattleSkill } from './objects';
import { BattleScene } from './battle-scene';
import { LoadingService } from '../loading';
import { CatalogService } from '../share/catalog-service';
import { IExternalProfile } from '../share/models/external-profile.model';

@Injectable({
    providedIn: 'root'
})
export class BattleService {
    scene?: BattleScene;

    constructor(private loadingService: LoadingService, private battleHubService: BattleHubService,
                private profileService: ProfileService, private catalogService: CatalogService) {
        battleHubService.addNewListener(BATTLE_START_GAME, (synchronizer: ISynchronizer) => this.setupBattle(synchronizer));
        battleHubService.addNewListener(BATTLE_END_GAME, (synchronizer: ISynchronizer) =>
            this.processIncomingMessage(synchronizer, BattleChangeInstructionAction.EndGame));
        battleHubService.addNewListener(BATTLE_ATTACK, (synchronizer: ISynchronizer) =>
            this.processIncomingMessage(synchronizer, BattleChangeInstructionAction.Attack));
        battleHubService.addNewListener(BATTLE_MOVE, (synchronizer: ISynchronizer) =>
            this.processIncomingMessage(synchronizer, BattleChangeInstructionAction.Move));
        battleHubService.addNewListener(BATTLE_CAST, (synchronizer: ISynchronizer) =>
            this.processIncomingMessage(synchronizer, BattleChangeInstructionAction.Cast));
        battleHubService.addNewListener(BATTLE_WAIT, (synchronizer: ISynchronizer) =>
            this.processIncomingMessage(synchronizer, BattleChangeInstructionAction.Wait));
        battleHubService.addNewListener(BATTLE_END_TURN, (synchronizer: ISynchronizer) =>
            this.processIncomingMessage(synchronizer, BattleChangeInstructionAction.EndTurn));
        battleHubService.addNewListener(BATTLE_SKIP_TURN, (synchronizer: ISynchronizer) =>
            this.processIncomingMessage(synchronizer, BattleChangeInstructionAction.SkipTurn));
        battleHubService.addNewListener(BATTLE_DECORATION, (synchronizer: ISynchronizer) =>
            this.processIncomingMessage(synchronizer, BattleChangeInstructionAction.Decoration));
        battleHubService.addNewListener(BATTLE_NO_ACTORS_DRAW, (synchronizer: ISynchronizer) =>
            this.processIncomingMessage(synchronizer, BattleChangeInstructionAction.NoActorsDraw));
    }

    private removeScene() {
        if (this.scene) {
            this.scene.dispose();
            this.scene = null;
        }
    }

    syncWithFullSynchronization() {
        this.loadingService.loadingStart('Synchronization...', 1);
        this.scene.pause = true;
        this.scene.events = [];
        this.scene.tempEvent = null;
        this.profileService.getProfileStatus().subscribe((result: IExternalWrapper<IProfileStatus>) => {
        });
    }

    orderMove(targetActor: BattleActor, targetX: number, targetY: number): boolean {
        if (targetActor.orderMove(targetX, targetY)) {
            this.battleHubService.orderMove(targetActor.id, targetX, targetY, 0.5);
            return true;
        }
        return false;
    }

    orderAttack(targetActor: BattleActor, targetX: number, targetY: number): boolean {
        if (targetActor.orderAttack(targetX, targetY)) {
            this.battleHubService.orderAttack(targetActor.id, targetX, targetY, 0.5);
            return true;
        }
        return false;
    }

    orderCast(targetActor: BattleActor, targetSkill: BattleSkill, targetX: number, targetY: number): boolean {
        if (targetActor.orderCast(targetSkill, targetX, targetY)) {
            this.battleHubService.orderCast(targetActor.id, targetSkill.id, targetX, targetY, 0.5);
            return true;
        }
        return false;
    }

    orderWait(targetActor: BattleActor): boolean {
        if (targetActor.orderWait()) {
            this.battleHubService.orderWait(targetActor.id, 0.5);
            return true;
        }
        return false;
    }

    private async setupBattleAsync(sync: ISynchronizer): Promise<IExternalWrapper<ISynchronizer>> {
        const profiles: IExternalProfile[] = [];
        for (const i in sync.players) {
            if (sync.players.hasOwnProperty(i)) {
                const result = await this.profileService.getExternalProfile(sync.players[i].id).toPromise();
                if (result.statusCode !== 200) {
                    return {
                        statusCode: 500,
                        errors: [STRINGS.unexpectedError]
                    };
                }
                profiles.push(result.resObject);
            }
        }
        const nativesResult = await this.catalogService.getNatives().toPromise();
        if (nativesResult.statusCode !== 200) {
            return {
                statusCode: 500,
                errors: [STRINGS.unexpectedError]
            };
        }
        this.scene = new BattleScene(nativesResult.resObject, sync, profiles);
    }

    setupBattle(sync: ISynchronizer) {
        this.setupBattleAsync(sync).then((result: IExternalWrapper<ISynchronizer>) => {
            if (result.statusCode === 200) {
                this.scene.addEventBySynchronizer(result.resObject, BattleChangeInstructionAction.StartGame);
            } else {
                this.loadingService.loadingEnd(this.loadingService.tempVersion, result.errors[0]);
            }
        });
    }

    leaveBattle() {
        this.removeScene();
    }

    private processIncomingMessage(sync: ISynchronizer, action: BattleChangeInstructionAction) {
        if (this.scene) {
            this.scene.addEventBySynchronizer(sync, action);
        }
    }
}
