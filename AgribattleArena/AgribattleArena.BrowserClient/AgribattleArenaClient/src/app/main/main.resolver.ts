import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import {map, timeout, catchError} from 'rxjs/operators';
import { LoadingService } from '../loading';
import { ProfileService } from '../share/profile.service';
import { Subscription, of, Subject } from 'rxjs';
import { IExternalWrapper, IProfile } from '../share/models';
import { ENVIRONMENT } from '../environment';
import { GameHubService } from '../share/game-hub.service';
import { Store } from '@ngrx/store';
import * as stateStore from '../share/storage/reducers';
import * as profileAction from '../share/storage/actions/profile';

@Injectable()
export class MainResolver implements Resolve<any> {
    constructor(private profileService: ProfileService, private gameHubService: GameHubService, private store: Store<stateStore.State>) {

    }
    resolve() {
        const subject = new Subject<string>();
        this.profileService.getProfile()
            .pipe(
                timeout(ENVIRONMENT.startLoadingTimeout),
                catchError(error => {
                    return of({
                        statusCode: 500
                    } as IExternalWrapper<IProfile>);
                }))
            .subscribe((resObject: IExternalWrapper<IProfile>) => {
                if (resObject.statusCode === 200) {
                    this.gameHubService.connect().subscribe((hubResult: IExternalWrapper<any>) => {
                        if (hubResult.statusCode !== 200) {
                            this.store.dispatch(new profileAction.ChangeAuthorized(false));
                            subject.next(hubResult.errors[0]);
                            subject.complete();
                        }
                        subject.next(null);
                        subject.complete();
                    });
                } else {
                    subject.next(null);
                    subject.complete();
                }
            });
        return subject;
    }
}
