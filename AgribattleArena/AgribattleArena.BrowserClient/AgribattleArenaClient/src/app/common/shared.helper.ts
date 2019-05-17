import { IExternalWrapper } from '../share/models';

export function checkServiceResponseError<T>(resObject: IExternalWrapper<T>): boolean {
    return resObject.statusCode >= 400 || (resObject.errors !== undefined && resObject.errors.length > 0);
}

export function checkServiceResponseErrorContent<T>(resObject: IExternalWrapper<T>): boolean {
    return resObject.errors !== undefined && resObject.errors.length > 0;
}
