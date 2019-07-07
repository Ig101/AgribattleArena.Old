import { Injectable } from '@angular/core';
import { Subscription, Observable, Subject } from 'rxjs';
import { IExternalWrapper, ISynchronizer } from './models';
import { STRINGS, ENVIRONMENT } from '../environment';
import * as signalR from '@aspnet/signalr';
import { LoadingService } from '../loading';
import { QueueService } from './queue.service';

@Injectable({
    providedIn: 'root'
})
export class GameHubService {

    connectionState: boolean;

    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(ENVIRONMENT.gameHubConnectionEndpoint)
        .build();
        this.connectionState = false;
        this.hubConnection.onclose((error?: Error) => this.connectionState = false);
     }

    private hubConnection: signalR.HubConnection;

    connect(): Observable<IExternalWrapper<any>> {
        const subject = new Subject<IExternalWrapper<any>>();
        this.connectionState = true;
        this.hubConnection
        .start()
        .then(() => {
            setTimeout(() => {
                if (this.connectionState) {
                    subject.next({
                        statusCode: 200
                    } as IExternalWrapper<any>);
                } else {
                    subject.next({
                        statusCode: 400,
                        errors: [STRINGS.hubDoubleConnectionError]
                    } as IExternalWrapper<any>);
                }
                subject.complete();
            }, 100);
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
        this.connectionState = false;
        this.hubConnection.stop();
    }

    addNewListener(methodName: string, listener: (...args: any[]) => void) {
        this.hubConnection.on(methodName, listener);
    }
}
