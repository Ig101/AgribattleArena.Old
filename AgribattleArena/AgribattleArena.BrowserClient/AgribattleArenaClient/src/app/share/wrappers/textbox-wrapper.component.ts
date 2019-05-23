import { Component, Input, EventEmitter, Output, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'app-textbox-wrapper',
    templateUrl: './textbox-wrapper.component.html',
    changeDetection: ChangeDetectionStrategy.Default
})
export class TextboxWrapperComponent {

    @Input() parentForm: FormGroup;

    @Input() divClass: string;
    @Input() label: string;
    @Input() labelClass: string;
    @Input() inputName: string;
    @Input() inputPlaceholder: string;
    @Input() inputType: string;
    @Input() inputClass: string;

    @Output() changeEmitter = new EventEmitter();

    changeElement() {
        this.changeEmitter.emit();
    }
}
