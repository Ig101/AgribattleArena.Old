import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function controlfirstLetterValidator(control: FormControl) {
    const error = Validators.pattern('^[A-Za-z]+.{0,}$')(control);
    if (error) {
      return {firstLetter: STRINGS.firstLetterError};
    }
    return null;
}
