import { Injectable, OnInit } from '@angular/core';
import { IExternalWrapper } from './models';
import { Subject, Observable, of } from 'rxjs';
import { ErrorHandleHelper } from '../common/helpers/error-handle.helper';
import { HttpErrorResponse } from '@angular/common/http';
import { LoadingService } from '../loading';
import { ENVIRONMENT, STRINGS } from '../environment';

@Injectable({
    providedIn: 'root'
})
export class QueueService {

    inQueue = false;
    private inQueueServerSide = false;
    timePassed: number;

    constructor(private loadingService: LoadingService) { }

    private timerTick() {
        if (this.inQueue) {
            this.timePassed += 1000;
            if (this.timePassed >= ENVIRONMENT.queueTimeout) {
                this.dequeue();
                this.loadingService.loadingError(STRINGS.queueTimeout, 0.5);
            }
            setTimeout(() => this.timerTick(), 1000);
        }
    }

    private setQueue(inQueue: boolean) {
        this.inQueue = inQueue;
        if (this.inQueue) {
            this.timePassed = -1000;
            this.timerTick();
        }
    }

    private returnOldValue() {
        this.setQueue(this.inQueueServerSide);
    }

    private errorHandler(errorResult: HttpErrorResponse) {
        let errorMessage: string;
        switch (errorResult.status) {
            case 404:
                errorMessage = STRINGS.queueUnexpectedError;
                break;
            default:
                errorMessage = STRINGS.queueUnexpectedError;
                break;
        }
        this.returnOldValue();
        return of({
            statusCode: errorResult.status,
            errors: [errorMessage]
        });
    }

    enqueue(): Observable<IExternalWrapper<any>> {
        this.setQueue(true);
        const subject = new Subject<IExternalWrapper<any>>();
        setTimeout(() => {
            this.returnOldValue();
            subject.next({
                statusCode: 501,
                errors: [STRINGS.notImplemented]
            });
            subject.complete();
        }, 10000);
        return subject;
    }

    dequeue(): Observable<IExternalWrapper<any>> {
        this.setQueue(false);
        const subject = new Subject<IExternalWrapper<any>>();
        setTimeout(() => {
            this.returnOldValue();
            subject.next({
                statusCode: 501,
                errors: [STRINGS.notImplemented]
            });
            subject.complete();
        }, 10000);
        return subject;
    }
}
