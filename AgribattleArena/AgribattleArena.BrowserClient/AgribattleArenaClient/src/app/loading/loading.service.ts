import { Injectable } from '@angular/core';
import { ILoadingModel } from './loading.model';
import { Subject } from 'rxjs';

@Injectable()
export class LoadingService {

    private loadingState: Subject<ILoadingModel> = new Subject<ILoadingModel>();

    loadingStart(incomingMessage: string) {
        this.loadingState.next({
            loading: true,
            message: incomingMessage
        });
    }

    getState() {
        return this.loadingState;
    }

    loadingEnd() {
        this.loadingState.next({
            loading: false
        });
    }

    loadingStatus() {

    }
}
