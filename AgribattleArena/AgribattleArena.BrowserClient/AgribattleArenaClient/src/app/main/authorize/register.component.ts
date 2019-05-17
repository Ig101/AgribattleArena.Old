import { Component, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../share/index';
import { LoadingService } from 'src/app/loading';
import { IExternalWrapper } from 'src/app/share/models';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['../main.component.css']
})
export class RegisterComponent {

    @Output() cancelEmitter = new EventEmitter();
    @Output() registerEmitter = new EventEmitter();

    private userName;
    private email;
    private password;
    private confirmPassword;

    constructor(private authService: AuthService, private loadingService: LoadingService) {

    }

    registerButtonPress(formValue) {
        const ver = this.loadingService.loadingStart('Registration...', 0.5);
        this.authService.register(formValue).subscribe((resObject: IExternalWrapper<any>) => {
            this.loadingService.loadingEnd(ver, resObject.error);
            this.registerEmitter.emit();
        });
    }

    cancelButtonPress() {
        this.cancelEmitter.emit();
    }
}
