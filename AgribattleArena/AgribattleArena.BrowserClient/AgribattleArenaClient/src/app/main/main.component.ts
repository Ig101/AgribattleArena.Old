import { Component, OnInit, AfterViewInit, EventEmitter } from '@angular/core';
import { LoadingService } from '../loading';
import { StartPageComponent } from './start-page.component';
import { ActivatedRoute } from '@angular/router';
import { ENVIRONMENT } from '../environment';

@Component({
    selector: 'app-main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit, AfterViewInit {

    loginAction = new EventEmitter();

    constructor(private loadingService: LoadingService, private route: ActivatedRoute) { }

    ngOnInit() {
        const profile = this.route.snapshot.data.profile;
        if (profile) {
            this.loginAction.emit(profile);
        }
    }

    ngAfterViewInit() {
        setTimeout(() => this.loadingService.loadingEnd(), ENVIRONMENT.afterLoadingDelay);
    }
}
