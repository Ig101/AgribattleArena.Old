import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { LoadingService } from '../loading';
import { ProfileService } from '../share/profile.service';
import { IExternalWrapper, IProfile } from '../share/models';
import { StartPageComponent } from './start-page.component';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit, AfterViewInit {

    @ViewChild(StartPageComponent) startPage: StartPageComponent;

    constructor(private loadingService: LoadingService, private profileService: ProfileService) { }

    ngOnInit() { }

    ngAfterViewInit() {
        const subscription: Subscription = this.profileService.getProfile().subscribe((resObject: IExternalWrapper<IProfile>) => {
            if (resObject.statusCode === 200) {
                this.startPage.login(resObject.resObject);
            }
            this.loadingService.loadingEnd(this.loadingService.tempVersion);
        });
        setTimeout(() => {
            subscription.unsubscribe();
            this.loadingService.loadingEnd(this.loadingService.tempVersion);
        }, 5000);
    }
}
