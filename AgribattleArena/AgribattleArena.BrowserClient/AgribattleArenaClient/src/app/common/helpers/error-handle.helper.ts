import { ValidationErrors } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export class ErrorHandleHelper {

    static getBadRequestError(error: ValidationErrors) {
        if (error) {
            return Object.values(error)[0] as string;
        } else {
            return STRINGS.badRequest;
        }
    }

    static getInternalServerError(error: ValidationErrors) {
        return STRINGS.serverError;
    }

    static getUnauthorizedError(error: ValidationErrors) {
        return STRINGS.unathorized;
    }
}
