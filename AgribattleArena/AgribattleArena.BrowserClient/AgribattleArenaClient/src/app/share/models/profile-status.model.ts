import { ProfileStatusEnum } from './enums/profile-status.enum';
import { IProfileQueueStatus } from './profile-queue-status.model';
import { ISynchronizer } from './synchronizer.model';

export interface IProfileStatus {
    status: ProfileStatusEnum;
    battleInfo: ISynchronizer;
    queueInfo: IProfileQueueStatus;
}
