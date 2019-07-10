import { Injectable, ErrorHandler } from '@angular/core';
import { IProfile, IExternalWrapper, IProfileStatus } from './models';
import { Subject, Observable, of } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { getParseErrors } from '@angular/compiler';
import { ErrorHandleHelper } from '../common/helpers/error-handle.helper';
import { STRINGS, ENVIRONMENT } from '../environment';
import { Store } from '@ngrx/store';
import * as stateStore from './storage/reducers';
import * as profileAction from './storage/actions/profile';
import { IExternalProfile } from './models/external-profile.model';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

    constructor(private http: HttpClient, private store: Store<stateStore.State>) {
    }

    private errorHandler(errorResult: HttpErrorResponse) {
        let errorMessage: string;
        switch (errorResult.status) {
            case 404:
                errorMessage = ErrorHandleHelper.getUnauthorizedError(errorResult.error);
                break;
            default:
                errorMessage = ErrorHandleHelper.getInternalServerError(errorResult.error);
                break;
        }
        return of({
            statusCode: errorResult.status,
            errors: [errorMessage]
        });
    }

    isAuthenticated(): Observable<IExternalWrapper<boolean>> {
        const subject = new Subject<IExternalWrapper<boolean>>();
        setTimeout(() => {
            subject.next({
                statusCode: 501,
                errors: [STRINGS.notImplemented]
            });
            subject.complete();
        }, 50);
        return subject;
    }

    private getProfileRequest(subject: Subject<IExternalWrapper<IProfile>>) {
        this.http.get('/api/profile')
        .pipe(map((result: IProfile) => {
            return {
                statusCode: 200,
                resObject: result
            } as IExternalWrapper<IProfile>;
        }))
        .pipe(catchError(this.errorHandler))
        .subscribe((profileResult: IExternalWrapper<IProfile>) => {
            this.store.dispatch(new profileAction.Change(profileResult.resObject));
            subject.next(profileResult);
            subject.complete();
        });
    }

    updateProfile(): Observable<IExternalWrapper<IProfile>> {
        const subject = new Subject<IExternalWrapper<IProfile>>();
        this.getProfileRequest(subject);
        return subject;
    }

    getProfile(): Observable<IExternalWrapper<IProfile>> {
        const subject = new Subject<IExternalWrapper<IProfile>>();
        this.store.select(stateStore.getAuthorized)
        .pipe(catchError(() => {
            return of(true);
        }))
        .subscribe((authorized: boolean) => {
            if (authorized) {
                this.store.select(stateStore.getProfile)
                    .pipe(catchError(() => {
                        this.getProfileRequest(subject);
                        return of(null);
                    }))
                    .subscribe((profile) => {
                    if (profile != null) {
                        subject.next({
                            statusCode: 200,
                            resObject: profile});
                        subject.complete();
                    }
                });
            } else {
                subject.next({
                    statusCode: 404,
                    errors: [STRINGS.unathorized]
                });
            }
        });
        return subject;
    }

    getExternalProfile(id: string): Observable<IExternalWrapper<IExternalProfile>> {
        const subject = new Subject<IExternalWrapper<IExternalProfile>>();
        this.http.get('/api/profile/' + id)
            .pipe(map((result: IExternalProfile) => {
                return {
                    statusCode: 200,
                    resObject: result
                } as IExternalWrapper<IExternalProfile>;
            }))
            .pipe(catchError(this.errorHandler))
            .subscribe((result: IExternalWrapper<any>) => {
                subject.next(result);
                subject.complete();
            });
        return subject;
    }

    private getProfileStatusRequest(subject: Subject<IExternalWrapper<IProfileStatus>>) {
        this.http.get('/api/profile')
        .pipe(map((result: IProfileStatus) => {
            return {
                statusCode: 200,
                resObject: result
            } as IExternalWrapper<IProfileStatus>;
        }))
        .pipe(catchError(this.errorHandler))
        .subscribe((profileResult: IExternalWrapper<IProfileStatus>) => {
            this.store.dispatch(new profileAction.ChangeStatus(profileResult.resObject));
            subject.next(profileResult);
            subject.complete();
        });
    }

    updateProfileStatus(): Observable<IExternalWrapper<IProfileStatus>> {
        const subject = new Subject<IExternalWrapper<IProfileStatus>>();
        this.getProfileStatusRequest(subject);
        return subject;
    }

    getProfileStatus(): Observable<IExternalWrapper<IProfileStatus>> {
        const subject = new Subject<IExternalWrapper<IProfileStatus>>();
        this.store.select(stateStore.getAuthorized)
        .pipe(catchError(() => {
            return of(true);
        }))
        .subscribe((authorized: boolean) => {
            if (authorized) {
                this.store.select(stateStore.getProfileStatus)
                .pipe(catchError(() => {
                    this.getProfileStatusRequest(subject);
                    return of(null);
                }))
                .subscribe((profileStatus) => {
                    if (profileStatus != null) {
                        subject.next({
                            statusCode: 200,
                            resObject: profileStatus});
                        subject.complete();
                    }
                });
            } else {
                subject.next({
                    statusCode: 404,
                    errors: [STRINGS.unathorized]
                });
            }
        });
        return subject;
    }
}
