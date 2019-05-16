import { Component } from '@angular/core';
import { AuthorizeSwitchEnum } from './authorize';

@Component({
    selector: 'app-start',
    templateUrl: './start-page.component.html',
    styleUrls: ['./main.component.css']
})
export class StartPageComponent {
    private authorizeSwitchEnum = AuthorizeSwitchEnum;
    public authorizeSwitch: AuthorizeSwitchEnum;

    constructor() {
        this.authorizeSwitch = AuthorizeSwitchEnum.Login;
    }

    goToRegister(){
        this.authorizeSwitch = AuthorizeSwitchEnum.Register;
    }

    goToLogin(){
        this.authorizeSwitch = AuthorizeSwitchEnum.Login;
    }

    goToProfile(){
        this.authorizeSwitch = AuthorizeSwitchEnum.Profile;
    }
}
