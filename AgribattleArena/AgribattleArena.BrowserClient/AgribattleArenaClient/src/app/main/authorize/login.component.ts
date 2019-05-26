import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { AuthService} from '../../share/index';
import { LoadingService } from 'src/app/loading';
import { IExternalWrapper, IProfile } from 'src/app/share/models';
import { checkServiceResponseError, getServiceResponseErrorContent } from 'src/app/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { controlRequiredValidator } from 'src/app/common/validators';
import { FormControlWrapper } from 'src/app/share/wrappers/form-control-wrapper.control';
import { ENVIRONMENT } from 'src/app/environment';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['../main.component.css']
})
export class LoginComponent implements OnInit {

    @Output() registerEmitter = new EventEmitter();
    @Output() forgotPasswordEmitter = new EventEmitter();
    @Output() loginEmitter = new EventEmitter();

    private loginForm: FormGroup;
    private userName: FormControl;
    private password: FormControl;

    constructor(private authService: AuthService, private loadingService: LoadingService) {

    }

    ngOnInit() {
        this.userName = new FormControlWrapper('User Name', false, '', controlRequiredValidator);
        this.password = new FormControlWrapper('Password', true, '', controlRequiredValidator);
        this.loginForm = new FormGroup({
          userName: this.userName,
          password: this.password
        });
    }

    loginButtonPress(formValue) {
        const ver = this.loadingService.loadingStart('Authorization...', ENVIRONMENT.defaultLoadingOpacity);
        this.authService.login(formValue).subscribe((resObject: IExternalWrapper<IProfile>) => {
            if (checkServiceResponseError(resObject)) {
                this.loadingService.loadingEnd(ver, getServiceResponseErrorContent(resObject));
            } else {
                this.loadingService.loadingEnd(ver);
                this.loginEmitter.emit(resObject.resObject);
            }
            this.password.setValue('');
        });
    }

    registerButtonPress() {
        this.registerEmitter.emit();
    }

    forgotPasswordButtonPress() {
        this.forgotPasswordEmitter.emit();
    }
}
