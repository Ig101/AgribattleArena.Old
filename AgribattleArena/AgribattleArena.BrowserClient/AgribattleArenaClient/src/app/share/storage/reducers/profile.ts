import { IProfileState } from '../../models/profile-state.model';
import * as profileAction from '../actions/profile';

export const initialState: IProfileState = {
    profile: null,
    profileStatus: null
};

export function reducer(state = initialState, action: profileAction.Action) {

    switch (action.type) {
        case profileAction.CHANGE_PROFILE:
            return {
                ...state,
                profile: action.payload
            };
        case profileAction.CHANGE_PROFILE_STATUS:
            return {
                ...state,
                profileStatus: action.payload
            };
    }
}

export const getProfile = (state: IProfileState) => state.profile;
export const getProfileStatus = (state: IProfileState) => state.profileStatus;
