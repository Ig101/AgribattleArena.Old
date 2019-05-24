import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { AuthService} from '../../share/index';
import { LoadingService } from 'src/app/loading';
import { IExternalWrapper, IProfile } from 'src/app/share/models';
import { checkServiceResponseError, getServiceResponseErrorContent } from 'src/app/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { controlRequiredValidator } from 'src/app/share/validators';

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
        this.userName = new FormControl('', controlRequiredValidator);
        this.password = new FormControl('', controlRequiredValidator);
        this.loginForm = new FormGroup({
          userName: this.userName,
          password: this.password
        });
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
