import { Component, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/share/profile.service';
import { Router } from '@angular/router';
import { LoadingService } from 'src/app/loading';
import { IRouteLink } from 'src/app/common';

@Component({
    selector: 'app-side-bar',
    templateUrl: './side-bar.component.html',
    styleUrls: ['../hub.component.css']
})
export class SideBarComponent {

    constructor(private profileService: ProfileService, private loadingService: LoadingService, private router: Router) {

    }

    settingsButtonPress() {
        alert('Coming soon');
    }

    exitButtonPress() {
        this.loadingService.loadingStart('Exiting...', 1, {
            router: this.router,
            route: '/'
        } as IRouteLink);
    }
}
