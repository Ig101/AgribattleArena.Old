import { FormControl, Validators } from '@angular/forms';

export function passwordUppercaseValidator(control: FormControl) {
    const error = Validators.pattern('(?=.*[A-Z]).{0,}')(control);
    if (error) {
      return {oneUppercase: 'should have at least one uppercase letter (A-Z).'};
    }
    return null;
}
