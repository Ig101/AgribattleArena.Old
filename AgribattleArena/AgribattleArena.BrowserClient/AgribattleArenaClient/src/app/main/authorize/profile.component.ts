import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../share/authorization.service';
import { LoadingService } from 'src/app/loading';
import { IProfile, IExternalWrapper } from 'src/app/share/models';
import { checkServiceResponseError, getServiceResponseErrorContent, IRouteLink } from 'src/app/common';
import { ENVIRONMENT } from 'src/app/environment';
import { QueueService } from 'src/app/share/queue.service';
import { BattleHubService } from 'src/app/share/battle-hub.service';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['../main.component.css']
})
export class ProfileComponent {

    @Input() profile: IProfile;

    @Output() logOutEmitter = new EventEmitter();

    constructor(private authService: AuthService, private loadingService: LoadingService, private queueService: QueueService) {

    }

    logOutButtonPress() {
        const ver = this.loadingService.loadingStart('Logging out...', ENVIRONMENT.defaultLoadingOpacity);
        this.authService.logout().subscribe((resObject: IExternalWrapper<any>) => {
            if (checkServiceResponseError(resObject)) {
                this.loadingService.loadingError(getServiceResponseErrorContent(resObject), ENVIRONMENT.defaultLoadingOpacity);
            } else {
                this.queueService.dequeue();
                this.loadingService.loadingEnd(ver);
                this.logOutEmitter.emit();
            }
        });
    }
}
