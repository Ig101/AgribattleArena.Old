import { Component, Input, EventEmitter, Output, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
    selector: 'app-textbox-wrapper',
    templateUrl: './textbox-wrapper.component.html',
    changeDetection: ChangeDetectionStrategy.Default
})
export class TextboxWrapperComponent implements OnInit {

    @Input() parentForm: FormGroup;
    private control: FormControl;

    @Input() divClass: string;
    @Input() label: string;
    @Input() labelClass: string;
    @Input() inputName: string;
    @Input() inputPlaceholder: string;
    @Input() inputType: string;
    @Input() inputClass: string;
    @Input() name: string;

    @Output() changeEmitter = new EventEmitter();

    ngOnInit() {
        this.control = this.parentForm.controls[this.inputName] as FormControl;
    }

    changeElement() {
        this.changeEmitter.emit();
    }
}
