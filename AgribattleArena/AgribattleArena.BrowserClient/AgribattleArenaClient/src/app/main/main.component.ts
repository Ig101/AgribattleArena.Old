import { Component, OnInit, AfterViewInit, EventEmitter, OnDestroy } from '@angular/core';
import { LoadingService } from '../loading';
import { StartPageComponent } from './start-page.component';
import { ActivatedRoute } from '@angular/router';
import { ENVIRONMENT } from '../environment';
import { BattleHubService } from '../share/battle-hub.service';

@Component({
    selector: 'app-main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.css']
})
export class MainComponent implements AfterViewInit {

    constructor(private loadingService: LoadingService, private activatedRoute: ActivatedRoute) { }

    ngAfterViewInit() {
        const error = this.activatedRoute.snapshot.data.error;
        console.log(error);
        setTimeout(() => {
            error ? this.loadingService.loadingEnd(this.loadingService.tempVersion, error) : this.loadingService.loadingEnd();
        }, ENVIRONMENT.afterLoadingDelay);
    }
}
