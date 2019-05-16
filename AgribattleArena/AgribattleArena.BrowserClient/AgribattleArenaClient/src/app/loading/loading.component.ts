import { Component, OnInit, OnDestroy, Output } from '@angular/core';
import { ILoadingModel } from './loading.model';
import { LoadingService } from './loading.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-loading',
    templateUrl: './loading.component.html',
    styleUrls: ['./loading.component.css']
})
export class LoadingComponent implements OnInit, OnDestroy {

    loading = 1;
    loadingMessage = 'Loading...';
    loadingOpaque = 1;
    loadingState: Subscription;

    constructor(private loadingService: LoadingService) {

    }

    ngOnInit() {
        this.loadingState = this.loadingService.getState().subscribe((value) => {
            this.loadingMessage = value.message;
            this.loading = value.loading;
            this.loadingOpaque = value.opaque;
        });
    }

    ngOnDestroy() {
        this.loadingState.unsubscribe();
    }
}
