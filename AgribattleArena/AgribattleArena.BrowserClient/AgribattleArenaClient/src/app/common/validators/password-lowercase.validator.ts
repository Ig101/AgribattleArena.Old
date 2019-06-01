import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function passwordLowercaseValidator(control: FormControl) {
    const error = Validators.pattern('(?=.*[a-z]).{0,}')(control);
    if (error) {
      return {oneLowercase: STRINGS.oneLowercaseError};
    }
    return null;
}
