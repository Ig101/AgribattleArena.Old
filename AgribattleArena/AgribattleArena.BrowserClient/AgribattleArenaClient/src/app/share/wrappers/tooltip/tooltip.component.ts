import { Component, Input, OnInit } from '@angular/core';
import { FormControlWrapper } from '../form-control-wrapper.control';

@Component({
    selector: 'app-tooltip',
    templateUrl: './tooltip.component.html',
    styleUrls: [ './tooltip.style.css' ]
})
export class TooltipComponent {
    @Input() control: FormControlWrapper;
    @Input() controlFocus: boolean;

    getErrorsList() {
        return Object.values(this.control.errors);
    }
}
