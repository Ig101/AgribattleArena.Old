import { FormControl, Validators } from '@angular/forms';

export function passwordDigitsValidator(control: FormControl) {
    const error = Validators.pattern('(?=.*[0-9]).{0,}')(control);
    if (error) {
      return {oneDigit: 'should have at least one digit'};
    }
    return null;
}
