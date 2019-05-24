import { FormControl, Validators } from '@angular/forms';

export function emailValidator(control: FormControl) {
    const error = Validators.email(control);
    if (error) {
        return {email: 'is incorrect'};
    }
    return null;
}
