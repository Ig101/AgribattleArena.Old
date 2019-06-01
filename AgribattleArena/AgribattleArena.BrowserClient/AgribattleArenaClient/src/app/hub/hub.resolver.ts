import { Injectable } from '@angular/core';
import { Resolve, Router } from '@angular/router';
import {map, timeout, catchError} from 'rxjs/operators';
import { LoadingService } from '../loading';
import { ProfileService } from '../share/profile.service';
import { Subscription, of, Subject } from 'rxjs';
import { IExternalWrapper, IProfile } from '../share/models';
import { ENVIRONMENT, STRINGS } from '../environment';

@Injectable()
export class HubResolver implements Resolve<any> {
    constructor(private profileService: ProfileService, private loadingService: LoadingService, private router: Router) {

    }
    resolve() {
        return this.profileService.getFullProfile(true)
            .pipe(
                timeout(ENVIRONMENT.startLoadingTimeout),
                catchError(error => {
                    return of({
                        statusCode: 500
                    } as IExternalWrapper<IProfile>);
                }))
            .pipe(map((resObject: IExternalWrapper<IProfile>) => {
                if (!resObject.resObject) {
                    this.loadingService.loadingChangeMessage(STRINGS.authorizationFailed);
                    this.router.navigate(['/']);
                }
                return resObject.resObject;
            }));

    }
}
