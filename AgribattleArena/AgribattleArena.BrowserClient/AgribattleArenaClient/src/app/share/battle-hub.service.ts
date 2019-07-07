import { Injectable } from '@angular/core';
import { Subscription, Observable, Subject } from 'rxjs';
import { IExternalWrapper, ISynchronizer } from './models';
import { STRINGS, ENVIRONMENT } from '../environment';
import * as signalR from '@aspnet/signalr';

@Injectable({
    providedIn: 'root'
})
export class BattleHubService {

    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(ENVIRONMENT.hubConnectionEndpoint)
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
            } as IExternalWrapper<Subscription>);
            this.addBattleListeners();
            subject.complete();
        })
        .catch(() => {
            subject.next({
                statusCode: 500,
                errors: [STRINGS.hubUnexpectedError]
            } as IExternalWrapper<Subscription>);
            subject.complete();
        });
        return subject;
    }

    disconnect(): any {
        this.hubConnection.stop();
    }

    addNewListener(methodName: string, listener: (...args: any[]) => void) {
        this.hubConnection.on(methodName, listener);
    }

    startGameListener(synchronizer: ISynchronizer) {
        console.log({action: 'StartGame', sync: synchronizer});
    }

    moveListener(synchronizer: ISynchronizer) {
        console.log({action: 'Move', sync: synchronizer});
    }

    attackListener(synchronizer: ISynchronizer) {
        console.log({action: 'Attack', sync: synchronizer});
    }

    castListener(synchronizer: ISynchronizer) {
        console.log({action: 'Cast', sync: synchronizer});
    }

    waitListener(synchronizer: ISynchronizer) {
        console.log({action: 'Wait', sync: synchronizer});
    }

    decorationListener(synchronizer: ISynchronizer) {
        console.log({action: 'Decoration', sync: synchronizer});
    }

    endTurnListener(synchronizer: ISynchronizer) {
        console.log({action: 'EndTurn', sync: synchronizer});
    }

    endGameListener(synchronizer: ISynchronizer) {
        console.log({action: 'EndGame', sync: synchronizer});
    }

    skipTurnListener(synchronizer: ISynchronizer) {
        console.log({action: 'SkipTurn', sync: synchronizer});
    }

    noActorsDrawListener(synchronizer: ISynchronizer) {
        console.log({action: 'NoActorsDraw', sync: synchronizer});
    }

    addBattleListeners() {
        this.addNewListener('BattlePrepare', () => console.log('BattlePrepare'));
        this.addNewListener('BattleSynchronizationError', () => console.log('SynchronizationError'));
        this.addNewListener('BattleStartGame', (synchronizer: ISynchronizer) => this.startGameListener(synchronizer));
        this.addNewListener('BattleMove', (synchronizer: ISynchronizer) => this.moveListener(synchronizer));
        this.addNewListener('BattleAttack', (synchronizer: ISynchronizer) => this.attackListener(synchronizer));
        this.addNewListener('BattleCast', (synchronizer: ISynchronizer) => this.castListener(synchronizer));
        this.addNewListener('BattleWait', (synchronizer: ISynchronizer) => this.waitListener(synchronizer));
        this.addNewListener('BattleDecoration', (synchronizer: ISynchronizer) => this.decorationListener(synchronizer));
        this.addNewListener('BattleEndTurn', (synchronizer: ISynchronizer) => this.endTurnListener(synchronizer));
        this.addNewListener('BattleEndGame', (synchronizer: ISynchronizer) => this.endGameListener(synchronizer));
        this.addNewListener('BattleSkipTurn', (synchronizer: ISynchronizer) => this.skipTurnListener(synchronizer));
        this.addNewListener('BattleNoActorsDraw', (synchronizer: ISynchronizer) => this.noActorsDrawListener(synchronizer));
    }
}
