import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../share/authorization.service';
import { LoadingService } from 'src/app/loading';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { IProfile, IExternalWrapper } from 'src/app/share/models';
import { checkServiceResponseError, getServiceResponseErrorContent, IRouteLink } from 'src/app/common';
import { ENVIRONMENT } from 'src/app/environment';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['../main.component.css']
})
export class ProfileComponent implements OnInit {

    @Input() profile: IProfile;

    @Output() logOutEmitter = new EventEmitter();

    private profileForm: FormGroup;

    constructor(private authService: AuthService, private loadingService: LoadingService, private router: Router) {

    }

    ngOnInit() {
        this.profileForm = new FormGroup({ });
    }

    playButtonPress() {
        this.loadingService.loadingStart('Loading...', 1, {route: '/hub', router: this.router} as IRouteLink);
    }

    logOutButtonPress() {
        const ver = this.loadingService.loadingStart('Logging out...', ENVIRONMENT.defaultLoadingOpacity);
        this.authService.logout().subscribe((resObject: IExternalWrapper<any>) => {
            if (checkServiceResponseError(resObject)) {
                this.loadingService.loadingError(getServiceResponseErrorContent(resObject), ENVIRONMENT.defaultLoadingOpacity);
            } else {
                this.loadingService.loadingEnd(ver);
                this.logOutEmitter.emit();
            }
        });
    }
}
