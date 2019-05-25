import { Injectable } from '@angular/core';
import { IUserForRegistration, IUserForLogin, IProfile, IExternalWrapper } from './models';
import { Subject, Observable, of } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private http: HttpClient) { }

    private errorHandler(errorResult: HttpErrorResponse) {
        let errorMessage: string;
        switch (errorResult.status) {
            case 401:
                    errorMessage = 'Incorrect login or password';
                    break;
            case 404:
                errorMessage = 'Unauthorized';
                break;
            default:
                errorMessage = 'Server error';
                break;
        }
        return of({
            statusCode: errorResult.status,
            errors: [errorMessage]
        });
    }


    login(user: IUserForLogin): Observable<IExternalWrapper<IProfile>> {
        const subject = new Subject<IExternalWrapper<IProfile>>();
        this.http.post('/api/auth/login', {
            login: user.userName,
            password: user.password
        })
            .pipe(map((result: any) => {
                return {
                    statusCode: 200
                } as IExternalWrapper<any>;
            }))
            .pipe(catchError(this.errorHandler))
            .subscribe((loginResult: IExternalWrapper<any>) => {
                if (loginResult.statusCode === 200) {
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
                } else {
                    subject.next(loginResult);
                    subject.complete();
                }
            });
        return subject;
    }

    register(user: IUserForRegistration): Observable<IExternalWrapper<any>> {
        console.log(user);
        const subject = new Subject<IExternalWrapper<any>>();
        setTimeout(() => {
            subject.next({
                statusCode: 501,
                errors: ['Not implemented']
            });
            subject.complete();
        }, 2000);
        return subject;
    }

    logout(): Observable<IExternalWrapper<any>> {
        const subject = new Subject<IExternalWrapper<any>>();
        this.http.delete('/api/auth/login')
            .pipe(map((result: any) => {
                return {
                    statusCode: 200
                } as IExternalWrapper<any>;
            }))
            .pipe(catchError(this.errorHandler))
            .subscribe((logoutResult: IExternalWrapper<any>) => {
                subject.next(logoutResult);
            });
        return subject;
    }
}
