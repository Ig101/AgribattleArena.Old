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
import { INativesStore } from './models/natives-store.model';
import { IRevelationLevel } from './models/revelation-level.model';

@Injectable({
    providedIn: 'root'
})
export class CatalogService {

    constructor(private http: HttpClient, private store: Store<stateStore.State>) {
    }

    private errorHandler(errorResult: HttpErrorResponse) {
        let errorMessage: string;
        switch (errorResult.status) {
            default:
                errorMessage = ErrorHandleHelper.getInternalServerError(errorResult.error);
                break;
        }
        return of({
            statusCode: errorResult.status,
            errors: [errorMessage]
        });
    }

    getNatives(): Observable<IExternalWrapper<INativesStore>> {
        const subject = new Subject<IExternalWrapper<INativesStore>>();
        this.http.get('/api/catalogs/frontendNatives')
            .pipe(map((result: INativesStore) => {
                return {
                    statusCode: 200,
                    resObject: result
                } as IExternalWrapper<INativesStore>;
            }))
            .pipe(catchError(this.errorHandler))
            .subscribe((result: IExternalWrapper<INativesStore>) => {
                subject.next(result);
                subject.complete();
            });
        return subject;
    }

    getRevelationLevels(): Observable<IExternalWrapper<IRevelationLevel[]>> {
        const subject = new Subject<IExternalWrapper<IRevelationLevel[]>>();
        this.http.get('/api/catalogs/revelationLevels')
            .pipe(map((result: IRevelationLevel[]) => {
                return {
                    statusCode: 200,
                    resObject: result
                } as IExternalWrapper<IRevelationLevel[]>;
            }))
            .pipe(catchError(this.errorHandler))
            .subscribe((result: IExternalWrapper<IRevelationLevel[]>) => {
                subject.next(result);
                subject.complete();
            });
        return subject;
    }
}
