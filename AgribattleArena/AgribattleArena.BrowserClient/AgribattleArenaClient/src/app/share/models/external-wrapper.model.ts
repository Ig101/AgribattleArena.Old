export interface IExternalWrapper<T> {
    statusCode: number;
    errors?: string[];
    resObject?: T;
}
