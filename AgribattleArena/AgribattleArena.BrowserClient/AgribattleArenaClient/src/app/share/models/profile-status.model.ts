import { ProfileStatusEnum } from './enums/profile-status.enum';
import { IProfileBattleStatus } from './profile-battle-status.model';
import { IProfileQueueStatus } from './profile-queue-status.model';

export interface IProfileStatus {
    status: ProfileStatusEnum;
    battleInfo: IProfileBattleStatus;
    queueInfo: IProfileQueueStatus;
}
