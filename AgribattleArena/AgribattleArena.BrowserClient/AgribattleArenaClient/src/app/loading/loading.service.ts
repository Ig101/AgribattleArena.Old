import { Injectable } from '@angular/core';
import { ILoadingModel } from './loading.model';
import { Subject, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { IRouteLink } from '../common';
import { LoadingStatusEnum } from './loading-status.enum';
import { version } from 'punycode';
import { Version } from '@angular/compiler';
import { ENVIRONMENT } from '../environment';

@Injectable()
export class LoadingService {

    tempVersion = 0;

    runtimeTimer: any;

    private loadingStateSubject: Subject<ILoadingModel> = new Subject<ILoadingModel>();

    private loadingState: ILoadingModel = {
        loadingStatus: LoadingStatusEnum.Loading,
        loading: 1,
        message: 'Loading...',
        opaque: 1
    };

    private loadingIncrement() {
        this.tempVersion++;
        if (this.tempVersion === Number.MAX_VALUE) {
            this.tempVersion = 0;
        }
    }

    private updateLoadingState() {
        this.loadingStateSubject.next(this.loadingState);
    }

    private setupLoadingError(error: string) {
        this.loadingState.loadingStatus = LoadingStatusEnum.Error;
        this.loadingState.message = error;
    }

    private loadingAnimation(ver: number, side: boolean, routeLink?: IRouteLink) {
        if (this.tempVersion === ver) {
            const newLoading = this.loadingState.loading + ENVIRONMENT.loadingSpeed * (side ? 1 : -1);
            const newMessage = this.loadingState.message;
            const newOpaque = this.loadingState.opaque;

            this.loadingState. loading = newLoading;

            let end = false;
            if ((side && newLoading >= 1) || (!side && newLoading <= 0)) {
                this.loadingState.loading = side ? 1 : 0;
                end = true;
            }
            this.updateLoadingState();
            if (!end) {
                setTimeout(() => {
                    this.loadingAnimation(ver, side, routeLink);
                }, 20);
            } else if (routeLink !== undefined) {
                routeLink.router.navigate([routeLink.route]);
            }
        }
    }

    getState() {
        return this.loadingStateSubject;
    }

    loadingStart(incomingMessage: string, incomingOpaque: number, routeLink?: IRouteLink, timeout?: number): number {
        focus();
        clearTimeout(this.runtimeTimer);
        this.loadingIncrement();
        this.loadingState = {
            loadingStatus: LoadingStatusEnum.Loading,
            loading: this.loadingState.loading,
            message: incomingMessage,
            opaque: incomingOpaque
        };
        this.runtimeTimer = setTimeout(() => {
            this.loadingEnd(this.tempVersion, 'Loading process timeout');
        }, timeout === undefined ? ENVIRONMENT.loadingTimeout : timeout);
        this.loadingAnimation(this.tempVersion, true, routeLink);
        return this.tempVersion;
    }

    loadingError(incomingError: string, incomingOpaque: number): number {
        this.loadingIncrement();
        this.loadingState = {
            loadingStatus: LoadingStatusEnum.Error,
            loading: this.loadingState.loading,
            message: incomingError,
            opaque: incomingOpaque
        };
        this.loadingAnimation(this.tempVersion, true);
        return this.tempVersion;
    }

    loadingEnd(ver?: number, error?: string) {
        if (ver === undefined || this.tempVersion === ver) {
            clearTimeout(this.runtimeTimer);
            if (error === undefined) {
                this.loadingIncrement();
                this.loadingAnimation(this.tempVersion, false);
            } else {
                this.setupLoadingError(error);
                this.updateLoadingState();
            }
        }
    }

    loadingChangeMessage(incomingMessage: string) {
        this.loadingState.message = incomingMessage;
        this.updateLoadingState();
    }
}
