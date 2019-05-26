import { Injectable, ErrorHandler } from '@angular/core';
import { IProfile, IExternalWrapper, IProfileActor } from './models';
import { Subject, Observable, of } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { getParseErrors } from '@angular/compiler';
import { ErrorHandleHelper } from '../common/helpers/error-handle.helper';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

    tempProfile: IProfileActor;

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
                errors: ['Not implemented']
            });
            subject.complete();
        }, 50);
        return subject;
    }

    getProfile(): Observable<IExternalWrapper<IProfile>> {
        const subject = new Subject<IExternalWrapper<IProfile>>();
        this.http.get('/api/profile')
            .pipe(map((result: IProfile) => {
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
}
