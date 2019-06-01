import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function controlRequiredValidator(control: FormControl) {
    const error = Validators.required(control);
    if (error) {
        return {required: STRINGS.requiredError};
    }
    return null;
}
