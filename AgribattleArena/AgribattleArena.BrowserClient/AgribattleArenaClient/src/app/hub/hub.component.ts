import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { LoadingService } from '../loading';
import { ActivatedRoute } from '@angular/router';
import { ENVIRONMENT } from '../environment';
import { BattleHubService } from '../share/battle-hub.service';

@Component({
    selector: 'app-hub',
    templateUrl: './hub.component.html',
    styleUrls: ['./hub.component.css']
})
export class HubComponent implements OnInit, AfterViewInit, OnDestroy {

    constructor(private loadingService: LoadingService, private route: ActivatedRoute, private battleHubService: BattleHubService) { }

    ngOnInit() {

    }

    ngAfterViewInit() {
        setTimeout(() => this.loadingService.loadingEnd(), ENVIRONMENT.afterLoadingDelay);
    }

    ngOnDestroy() {
        this.battleHubService.disconnect();
    }
}
