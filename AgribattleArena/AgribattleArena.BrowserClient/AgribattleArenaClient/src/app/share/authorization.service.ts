import { Injectable } from '@angular/core';
import { IUserForRegistration, IUserForLogin, IProfile, IExternalWrapper } from './models';
import { Subject, Observable, of } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { ErrorHandleHelper } from '../common/helpers/error-handle.helper';
import { ProfileService } from './profile.service';
import { STRINGS } from '../environment';
import { Store } from '@ngrx/store';
import * as stateStore from './storage/reducers';
import * as profileAction from './storage/actions/profile';
import { GameHubService } from './game-hub.service';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private http: HttpClient, private store: Store<stateStore.State>, private gameHubService: GameHubService) { }

    private errorHandler(errorResult: HttpErrorResponse) {
        let errorMessage: string;
        switch (errorResult.status) {
            case 400:
                errorMessage = ErrorHandleHelper.getBadRequestError(errorResult.error);
                break;
            case 401:
                errorMessage = STRINGS.incorrectLogin;
                break;
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
                    this.gameHubService.connect().subscribe((hubResult: IExternalWrapper<any>) => {
                        if (hubResult.statusCode === 200) {
                            this.http.get('/api/profile/actors')
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
                        } else {
                            this.store.dispatch(new profileAction.ChangeAuthorized(false));
                            subject.next(hubResult);
                            subject.complete();
                        }
                    });
                } else {
                    subject.next(loginResult);
                    subject.complete();
                }
            });
        return subject;
    }

    register(user: IUserForRegistration): Observable<IExternalWrapper<any>> {
        const subject = new Subject<IExternalWrapper<any>>();
        this.http.post('/api/auth/register', {
            login: user.userName,
            email: user.email,
            password: user.password
        })
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
                console.log(logoutResult);
                if (logoutResult.statusCode === 200) {
                    this.gameHubService.disconnect();
                    this.store.dispatch(new profileAction.ChangeAuthorized(false));
                }
                subject.next(logoutResult);
            });
        return subject;
    }
}
