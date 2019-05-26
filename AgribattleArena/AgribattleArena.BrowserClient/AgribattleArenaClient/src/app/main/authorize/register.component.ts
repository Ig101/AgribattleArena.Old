import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { AuthService } from '../../share/index';
import { LoadingService } from 'src/app/loading';
import { IExternalWrapper } from 'src/app/share/models';
import { checkServiceResponseError, getServiceResponseErrorContent } from 'src/app/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { controlRequiredValidator, passwordDigitsValidator, passwordLowercaseValidator, passwordUppercaseValidator,
    passwordSpecialValidator, controlMinLengthValidator, confirmPasswordValidator, controlLettersDigitsValidator,
    controlfirstLetterValidator, controlMaxLengthValidator, emailValidator } from 'src/app/common/validators';
import { FormControlWrapper } from 'src/app/share/wrappers/form-control-wrapper.control';
import { identifierModuleUrl } from '@angular/compiler';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['../main.component.css']
})
export class RegisterComponent implements OnInit {

    @Output() cancelEmitter = new EventEmitter();
    @Output() registerEmitter = new EventEmitter();

    private registerForm: FormGroup;
    private userName: FormControl;
    private email: FormControl;
    private password: FormControl;
    private confirmPassword: FormControl;

    constructor(private authService: AuthService, private loadingService: LoadingService) {

    }
    ngOnInit() {
        this.userName = new FormControlWrapper('User Name', false, '', [
            controlRequiredValidator,
            controlLettersDigitsValidator,
            controlfirstLetterValidator,
            controlMinLengthValidator(3),
            controlMaxLengthValidator(20)
        ]);
        this.email = new FormControlWrapper('Email', false, '', [
            controlRequiredValidator,
            emailValidator
        ]);
        this.password = new FormControlWrapper('Password', true, '', [
            controlRequiredValidator,
            passwordDigitsValidator,
            passwordLowercaseValidator,
            passwordUppercaseValidator,
            passwordSpecialValidator,
            controlMinLengthValidator(6)
        ]);
        this.confirmPassword = new FormControlWrapper('Confirm Password', true, '', [
            confirmPasswordValidator
        ]);
        this.registerForm = new FormGroup({
          userName: this.userName,
          email: this.email,
          password: this.password,
          confirmPassword: this.confirmPassword
        });
    }

    registerButtonPress(formValue) {
        const ver = this.loadingService.loadingStart('Registration...', 0.5);
        this.authService.register(formValue).subscribe((resObject: IExternalWrapper<any>) => {
            if (checkServiceResponseError(resObject)) {
                this.loadingService.loadingEnd(ver, getServiceResponseErrorContent(resObject));
            } else {
                this.loadingService.loadingEnd(ver, 'Registration is completed.');
                this.registerEmitter.emit();
            }
            this.password.setValue('');
            this.confirmPassword.setValue('');
        });
    }

    cancelButtonPress() {
        this.cancelEmitter.emit();
    }
}
