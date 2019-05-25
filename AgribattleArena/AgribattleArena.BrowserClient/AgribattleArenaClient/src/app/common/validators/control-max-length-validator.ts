import { FormControl, Validators } from '@angular/forms';

export function controlMaxLengthValidator(maxLength: number) {
    return (control: FormControl) => {
      const error = Validators.maxLength(maxLength)(control);
      if (error) {
        return {maxLength: 'should contain less than or equal to' + maxLength + ' symbols'};
      }
      return null;
    };
}
