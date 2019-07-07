import { IProfileState } from '../../models/profile-state.model';
import * as profileAction from '../actions/profile';
import { stat } from 'fs';

export const initialState: IProfileState = {
    profile: null,
    profileStatus: null,
    authorized: true
};

export function reducer(state = initialState, action: profileAction.Action) {

    switch (action.type) {
        case profileAction.CHANGE_PROFILE:
            return {
                ...state,
                profile: action.payload,
                authorized: action.payload !== undefined
            };
        case profileAction.CHANGE_PROFILE_STATUS:
            return {
                ...state,
                profileStatus: action.payload,
                authorized: action.payload !== undefined
            };
        case profileAction.CHANGE_AUTHORIZED_STATE:
            return {
                ...state,
                authorized: action.payload
            };
    }
}

export const getProfile = (state: IProfileState) => state.authorized ? state.profile : null;
export const getProfileStatus = (state: IProfileState) => state.authorized ? state.profileStatus : null;
export const getAuthorized = (state: IProfileState) => state.authorized;
