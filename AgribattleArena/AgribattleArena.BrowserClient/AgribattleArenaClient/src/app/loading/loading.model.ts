import { LoadingStatusEnum } from './loading-status.enum';

export interface ILoadingModel {
    loadingStatus: LoadingStatusEnum;
    loading: number;
    message?: string;
    opaque: number;
}
