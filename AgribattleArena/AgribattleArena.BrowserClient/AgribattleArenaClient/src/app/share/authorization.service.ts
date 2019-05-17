import { Injectable } from '@angular/core';
import { IUserForLogin } from './user-for-login.model';
import { IUserForRegistration } from './user-for-registration.model';
import { Subject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    login(user: IUserForLogin): Observable<string> {
        console.log(user);
        const subject = new Subject<string>();
        setTimeout(() => {
            subject.next('Not Implemented');
            subject.complete();
        }, 40000);
        return subject;
    }

    register(user: IUserForRegistration): Observable<string> {
        console.log(user);
        const subject = new Subject<string>();
        setTimeout(() => {
            subject.next('Not Implemented');
            subject.complete();
        }, 2000);
        return subject;
    }

    logout(): Observable<string> {
        console.log(false);
        const subject = new Subject<string>();
        setTimeout(() => {
            subject.next('Not Implemented');
            subject.complete();
        }, 50);
        return subject;
    }

    isAuthenticated(): Observable<boolean> {
        console.log(false);
        const subject = new Subject<boolean>();
        setTimeout(() => {
            subject.next(false);
            subject.complete();
        }, 50);
        return subject;
    }
}
