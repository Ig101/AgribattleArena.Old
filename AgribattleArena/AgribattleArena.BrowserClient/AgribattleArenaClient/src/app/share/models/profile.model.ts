import { IProfileActor } from './profile-actor.model';

export interface IProfile {
    userName: string;
    resources: number;
    donationResources: number;
    revelations: number;
    revelationsLevel: number;
    barracksSize: number;
    actors?: IProfileActor[];
}
