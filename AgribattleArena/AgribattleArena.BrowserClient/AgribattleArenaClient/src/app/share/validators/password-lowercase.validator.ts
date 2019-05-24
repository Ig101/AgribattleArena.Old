import { FormControl, Validators } from '@angular/forms';

export function passwordLowercaseValidator(control: FormControl) {
    const error = Validators.pattern('(?=.*[a-z]).{0,}')(control);
    if (error) {
      return {oneLowercase: 'should have at least one lowercase letter (a-z)'};
    }
    return null;
}
