import { Injectable, EventEmitter } from '@angular/core';
import { Subscription, Observable, Subject } from 'rxjs';
import { IExternalWrapper, ISynchronizer } from './models';
import { STRINGS, ENVIRONMENT } from '../environment';
import * as signalR from '@aspnet/signalr';
import { LoadingService } from '../loading';
import { QueueService } from './queue.service';

export const BATTLE_PREPARE = 'BattlePrepare';
export const BATTLE_SYNC_ERROR = 'BattleSynchronizationError';
export const BATTLE_START_GAME = 'BattleStartGame';
export const BATTLE_MOVE = 'BattleMove';
export const BATTLE_ATTACK = 'BattleAttack';
export const BATTLE_CAST = 'BattleCast';
export const BATTLE_WAIT = 'BattleWait';
export const BATTLE_DECORATION = 'BattleDecoration';
export const BATTLE_END_TURN = 'BattleEndTurn';
export const BATTLE_END_GAME = 'BattleEndGame';
export const BATTLE_SKIP_TURN = 'BattleSkipTurn';
export const BATTLE_NO_ACTORS_DRAW = 'BattleNoActorsDraw';

export type BattleHubReturnMethod = typeof BATTLE_PREPARE | typeof BATTLE_ATTACK | typeof BATTLE_CAST | typeof BATTLE_DECORATION |
    typeof BATTLE_END_GAME | typeof BATTLE_END_TURN | typeof BATTLE_MOVE | typeof BATTLE_NO_ACTORS_DRAW |
    typeof BATTLE_SKIP_TURN | typeof BATTLE_START_GAME | typeof BATTLE_SYNC_ERROR | typeof BATTLE_WAIT;

export type BattleHubSendMethod = never;

@Injectable({
    providedIn: 'root'
})
export class BattleHubService {

    constructor(private loagingService: LoadingService) {
        this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(ENVIRONMENT.battleHubConnectionEndpoint)
        .build();
     }

    private hubConnection: signalR.HubConnection;

    connect(): Observable<IExternalWrapper<any>> {
        const subject = new Subject<IExternalWrapper<any>>();
        this.hubConnection
        .start()
        .then(() => {
            subject.next({
                statusCode: 200
            } as IExternalWrapper<any>);
            this.addBattleListeners();
            subject.complete();
        })
        .catch(() => {
            subject.next({
                statusCode: 500,
                errors: [STRINGS.hubUnexpectedError]
            } as IExternalWrapper<any>);
            subject.complete();
        });
        return subject;
    }

    disconnect(): any {
        this.hubConnection.stop();
    }

    private catchHubError(error: any, errorScreenOpaque: number) {
        console.log(error);
        this.loagingService.loadingError(STRINGS.hubUnexpectedError, errorScreenOpaque);
    }

    addNewListener(methodName: BattleHubReturnMethod, listener: (synchronizer: ISynchronizer) => void) {
        this.hubConnection.on(methodName, listener);
    }

    orderAttack(actorId: number, targetX: number, targetY: number, errorScreenOpaque: number = 0.5) {
        this.hubConnection.invoke('OrderAttack', actorId, targetX, targetY).catch(err => this.catchHubError(err, errorScreenOpaque));
    }

    orderMove(actorId: number, targetX: number, targetY: number, errorScreenOpaque: number = 0.5) {
        this.hubConnection.invoke('OrderMove', actorId, targetX, targetY).catch(err => this.catchHubError(err, errorScreenOpaque));
    }

    orderCast(actorId: number, skillId: number, targetX: number, targetY: number, errorScreenOpaque: number = 0.5) {
        this.hubConnection.invoke('OrderCast', actorId, skillId, targetX, targetY).catch(err => this.catchHubError(err, errorScreenOpaque));
    }

    orderWait(actorId: number, errorScreenOpaque: number = 0.5) {
        this.hubConnection.invoke('OrderWait', actorId).catch(err => this.catchHubError(err, errorScreenOpaque));
    }

    // TODO
    private prepareListener() {
        console.log('BattlePrepare');
    }

    private syncErrorListener() {
        console.log('SynchronizationError');
    }

    private startGameListener(synchronizer: ISynchronizer) {
        console.log({action: 'StartGame', sync: synchronizer});
    }

    private moveListener(synchronizer: ISynchronizer) {
        console.log({action: 'Move', sync: synchronizer});
    }

    private attackListener(synchronizer: ISynchronizer) {
        console.log({action: 'Attack', sync: synchronizer});
    }

    private castListener(synchronizer: ISynchronizer) {
        console.log({action: 'Cast', sync: synchronizer});
    }

    private waitListener(synchronizer: ISynchronizer) {
        console.log({action: 'Wait', sync: synchronizer});
    }

    private decorationListener(synchronizer: ISynchronizer) {
        console.log({action: 'Decoration', sync: synchronizer});
    }

    private endTurnListener(synchronizer: ISynchronizer) {
        console.log({action: 'EndTurn', sync: synchronizer});
    }

    private endGameListener(synchronizer: ISynchronizer) {
        console.log({action: 'EndGame', sync: synchronizer});
    }

    private skipTurnListener(synchronizer: ISynchronizer) {
        console.log({action: 'SkipTurn', sync: synchronizer});
    }

    private noActorsDrawListener(synchronizer: ISynchronizer) {
        console.log({action: 'NoActorsDraw', sync: synchronizer});
    }

    private  addBattleListeners() {
        this.addNewListener(BATTLE_PREPARE, () => this.prepareListener());
        this.addNewListener(BATTLE_SYNC_ERROR, () => this.syncErrorListener());
        this.addNewListener(BATTLE_START_GAME, (synchronizer: ISynchronizer) => this.startGameListener(synchronizer));
        this.addNewListener(BATTLE_MOVE, (synchronizer: ISynchronizer) => this.moveListener(synchronizer));
        this.addNewListener(BATTLE_ATTACK, (synchronizer: ISynchronizer) => this.attackListener(synchronizer));
        this.addNewListener(BATTLE_CAST, (synchronizer: ISynchronizer) => this.castListener(synchronizer));
        this.addNewListener(BATTLE_WAIT, (synchronizer: ISynchronizer) => this.waitListener(synchronizer));
        this.addNewListener(BATTLE_DECORATION, (synchronizer: ISynchronizer) => this.decorationListener(synchronizer));
        this.addNewListener(BATTLE_END_TURN, (synchronizer: ISynchronizer) => this.endTurnListener(synchronizer));
        this.addNewListener(BATTLE_END_GAME, (synchronizer: ISynchronizer) => this.endGameListener(synchronizer));
        this.addNewListener(BATTLE_SKIP_TURN, (synchronizer: ISynchronizer) => this.skipTurnListener(synchronizer));
        this.addNewListener(BATTLE_NO_ACTORS_DRAW, (synchronizer: ISynchronizer) => this.noActorsDrawListener(synchronizer));
    }
}
