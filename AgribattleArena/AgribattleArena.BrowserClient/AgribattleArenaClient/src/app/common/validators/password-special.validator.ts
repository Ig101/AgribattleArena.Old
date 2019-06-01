import { FormControl, Validators } from '@angular/forms';
import { STRINGS } from 'src/app/environment';

export function passwordSpecialValidator(control: FormControl) {
    const error = Validators.pattern('(?=.*[$@$!%*?&]).{0,}')(control);
    if (error) {
        return {oneSpecial: STRINGS.oneSpecialError};
    }
    return null;
}
