import { NgModule } from '@angular/core';
import { TOASTR_TOKEN, JQ_TOKEN } from '../common/index';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HubComponent } from './hub.component';
import { LoginComponent } from './login.component';
import { hubRoutes } from './hub.routes';

// tslint:disable-next-line: no-string-literal
const toastr = window['toastr'];
// tslint:disable-next-line: no-string-literal
const jQuery = window['$'];

@NgModule({
  declarations: [
      HubComponent,
      LoginComponent
  ],
  imports: [
      CommonModule,
      RouterModule.forChild(hubRoutes)
  ],
  providers: [
    {provide: TOASTR_TOKEN, useValue: toastr},
    {provide: JQ_TOKEN, useValue: jQuery}
  ],
  bootstrap: [
  ]
})
export class HubModule { }
