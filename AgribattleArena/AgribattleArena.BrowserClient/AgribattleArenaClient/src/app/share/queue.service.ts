import { Injectable, OnInit } from '@angular/core';
import { IExternalWrapper } from './models';
import { Subject, Observable, of } from 'rxjs';
import { ErrorHandleHelper } from '../common/helpers/error-handle.helper';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class QueueService {

    inQueue = false;
    timePassed: number;

    constructor() { }

    private timerTick() {
        if (this.inQueue) {
            this.timePassed++;
            setTimeout(() => this.timerTick(), 1000);
        }
    }

    private setQueue(inQueue: boolean) {
        if (inQueue) {
            this.timePassed = -1;
            this.timerTick();
        }
        this.inQueue = inQueue;
    }

    private errorHandler(errorResult: HttpErrorResponse) {
        let errorMessage: string;
        switch (errorResult.status) {
            case 404:
                errorMessage = ErrorHandleHelper.getUnauthorizedError(errorResult.error);
                break;
            default:
                errorMessage = ErrorHandleHelper.getInternalServerError(errorResult.error);
                break;
        }
        return of({
            statusCode: errorResult.status,
            errors: [errorMessage]
        });
    }

    enqueue(): Observable<IExternalWrapper<any>> {
        const subject = new Subject<IExternalWrapper<any>>();
        setTimeout(() => {
            this.setQueue(true);
            subject.next({
                statusCode: 501,
                errors: ['Not implemented']
            });
            subject.complete();
        }, 50);
        return subject;
    }

    dequeue(): Observable<IExternalWrapper<any>> {
        const subject = new Subject<IExternalWrapper<any>>();
        setTimeout(() => {
            this.setQueue(false);
            subject.next({
                statusCode: 501,
                errors: ['Not implemented']
            });
            subject.complete();
        }, 50);
        return subject;
    }
}
