import { SkillAction } from 'src/app/battle-engine/delegates/skill-delegates';

export interface ISkillNative {
    id: string;
    action: SkillAction;

    icon: string;
    name: string;
    description: string;
}
