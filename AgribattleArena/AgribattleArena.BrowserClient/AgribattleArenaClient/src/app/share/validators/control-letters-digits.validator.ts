import { FormControl, Validators } from '@angular/forms';

export function controlLettersDigitsValidator(control: FormControl) {
    const error = Validators.pattern('^[A-Za-z0-9]*$')(control);
    if (error) {
      return {lettersDigits: 'should contain only letters (A-z) and digits'};
    }
    return null;
}
