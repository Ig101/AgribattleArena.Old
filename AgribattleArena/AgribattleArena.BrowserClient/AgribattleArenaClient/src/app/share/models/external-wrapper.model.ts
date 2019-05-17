export interface IExternalWrapper<T> {
    statusCode: number;
    error?: string;
    resObject?: T;
}
