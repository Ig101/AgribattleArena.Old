export interface IProfileActor {
    id: number;
    name: string;
    actorNative: string;
    attackingSkillNative: string;
    skills: string[];
    strength: number;
    willpower: number;
    constitution: number;
    speed: number;
    actionPointsIncome: number;
    inParty: boolean;
}
