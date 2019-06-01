import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function emailValidator(control: FormControl) {
    const error = Validators.email(control);
    if (error) {
        return {email: STRINGS.emailError};
    }
    return null;
}
