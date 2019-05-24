import { FormControl, Validators } from '@angular/forms';

export function controlMinLengthValidator(minLength: number) {
    return (control: FormControl) => {
      const error = Validators.minLength(minLength)(control);
      if (error) {
        return {minLength: '\'s minimum length is ' + minLength};
      }
      return null;
    };
}
