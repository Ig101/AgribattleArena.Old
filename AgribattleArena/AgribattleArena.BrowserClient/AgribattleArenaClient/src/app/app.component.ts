import { Component } from '@angular/core';
import { LoadingService, ILoadingModel } from './loading';

@Component({
    selector: 'app-root',
    template: `
        <router-outlet>
        </router-outlet>
        <app-loading>
        </app-loading>
    `,
    styles: []
})
export class AppComponent {
}
