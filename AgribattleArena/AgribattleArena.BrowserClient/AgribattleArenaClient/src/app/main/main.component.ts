import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
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

    @ViewChild(StartPageComponent) startPage: StartPageComponent;

    constructor(private loadingService: LoadingService, private route: ActivatedRoute) { }

    ngOnInit() {
        const profile = this.route.snapshot.data.profile;
        if (profile) {
            this.startPage.login(profile);
        }
    }

    ngAfterViewInit() {
        setTimeout(() => this.loadingService.loadingEnd(), ENVIRONMENT.afterLoadingDelay);
    }
}
