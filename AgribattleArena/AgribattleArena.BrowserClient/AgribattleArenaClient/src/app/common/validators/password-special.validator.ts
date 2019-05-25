import { FormControl, Validators } from '@angular/forms';

export function passwordSpecialValidator(control: FormControl) {
    const error = Validators.pattern('(?=.*[$@$!%*?&]).{0,}')(control);
    if (error) {
        return {oneSpecial: 'should have at least one symbol from $,@,!,%,*,?,&'};
    }
    return null;
}
