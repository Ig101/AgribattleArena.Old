import { FormControl, Validators } from '@angular/forms';

export function controlMaxLengthValidator(maxLength: number) {
    return (control: FormControl) => {
      const error = Validators.maxLength(maxLength)(control);
      if (error) {
        return {maxLength: '\'s maximum length is ' + maxLength};
      }
      return null;
    };
}
