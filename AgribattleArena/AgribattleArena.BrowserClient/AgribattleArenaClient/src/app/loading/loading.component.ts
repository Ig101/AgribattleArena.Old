import { Component, OnInit, OnDestroy, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoadingStatusEnum } from './loading-status.enum';
import { LoadingService } from './loading.service';

@Component({
    selector: 'app-loading',
    templateUrl: './loading.component.html',
    styleUrls: ['./loading.component.css']
})
export class LoadingComponent implements OnInit, OnDestroy {

    private loadingStatusEnum = LoadingStatusEnum;
    private loadingStatus: LoadingStatusEnum = LoadingStatusEnum.Loading;
    private loading = 1;
    private loadingMessage = 'Loading...';
    private loadingOpaque = 1;

    loadingState: Subscription;

    constructor(private loadingService: LoadingService) {

    }

    ngOnInit() {
        this.loadingState = this.loadingService.getState().subscribe((value) => {
            this.loadingMessage = value.message;
            this.loading = value.loading;
            this.loadingOpaque = value.opaque;
            this.loadingStatus = value.loadingStatus;
        });
    }

    ngOnDestroy() {
        this.loadingState.unsubscribe();
    }

    errorOkButtonPress() {
        this.loadingService.loadingEnd();
    }
}
