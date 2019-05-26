import { Component, OnInit, AfterViewInit } from '@angular/core';
import { LoadingService } from '../loading';
import { ActivatedRoute } from '@angular/router';
import { ENVIRONMENT } from '../environment';

@Component({
    selector: 'app-hub',
    templateUrl: './hub.component.html',
    styleUrls: ['./hub.component.css']
})
export class HubComponent implements OnInit, AfterViewInit {

    constructor(private loadingService: LoadingService, private route: ActivatedRoute) { }

    ngOnInit() {

    }

    ngAfterViewInit() {
        setTimeout(() => this.loadingService.loadingEnd(), ENVIRONMENT.afterLoadingDelay);
    }
}
