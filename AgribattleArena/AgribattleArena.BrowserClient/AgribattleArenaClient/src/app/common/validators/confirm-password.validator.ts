import { FormControl } from '@angular/forms';

export function confirmPasswordValidator(control: FormControl) {
    if (control.root.value.password === control.value) {
        return null;
    } else {
        return {confirmPassword: 'should be the same as password.'};
    }
}
