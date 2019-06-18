import { Component, OnInit } from '@angular/core';
import { AuthorizeSwitchEnum, LoginComponent } from './authorize';
import { IProfile } from '../share/models';
import { MainComponent } from './main.component';
import { IParentActionComponent } from '../common/interfaces/parent-action-component.interface';
import { ParentEventEmitterHelper } from '../common/helpers/parent-event-emitter.helper';
import { ProfileService } from '../share/profile.service';
import { LoadingService } from '../loading';

@Component({
    selector: 'app-start',
    templateUrl: './start-page.component.html',
    styleUrls: ['./main.component.css']
})
export class StartPageComponent implements OnInit, IParentActionComponent {
    private authorizeSwitchEnum = AuthorizeSwitchEnum;
    public authorizeSwitch: AuthorizeSwitchEnum;
    profile: IProfile;

    constructor(private profileService: ProfileService, private loadingService: LoadingService) { }

    ngOnInit() {
        if (this.profileService.tempProfile) {
            this.login(this.profileService.tempProfile);
        } else {
            this.authorizeSwitch = AuthorizeSwitchEnum.Login;
        }
    }

    goToRegister() {
        this.authorizeSwitch = AuthorizeSwitchEnum.Register;
    }

    goToLogin() {
        this.authorizeSwitch = AuthorizeSwitchEnum.Login;
    }

    goToProfile() {
        this.authorizeSwitch = AuthorizeSwitchEnum.Profile;
    }

    goToForgotPassword() {
        this.loadingService.loadingError('Not implemented. Coming soon', 0.5);
    }

    login(profile: IProfile) {
        this.profile = profile;
        this.goToProfile();
    }

    parentAction(profile: any) {
        this.login(profile as IProfile);
    }
}
