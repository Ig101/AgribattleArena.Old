import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../share';
import { LoadingService } from 'src/app/loading';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { IProfile, IExternalWrapper } from 'src/app/share/models';
import { checkServiceResponseError, getServiceResponseErrorContent } from 'src/app/common';

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

    }

    logOutButtonPress() {
        const ver = this.loadingService.loadingStart('Logging out...', 0.5);
        this.authService.logout().subscribe((resObject: IExternalWrapper<any>) => {
            if (checkServiceResponseError(resObject)) {
                this.loadingService.loadingEnd(ver, getServiceResponseErrorContent(resObject));
                return;
            }
            this.logOutEmitter.emit();
        });
    }
}
