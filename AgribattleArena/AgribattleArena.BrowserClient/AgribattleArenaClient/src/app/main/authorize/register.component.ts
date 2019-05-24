import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { AuthService } from '../../share/index';
import { LoadingService } from 'src/app/loading';
import { IExternalWrapper } from 'src/app/share/models';
import { checkServiceResponseError, getServiceResponseErrorContent } from 'src/app/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { controlRequiredValidator, passwordDigitsValidator, passwordLowercaseValidator, passwordUppercaseValidator,
    passwordSpecialValidator, controlMinLengthValidator, confirmPasswordValidator, controlLettersDigitsValidator,
    controlfirstLetterValidator, controlMaxLengthValidator, emailValidator } from 'src/app/share/validators';
import { FormControlWrapper } from 'src/app/share/wrappers/form-control-wrapper.control';

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
        this.userName = new FormControlWrapper('User Name', '', [
            controlRequiredValidator,
            controlLettersDigitsValidator,
            controlfirstLetterValidator,
            controlMinLengthValidator(3),
            controlMaxLengthValidator(20)
        ]);
        this.email = new FormControlWrapper('Email', '', [
            controlRequiredValidator,
            emailValidator
        ]);
        this.password = new FormControlWrapper('Password', '', [
            controlRequiredValidator,
            passwordDigitsValidator,
            passwordLowercaseValidator,
            passwordUppercaseValidator,
            passwordSpecialValidator,
            controlMinLengthValidator(6)
        ]);
        this.confirmPassword = new FormControlWrapper('Confirm Password', '', [
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
                return;
            }
            // TODO register_logic
            this.registerEmitter.emit();
        });
    }

    cancelButtonPress() {
        this.cancelEmitter.emit();
    }
}
