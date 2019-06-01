import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function controlMinLengthValidator(minLength: number) {
    return (control: FormControl) => {
      const error = Validators.minLength(minLength)(control);
      if (error) {
        return {minLength: STRINGS.minLengthStartError + minLength + STRINGS.lengthEndError};
      }
      return null;
    };
}
