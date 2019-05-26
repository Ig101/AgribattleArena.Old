import { FormControl, Validators } from '@angular/forms';

export function controlfirstLetterValidator(control: FormControl) {
    const error = Validators.pattern('^[A-Za-z]+.{0,}$')(control);
    if (error) {
      return {firstLetter: 'should start with letter (A-z).'};
    }
    return null;
}
