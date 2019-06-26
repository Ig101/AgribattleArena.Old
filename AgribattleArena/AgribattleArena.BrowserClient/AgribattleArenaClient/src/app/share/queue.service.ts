import { Injectable, OnInit } from '@angular/core';
import { IExternalWrapper } from './models';
import { Subject, Observable, of, Subscription } from 'rxjs';
import { ErrorHandleHelper } from '../common/helpers/error-handle.helper';
import { HttpErrorResponse, HttpClient } from '@angular/common/http';
import { LoadingService } from '../loading';
import { ENVIRONMENT, STRINGS } from '../environment';
import { map, catchError } from 'rxjs/operators';
import { BattleHubService } from './battle-hub.service';

@Injectable({
    providedIn: 'root'
})
export class QueueService {

    inQueue = false;
    processingQueueRequest = false;
    private inQueueServerSide = false;
    timePassed: number;

    constructor(private loadingService: LoadingService, private http: HttpClient, private battleHub: BattleHubService) { }

    private timerTick() {
        if (this.inQueueServerSide || this.inQueue) {
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
        this.inQueue = this.inQueueServerSide;
    }

    private errorHandler(errorResult: HttpErrorResponse, queueService: QueueService) {
        let errorMessage: string;
        switch (errorResult.status) {
            default:
                errorMessage = STRINGS.queueUnexpectedError;
                break;
        }
        queueService.returnOldValue();
        return of({
            statusCode: errorResult.status,
            errors: [errorMessage]
        });
    }

    enqueue(): Observable<IExternalWrapper<any>> {
        this.processingQueueRequest = true;
        this.setQueue(true);
        const subject = new Subject<IExternalWrapper<any>>();
        this.battleHub.connect().subscribe((hubResult: IExternalWrapper<any>) => {
            if (hubResult.statusCode === 200) {
                this.http.post('/api/queue', {mode: 'main_duel'})
                .pipe(map((result: any) => {
                    return {
                        statusCode: 200,
                        resObject: result
                    } as IExternalWrapper<any>;
                }))
                .pipe(catchError((error) => this.errorHandler(error, this)))
                .subscribe((queueResult: IExternalWrapper<any>) => {
                    if (queueResult.statusCode === 200) {
                        this.inQueueServerSide = true;
                    } else {
                        this.battleHub.disconnect();
                    }
                    subject.next(queueResult);
                    this.processingQueueRequest = false;
                    subject.complete();
                });
            } else {
                this.returnOldValue();
                subject.next(hubResult);
                this.processingQueueRequest = false;
                subject.complete();
            }
        });
        return subject;
    }

    dequeue(): any {
        this.setQueue(false);
        this.inQueueServerSide = false;
        this.battleHub.disconnect();
    }
}
