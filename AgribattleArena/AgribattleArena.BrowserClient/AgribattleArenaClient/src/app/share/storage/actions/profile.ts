import { Action } from '@ngrx/store';
import { IProfile, IProfileStatus } from '../../models';

export const CHANGE_PROFILE = '[Profile] Change profile';
export const CHANGE_PROFILE_STATUS = '[Profile] Change profile status';
export const CHANGE_AUTHORIZED_STATE = '[Profile] Change authorized state';

export class Change implements Action {
    readonly type = CHANGE_PROFILE;

    constructor(public payload: IProfile) {

    }
}

export class ChangeStatus implements Action {
    readonly type = CHANGE_PROFILE_STATUS;

    constructor(public payload: IProfileStatus) {

    }
}

export class ChangeAuthorized implements Action {
    readonly type = CHANGE_AUTHORIZED_STATE;

    constructor(public payload: boolean) {

    }
}

export type Action = Change | ChangeStatus | ChangeAuthorized;
