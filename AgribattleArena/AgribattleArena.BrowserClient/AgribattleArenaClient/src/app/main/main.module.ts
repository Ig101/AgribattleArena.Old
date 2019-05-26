import { NgModule } from '@angular/core';
import { TOASTR_TOKEN, JQ_TOKEN, FocusRemoverDirective } from '../common';
import { AuthService } from '../share';
import { RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
import { mainRoutes } from './main.routes';
import { StartPageComponent } from './start-page.component';
import { LoginComponent, RegisterComponent, ProfileComponent, ForgotPasswordComponent} from './authorize';
import { WrappersModule } from '../share/wrappers/wrappers.module';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ProfileService } from '../share/profile.service';
import { MainResolver } from './main.resolver';

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
    WrappersModule,
    HttpClientModule
  ],
  providers: [
    AuthService,
    ProfileService,
    MainResolver,
    {provide: TOASTR_TOKEN, useValue: toastr},
    {provide: JQ_TOKEN, useValue: jQuery}
  ],
  bootstrap: [
      MainComponent
  ]
})
export class MainModule { }
