import { FormControl, FormGroup } from '@angular/forms';

export function confirmPasswordValidator(control: FormControl) {
    if (control.root.value.password === control.value) {
        return null;
    } else {
        return {confirmPassword: false};
    }
}
