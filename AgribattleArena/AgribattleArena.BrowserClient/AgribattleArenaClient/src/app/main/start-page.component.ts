import { Component, OnInit } from '@angular/core';
import { AuthorizeSwitchEnum, LoginComponent } from './authorize';
import { IProfile } from '../share/models';
import { MainComponent } from './main.component';
import { IParentActionComponent } from '../common/interfaces/parent-action-component.interface';
import { ParentEventEmitterHelper } from '../common/helpers/parent-event-emitter.helper';
import { ProfileService } from '../share/profile.service';
import { LoadingService } from '../loading';
import { Store } from '@ngrx/store';
import * as stateStore from '../share/storage/reducers';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
    selector: 'app-start',
    templateUrl: './start-page.component.html',
    styleUrls: ['./main.component.css']
})
export class StartPageComponent implements OnInit, IParentActionComponent {
    private authorizeSwitchEnum = AuthorizeSwitchEnum;
    public authorizeSwitch: AuthorizeSwitchEnum;
    profile: IProfile;

    constructor(private loadingService: LoadingService, private store: Store<stateStore.State>) { }

    ngOnInit() {
        this.authorizeSwitch = AuthorizeSwitchEnum.Login;
        this.store.select(stateStore.getProfile)
        .pipe(catchError(() => {
            return of(null);
        }))
        .subscribe((profile => {
            console.log('tempProfile');
            console.log(profile);
            if (profile) {
                this.login(profile);
            }
        }));
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
