import { Injectable } from '@angular/core';
import { ILoadingModel } from './loading.model';

@Injectable()
export class LoadingService {

    loadingState: ILoadingModel = {
        loading: false
    };

    loadingStart(message: string) {
        this.loadingState.loading = true;
        this.loadingState.message = message;
        console.log(message);
    }

    loadingEnd() {
        this.loadingState.loading = false;
    }

    loadingStatus() {

    }
}
