import { Injectable } from '@angular/core';
import { QueueService } from './queue.service';
import { BattleHubService } from './battle-hub.service';
import { Subscription, Observable, Subject } from 'rxjs';
import { IExternalWrapper } from './models';

@Injectable({
    providedIn: 'root'
})
export class BattlePreparationService {

    constructor(private queueService: QueueService, private battleHubService: BattleHubService) { }

    toBattle(): Observable<IExternalWrapper<Subscription>> {
        const subject = new Subject<IExternalWrapper<Subscription>>();
        this.queueService.enqueue()
            .subscribe((enqueueResult: IExternalWrapper<any>) => {
                if (enqueueResult.statusCode === 200) {
                    this.battleHubService.connect()
                        .subscribe((connectionResult: IExternalWrapper<Subscription>) => {
                            subject.next(connectionResult);
                            subject.complete();
                        });
                } else {
                    subject.next(enqueueResult);
                    subject.complete();
                }
            });
        return subject;
    }

    leaveQueue() {
        const subject = new Subject<IExternalWrapper<Subscription>>();
        this.queueService.dequeue()
            .subscribe((enqueueResult: IExternalWrapper<any>) => {
                if (enqueueResult.statusCode === 200) {
                    this.battleHubService.disconnect()
                        .subscribe((connectionResult: IExternalWrapper<any>) => {
                            subject.next(connectionResult);
                            subject.complete();
                        });
                } else {
                    subject.next(enqueueResult);
                    subject.complete();
                }
            });
        return subject;
    }
}
