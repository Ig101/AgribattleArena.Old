import { Component, Input, EventEmitter, Output, ChangeDetectionStrategy, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { FormControlWrapper } from './form-control-wrapper.control';

@Component({
    selector: 'app-textbox-wrapper',
    templateUrl: './textbox-wrapper.component.html',
    changeDetection: ChangeDetectionStrategy.Default
})
export class TextboxWrapperComponent implements OnInit {
    @Input() parentForm: FormGroup;
    private control: FormControlWrapper;
    private focusState: boolean;

    @Input() divClass: string;
    @Input() label: string;
    @Input() labelClass: string;
    @Input() inputName: string;
    @Input() inputPlaceholder: string;
    @Input() inputType: string;
    @Input() inputClass: string;

    @Output() changeEmitter = new EventEmitter();

    ngOnInit() {
        this.control = this.parentForm.controls[this.inputName] as FormControlWrapper;
    }

    changeElement() {
        this.changeEmitter.emit();
    }

    checkValidity() {
        return this.control.invalid && this.control.touched && this.control.dirty;
    }
}
