import { Component, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../share/index';
import { LoadingService } from 'src/app/loading';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['../main.component.css']
})
export class RegisterComponent {

    @Output() cancelEmitter = new EventEmitter();

    private userName;
    private email;
    private password;
    private confirmPassword;

    constructor(private authService: AuthService, private loadingService: LoadingService) {

    }

    registerButtonPress(formValue) {
        this.loadingService.loadingStart('Registration...');
        this.authService.register(formValue).subscribe((error: string) => {
            console.log(error);
            this.loadingService.loadingEnd();
        });
    }

    cancelButtonPress() {
        this.cancelEmitter.emit();
    }
}
