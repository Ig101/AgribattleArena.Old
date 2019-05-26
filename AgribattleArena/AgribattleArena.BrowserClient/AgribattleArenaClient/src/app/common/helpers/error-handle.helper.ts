import { ValidationErrors } from '@angular/forms';

export class ErrorHandleHelper {

    static getBadRequestError(error: ValidationErrors) {
        if (error) {
            return Object.values(error)[0] as string;
        } else {
            return 'Wrong input.';
        }
    }

    static getInternalServerError(error: ValidationErrors) {
        return 'Server error. Try again later.';
    }

    static getUnauthorizedError(error: ValidationErrors) {
        return 'Unauthorized. Return to the login page.';
    }
}
