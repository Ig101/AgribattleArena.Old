import { Injectable } from '@angular/core';
import { IProfile, IExternalWrapper } from './models';
import { Subject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

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

}
