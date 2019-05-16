import { Injectable } from '@angular/core';
import { ILoadingModel } from './loading.model';
import { Subject } from 'rxjs';

@Injectable()
export class LoadingService {

    speed = 0.1;

    private loadingStateSubject: Subject<ILoadingModel> = new Subject<ILoadingModel>();
    private loadingState: ILoadingModel = {
        loading: 1,
        message: 'Loading...',
        opaque: 1
    };

    loadingStart(incomingMessage: string, incomingOpaque: number) {
        this.loadingState = {
            loading: this.loadingState.loading,
            message: incomingMessage,
            opaque: incomingOpaque
        };
        this.loadingAnimation(true);
    }

    getState() {
        return this.loadingStateSubject;
    }

    loadingEnd() {
        this.loadingAnimation(false);
    }

    private loadingAnimation(side: boolean) {
        const newLoading = this.loadingState.loading + this.speed * (side ? 1 : -1);
        const newMessage = this.loadingState.message;
        const newOpaque = this.loadingState.opaque;

        this.loadingState = {
            loading: newLoading,
            message: newMessage,
            opaque: newOpaque
        };

        let end = false;
        if ((side && newLoading >= 1) || (!side && newLoading <= 0)) {
            this.loadingState.loading = side ? 1 : 0;
            end = true;
        }
        this.loadingStateSubject.next(this.loadingState);
        if (!end) {
            setTimeout(() => {
                this.loadingAnimation(side);
            }, 20);
        }
    }

    loadingStatus() {

    }
}
