import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function passwordUppercaseValidator(control: FormControl) {
    const error = Validators.pattern('(?=.*[A-Z]).{0,}')(control);
    if (error) {
      return {oneUppercase: STRINGS.oneUppercaseError};
    }
    return null;
}
