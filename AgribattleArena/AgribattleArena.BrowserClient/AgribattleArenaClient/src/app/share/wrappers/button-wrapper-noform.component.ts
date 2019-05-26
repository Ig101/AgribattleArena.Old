import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-button-wrapper-noform',
    templateUrl: './button-wrapper-noform.component.html',
    styles: [`
        :host{ }
    `]
})
export class ButtonWrapperNoformComponent {

    @Input() divClass: string;
    @Input() buttonLabel: string;
    @Input() buttonType: string;
    @Input() buttonClass: string;

    @Output() clickEmitter = new EventEmitter();

    buttonPress() {
        this.clickEmitter.emit();
    }
}
