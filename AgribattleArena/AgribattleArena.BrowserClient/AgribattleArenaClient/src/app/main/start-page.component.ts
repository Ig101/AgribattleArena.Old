import { Component } from '@angular/core';
import { AuthorizeSwitchEnum, LoginComponent } from './authorize';
import { IProfile } from '../share/models';
import { MainComponent } from './main.component';
import { IParentActionComponent } from '../common/interfaces/parent-action-component.interface';
import { ParentEventEmitterHelper } from '../common/helpers/parent-event-emitter.helper';

@Component({
    selector: 'app-start',
    templateUrl: './start-page.component.html',
    styleUrls: ['./main.component.css']
})
export class StartPageComponent implements IParentActionComponent {
    private authorizeSwitchEnum = AuthorizeSwitchEnum;
    public authorizeSwitch: AuthorizeSwitchEnum;
    private profile: IProfile;

    constructor(mainComponent: MainComponent) {
        ParentEventEmitterHelper.subscribe(mainComponent.loginAction, this);
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
        this.goToProfile();
    }

    parentAction(profile: any) {
        this.login(profile as IProfile);
    }
}
