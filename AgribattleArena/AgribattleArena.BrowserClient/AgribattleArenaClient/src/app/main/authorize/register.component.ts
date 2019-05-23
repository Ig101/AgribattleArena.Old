import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { AuthService } from '../../share/index';
import { LoadingService } from 'src/app/loading';
import { IExternalWrapper } from 'src/app/share/models';
import { checkServiceResponseError, getServiceResponseErrorContent } from 'src/app/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { confirmPasswordValidator } from './confirm-password.validator';

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
        this.userName = new FormControl('', [
            Validators.required,
            Validators.pattern('^[A-Za-z]+[A-Za-z0-9]*$'),
            Validators.minLength(3),
            Validators.maxLength(20)
        ]);
        this.email = new FormControl('', [
            Validators.required,
            Validators.email
        ]);
        this.password = new FormControl('', [
            Validators.required,
            Validators.pattern('(?=.*[0-9]).{0,}'),
            Validators.pattern('(?=.*[a-z]).{0,}'),
            Validators.pattern('(?=.*[A-Z]).{0,}'),
            Validators.pattern('(?=.*[$@$!%*?&]).{0,}'),
            Validators.minLength(6)
        ]);
        this.confirmPassword = new FormControl('', [
            Validators.required,
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
