import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function controlMaxLengthValidator(maxLength: number) {
    return (control: FormControl) => {
      const error = Validators.maxLength(maxLength)(control);
      if (error) {
        return {maxLength: STRINGS.maxLengthStartError + maxLength + STRINGS.lengthEndError};
      }
      return null;
    };
}
