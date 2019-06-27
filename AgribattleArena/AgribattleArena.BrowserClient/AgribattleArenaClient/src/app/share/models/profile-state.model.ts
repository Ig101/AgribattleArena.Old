import { IProfile } from './profile.model';
import { IProfileStatus } from './profile-status.model';

export interface IProfileState {
    profile: IProfile;
    profileStatus: IProfileStatus;
}
