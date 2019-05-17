import { Component, Output, EventEmitter } from '@angular/core';
import { AuthService} from '../../share/index';
import { LoadingService } from 'src/app/loading';
import { IExternalWrapper, IProfile } from 'src/app/share/models';
import { checkServiceResponseError, getServiceResponseErrorContent } from 'src/app/common';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['../main.component.css']
})
export class LoginComponent {

    @Output() registerEmitter = new EventEmitter();
    @Output() forgotPasswordEmitter = new EventEmitter();
    @Output() loginEmitter = new EventEmitter();

    private userName;
    private password;

    constructor(private authService: AuthService, private loadingService: LoadingService) {

    }

    loginButtonPress(formValue) {
        const ver = this.loadingService.loadingStart('Authorization...', 0.5);
        this.authService.login(formValue).subscribe((resObject: IExternalWrapper<IProfile>) => {
            if (checkServiceResponseError(resObject)) {
                this.loadingService.loadingEnd(ver, getServiceResponseErrorContent(resObject));
                return;
            }
            // TODO login_logic
            this.loginEmitter.emit();
        });
    }

    registerButtonPress() {
        this.registerEmitter.emit();
    }

    forgotPasswordButtonPress() {
        this.forgotPasswordEmitter.emit();
    }
}
