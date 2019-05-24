import { FormControl, Validators } from '@angular/forms';

export function controlRequiredValidator(control: FormControl) {
    const error = Validators.required(control);
    if (error) {
        return {required: 'required'};
    }
    return null;
}
