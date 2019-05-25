import { Component } from '@angular/core';
import { AuthorizeSwitchEnum } from './authorize';
import { IProfile } from '../share/models';

@Component({
    selector: 'app-start',
    templateUrl: './start-page.component.html',
    styleUrls: ['./main.component.css']
})
export class StartPageComponent {
    private authorizeSwitchEnum = AuthorizeSwitchEnum;
    public authorizeSwitch: AuthorizeSwitchEnum;
    private profile: IProfile;

    constructor() {
        this.authorizeSwitch = AuthorizeSwitchEnum.Login;
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
        alert('Coming soon');
    }

    login(profile: IProfile) {
        this.profile = profile;
    }
}
