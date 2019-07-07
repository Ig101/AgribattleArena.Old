import { Injectable } from '@angular/core';
import { Subscription, Observable, Subject } from 'rxjs';
import { IExternalWrapper, ISynchronizer } from './models';
import { STRINGS, ENVIRONMENT } from '../environment';
import * as signalR from '@aspnet/signalr';
import { HubComponent } from '../hub/hub.component';

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

    addBattleListeners() {
        this.addNewListener('BattleStartGame', (synchronizer: ISynchronizer) => console.log(synchronizer));
    }
}
