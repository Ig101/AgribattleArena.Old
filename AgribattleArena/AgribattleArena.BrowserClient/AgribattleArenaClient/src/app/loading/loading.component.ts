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

    loading = true;
    loadingMessage = 'Loading...';
    loadingState: Subscription;

    constructor(private loadingService: LoadingService) {

    }

    ngOnInit() {
        this.loadingState = this.loadingService.getState().subscribe((value) => {
            this.loadingMessage = value.message;
            this.loading = value.loading;
            console.log(value);
        });
    }

    ngOnDestroy() {
        this.loadingState.unsubscribe();
    }
}
