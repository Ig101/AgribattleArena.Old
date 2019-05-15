import { NgModule } from '@angular/core';
import { TOASTR_TOKEN, JQ_TOKEN } from '../common/index';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
import { mainRoutes } from './main.routes';
import { LoginComponent, RegisterComponent, ProfileComponent} from './authorize/index';
import { StartPageComponent } from './start-page.component';

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
      StartPageComponent
  ],
  imports: [
      CommonModule,
      RouterModule.forChild(mainRoutes)
  ],
  providers: [
    {provide: TOASTR_TOKEN, useValue: toastr},
    {provide: JQ_TOKEN, useValue: jQuery}
  ],
  bootstrap: [
      MainComponent
  ]
})
export class MainModule { }
