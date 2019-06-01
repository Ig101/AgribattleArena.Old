import { FormControl } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function confirmPasswordValidator(control: FormControl) {
    if (control.root.value.password === control.value) {
        return null;
    } else {
        return {confirmPassword: STRINGS.confirmPasswordError};
    }
}
