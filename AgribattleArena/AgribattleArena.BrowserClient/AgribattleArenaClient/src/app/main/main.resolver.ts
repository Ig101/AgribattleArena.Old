import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import {map, timeout, catchError} from 'rxjs/operators';
import { LoadingService } from '../loading';
import { ProfileService } from '../share/profile.service';
import { Subscription, of } from 'rxjs';
import { IExternalWrapper, IProfile } from '../share/models';
import { ENVIRONMENT } from '../environment';

@Injectable()
export class MainResolver implements Resolve<any> {
    constructor(private profileService: ProfileService) {

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
                return resObject.resObject;
            }));
    }
}
