import { Injectable, ErrorHandler } from '@angular/core';
import { IProfile, IExternalWrapper, IProfileStatus } from './models';
import { Subject, Observable, of } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { getParseErrors } from '@angular/compiler';
import { ErrorHandleHelper } from '../common/helpers/error-handle.helper';
import { STRINGS } from '../environment';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

    tempProfile: IProfile;

    constructor(private http: HttpClient) { }

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
        console.log(false);
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

    getProfile(setupTempProfile: boolean): Observable<IExternalWrapper<IProfile>> {
        const subject = new Subject<IExternalWrapper<IProfile>>();
        this.http.get('/api/profile')
            .pipe(map((result: IProfile) => {
                if (setupTempProfile) {
                    this.tempProfile = result;
                }
                return {
                    statusCode: 200,
                    resObject: result
                } as IExternalWrapper<IProfile>;
            }))
            .pipe(catchError(this.errorHandler))
            .subscribe((profileResult: IExternalWrapper<IProfile>) => {
                subject.next(profileResult);
                subject.complete();
            });
        return subject;
    }

    getFullProfile(setupTempProfile: boolean): Observable<IExternalWrapper<IProfile>> {
        const subject = new Subject<IExternalWrapper<IProfile>>();
        this.http.get('/api/profile/actors')
            .pipe(map((result: IProfile) => {
                if (setupTempProfile) {
                    this.tempProfile = result;
                }
                return {
                    statusCode: 200,
                    resObject: result
                } as IExternalWrapper<IProfile>;
            }))
            .pipe(catchError(this.errorHandler))
            .subscribe((profileResult: IExternalWrapper<IProfile>) => {
                subject.next(profileResult);
                subject.complete();
            });
        return subject;
    }

    getProfileStatus(): Observable<IExternalWrapper<IProfileStatus>> {
        const subject = new Subject<IExternalWrapper<IProfileStatus>>();
        setTimeout(() => {
            subject.next({
                statusCode: 501,
                errors: [STRINGS.notImplemented]
            });
            subject.complete();
        }, 50);
        return subject;
    }
}
