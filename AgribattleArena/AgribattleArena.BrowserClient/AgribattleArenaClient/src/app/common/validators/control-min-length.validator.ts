import { FormControl, Validators } from '@angular/forms';

export function controlMinLengthValidator(minLength: number) {
    return (control: FormControl) => {
      const error = Validators.minLength(minLength)(control);
      if (error) {
        return {minLength: 'should contain at least ' + minLength + ' symbols.'};
      }
      return null;
    };
}
