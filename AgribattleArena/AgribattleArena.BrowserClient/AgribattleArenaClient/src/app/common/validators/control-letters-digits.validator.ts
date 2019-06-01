import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function controlLettersDigitsValidator(control: FormControl) {
    const error = Validators.pattern('^[A-Za-z0-9]*$')(control);
    if (error) {
      return {lettersDigits: STRINGS.letterDigitsError};
    }
    return null;
}
