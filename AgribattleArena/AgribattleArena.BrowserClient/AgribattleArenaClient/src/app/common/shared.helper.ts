import { IExternalWrapper } from '../share/models';
import { STRINGS } from '../environment';

export function checkServiceResponseError<T>(resObject: IExternalWrapper<T>): boolean {
    return resObject.statusCode >= 400 || (resObject.errors !== undefined && resObject.errors.length > 0);
}

export function getServiceResponseErrorContent<T>(resObject: IExternalWrapper<T>): string {
    return resObject.errors !== undefined && resObject.errors.length > 0 ? resObject.errors[0] : STRINGS.unexpectedError;
}
