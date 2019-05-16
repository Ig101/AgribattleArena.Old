import { Component } from '@angular/core';
import { LoadingService, ILoadingModel } from './loading';

@Component({
    selector: 'app-root',
    template: `
        <div *ngIf="loadingState.loading">Loading</div>
        <router-outlet>
        </router-outlet>
    `,
    styles: []
})
export class AppComponent {
    loadingState: ILoadingModel;

    constructor(private loadingService: LoadingService) {
        this.loadingState = loadingService.loadingState;
    }

}
