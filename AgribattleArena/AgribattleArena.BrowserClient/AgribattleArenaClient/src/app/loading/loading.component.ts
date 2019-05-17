import { Component, OnInit, OnDestroy, Output, ElementRef, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoadingStatusEnum } from './loading-status.enum';
import { LoadingService } from './loading.service';

@Component({
    selector: 'app-loading',
    templateUrl: './loading.component.html',
    styleUrls: ['./loading.component.css']
})
export class LoadingComponent implements OnInit, OnDestroy {

    @ViewChild('okButton') okButton: ElementRef;

    private loadingStatusEnum = LoadingStatusEnum;
    private loadingStatus: LoadingStatusEnum = LoadingStatusEnum.Loading;
    private loading = 1;
    private loadingMessage = 'Loading...';
    private loadingOpaque = 1;

    loadingState: Subscription;

    constructor(private loadingService: LoadingService) {

    }

    changeLoadingValue(value: LoadingStatusEnum) {
        if (value === LoadingStatusEnum.Error && this.loading > 0) {
            setTimeout(() => this.okButton.nativeElement.focus());
        }
        this.loadingStatus = value;
    }

    ngOnInit() {
        this.loadingState = this.loadingService.getState().subscribe((value) => {
            this.loadingMessage = value.message;
            this.loading = value.loading;
            this.loadingOpaque = value.opaque;
            this.changeLoadingValue(value.loadingStatus);
        });
    }

    ngOnDestroy() {
        this.loadingState.unsubscribe();
    }

    errorOkButtonPress() {
        this.loadingService.loadingEnd();
    }
}
