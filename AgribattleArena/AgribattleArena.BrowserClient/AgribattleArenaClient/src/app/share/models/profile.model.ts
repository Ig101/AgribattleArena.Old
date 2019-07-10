import { IProfileActor } from './profile-actor.model';

export interface IProfile {
    id: string;
    userName: string;
    resources: number;
    donationResources: number;
    revelations: number;
    revelationLevel: number;
    barracksSize: number;
    actors?: IProfileActor[];
}
