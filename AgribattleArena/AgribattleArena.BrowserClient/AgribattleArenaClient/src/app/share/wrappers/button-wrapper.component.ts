import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

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

    buttonPress() {
        this.clickEmitter.emit();
    }
}
