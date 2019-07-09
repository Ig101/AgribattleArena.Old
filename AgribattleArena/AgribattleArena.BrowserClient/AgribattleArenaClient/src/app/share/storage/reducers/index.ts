import {ActionReducerMap, createSelector, createFeatureSelector, ActionReducer, MetaReducer} from '@ngrx/store';

import * as profileReducer from './profile';
import { IProfileState } from '../../models/profile-state.model';

export interface State {
    profile: IProfileState;
}

export const reducers: ActionReducerMap<State> = {
    profile: profileReducer.reducer
};

export function logger(reducer: ActionReducer<State>): ActionReducer<State> {
    return (state: State, action: any): State => {
        console.log('state', state);
        console.log('action', action);
        return reducer(state, action);
    };
}

export const metaReducers: MetaReducer<State>[] = [];

export const getProfileState = createFeatureSelector<IProfileState>('profile');

export const getProfile = createSelector(
    getProfileState,
    profileReducer.getProfile
);

export const getProfileStatus = createSelector(
    getProfileState,
    profileReducer.getProfileStatus
);

export const getAuthorized = createSelector(
    getProfileState,
    profileReducer.getAuthorized
);
