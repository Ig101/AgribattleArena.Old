import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function passwordDigitsValidator(control: FormControl) {
    const error = Validators.pattern('(?=.*[0-9]).{0,}')(control);
    if (error) {
      return {oneDigit: STRINGS.oneDigitError};
    }
    return null;
}
