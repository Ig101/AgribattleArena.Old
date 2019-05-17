import { Injectable } from '@angular/core';
import { IUserForRegistration, IUserForLogin, IProfile, IExternalWrapper } from './models';
import { Subject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    login(user: IUserForLogin): Observable<IExternalWrapper<IProfile>> {
        console.log(user);
        const subject = new Subject<IExternalWrapper<IProfile>>();
        setTimeout(() => {
            subject.next({
                statusCode: 501,
                errors: ['Not implemented']
            });
            subject.complete();
        }, 2000);
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
        console.log(false);
        const subject = new Subject<IExternalWrapper<any>>();
        setTimeout(() => {
            subject.next({
                statusCode: 501,
                errors: ['Not implemented']
            });
            subject.complete();
        }, 50);
        return subject;
    }
}
