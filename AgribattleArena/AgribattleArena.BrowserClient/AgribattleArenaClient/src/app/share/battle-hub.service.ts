import { Injectable } from '@angular/core';
import { Subscription, Observable, Subject } from 'rxjs';
import { IExternalWrapper } from './models';

@Injectable({
    providedIn: 'root'
})
export class BattleHubService {

    constructor() { }

    connect(): Observable<IExternalWrapper<Subscription>> {
        const subject = new Subject<IExternalWrapper<any>>();
        setTimeout(() => {
            subject.next({
                statusCode: 501,
                errors: ['Not implemented']
            });
            subject.complete();
        }, 50);
        return subject;
    }

    subscribe(): Observable<IExternalWrapper<Subscription>> {
        const subject = new Subject<IExternalWrapper<any>>();
        setTimeout(() => {
            subject.next({
                statusCode: 501,
                errors: ['Not implemented']
            });
            subject.complete();
        }, 50);
        return subject;
    }

    disconnect(): Observable<IExternalWrapper<any>> {
        const subject = new Subject<IExternalWrapper<any>>();
        setTimeout(() => {
            subject.next({
                statusCode: 501,
                errors: ['Not implemented']
            });
            subject.complete();
        }, 50);
        return subject;
    }
}
