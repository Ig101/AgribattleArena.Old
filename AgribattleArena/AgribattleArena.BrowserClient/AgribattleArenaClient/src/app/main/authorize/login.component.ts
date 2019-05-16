import { Component, Output, EventEmitter } from '@angular/core';
import { AuthService} from '../../share/index';
import { LoadingService } from 'src/app/loading';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['../main.component.css']
})
export class LoginComponent {

    @Output() registerEmitter = new EventEmitter();
    @Output() forgotPasswordEmitter = new EventEmitter();

    private userName;
    private password;

    constructor(private authService: AuthService, private loadingService: LoadingService) {

    }

    loginButtonPress(formValue) {
        this.loadingService.loadingStart('Authorization...');
        this.authService.login(formValue).subscribe((error: string) => {
            console.log(error);
            this.loadingService.loadingEnd();
        });
    }

    registerButtonPress() {
        this.registerEmitter.emit();
    }

    forgotPasswordButtonPress() {
        this.forgotPasswordEmitter.emit();
    }
}
