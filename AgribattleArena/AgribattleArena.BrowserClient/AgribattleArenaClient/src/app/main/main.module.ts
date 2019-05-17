import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { TOASTR_TOKEN, JQ_TOKEN, FocusRemoverDirective } from '../common';
import { AuthService } from '../share';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
import { mainRoutes } from './main.routes';
import { StartPageComponent } from './start-page.component';
import { LoginComponent, RegisterComponent, ProfileComponent, ForgotPasswordComponent} from './authorize';

// tslint:disable-next-line: no-string-literal
const toastr = window['toastr'];
// tslint:disable-next-line: no-string-literal
const jQuery = window['$'];

@NgModule({
  declarations: [
    MainComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    StartPageComponent,
    ForgotPasswordComponent,
    FocusRemoverDirective
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(mainRoutes),
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    AuthService,
    {provide: TOASTR_TOKEN, useValue: toastr},
    {provide: JQ_TOKEN, useValue: jQuery}
  ],
  bootstrap: [
      MainComponent
  ]
})
export class MainModule { }
