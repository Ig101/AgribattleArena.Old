import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { LoadingService } from 'src/app/loading';
import { FormControlWrapper } from './form-control-wrapper.control';
import { ENVIRONMENT } from 'src/app/environment';

@Component({
    selector: 'app-button-wrapper',
    templateUrl: './button-wrapper.component.html',
    styles: [`
        :host{ }
    `]
})
export class ButtonWrapperComponent {

    @Input() parentForm: FormGroup;

    @Input() divClass: string;
    @Input() buttonLabel: string;
    @Input() buttonValidate: boolean;
    @Input() buttonType: string;
    @Input() buttonClass: string;

    @Output() clickEmitter = new EventEmitter();

    constructor(private loadingService: LoadingService) { }

    buttonPress() {
        if (this.buttonValidate && this.parentForm.invalid) {
            let error: string = null;
            // tslint:disable-next-line: forin
            for (const field in this.parentForm.controls) {
                const control = this.parentForm.get(field) as FormControlWrapper;
                if (control.errors && error === null) {
                    error = control.name + ' ' + Object.values(control.errors)[0];
                }
                if (control.removeOnClick) {
                    control.setValue('');
                }
            }
            this.loadingService.loadingError(error, ENVIRONMENT.defaultLoadingOpacity);
        } else {
            this.clickEmitter.emit();
        }
    }

    validateAll() {
        if (this.buttonValidate) {
            // tslint:disable-next-line: forin
            for (const field in this.parentForm.controls) {
                const control = this.parentForm.get(field);
                control.markAsDirty();
                control.markAsTouched();
            }
        }
    }
}
