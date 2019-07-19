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
import { INativesStoreMapped } from './models/natives-store-mapped.model';
import { ISpriteNative, IActorNative, IDecorationNative, ISkillNative, ISpecEffectNative,
    ITileNative, IBuffNative } from './models/natives/mapped';

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

    private mapNativesStore(store: INativesStore): INativesStoreMapped {
        if (!store) {
            return null;
        }
        const mappedSprites = store.sprites.map(old => {
            return {
                id: old.id
            } as ISpriteNative;
        });
        const mappedActors = store.actors.map(old => {
            return {
                id: old.id,
                icon: old.icon,
                name: old.name,
                description: old.description,
                fullDescription: old.fullDescription,
                size: old.size,
                shadowOpacity: old.shadowOpacity,
                sprite: mappedSprites.find(x => x.id === old.sprite),
                radius: old.radius
            } as IActorNative;
        });
        const mappedDecorations = store.decorations.map(old => {
            return {
                id: old.id,
                size: old.size,
                radius: old.radius,
                shadowOpacity: old.shadowOpacity,
                action: window[old.action],
                onDeathAction: window[old.onDeathAction],
                name: old.name,
                description: old.description,
                sprite: mappedSprites.find(x => x.id === old.sprite)
            } as IDecorationNative;
        });
        const mappedSkills = store.skills.map(old => {
            return {
                id: old.id,
                action: window[old.action],
                icon: old.icon,
                name: old.name,
                description: old.description
            } as ISkillNative;
        });
        const mappedEffects = store.specEffects.map(old => {
            return {
                id: old.id,
                size: old.size,
                action: window[old.action],
                onDeathAction: window[old.onDeathAction],
                name: old.name,
                description: old.description,
                sprite: mappedSprites.find(x => x.id === old.id)
            } as ISpecEffectNative;
        });
        const mappedTiles = store.tiles.map(old => {
            return {
                id: old.id,
                sprite: old.sprite,
                action: window[old.action],
                onStepAction: window[old.onStepAction],
                name: old.name,
                description: old.description
            } as ITileNative;
        });
        const mappedBuffs = store.buffs.map(old => {
            return {
                id: old.id,
                action: window[old.action],
                onPurgeAction: window[old.onPurgeAction],
                icon: old.icon,
                name: old.name,
                description: old.description
            } as IBuffNative;
        });
        return {
            sprites: mappedSprites,
            actors: mappedActors,
            buffs: mappedBuffs,
            decorations: mappedDecorations,
            specEffects: mappedEffects,
            skills: mappedSkills,
            tiles: mappedTiles
        } as INativesStoreMapped;
    }

    getNatives(): Observable<IExternalWrapper<INativesStoreMapped>> {
        const subject = new Subject<IExternalWrapper<INativesStoreMapped>>();
        this.http.get('/api/catalogs/frontendNatives')
            .pipe(map((result: INativesStore) => {
                return {
                    statusCode: 200,
                    resObject: result
                } as IExternalWrapper<INativesStore>;
            }))
            .pipe(catchError(this.errorHandler))
            .subscribe((result: IExternalWrapper<INativesStore>) => {
                subject.next({
                    statusCode: result.statusCode,
                    errors: result.errors,
                    resObject: this.mapNativesStore(result.resObject)
                });
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
