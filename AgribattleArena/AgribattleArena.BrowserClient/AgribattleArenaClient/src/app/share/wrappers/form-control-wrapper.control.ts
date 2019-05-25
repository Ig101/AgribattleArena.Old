import { FormControl, ValidatorFn, AbstractControlOptions, AsyncValidatorFn } from '@angular/forms';

export class FormControlWrapper extends FormControl {
    name: string;
    removeOnClick: boolean;

    constructor(name: string, removeOnClick: boolean, defaultValue?: string,
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(defaultValue, validatorOrOpts, asyncValidator);
        this.name = name;
        this.removeOnClick = removeOnClick;
    }
}
