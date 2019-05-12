import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { TOASTR_TOKEN, JQ_TOKEN } from './common/index';

// tslint:disable-next-line: no-string-literal
const toastr = window['toastr'];
// tslint:disable-next-line: no-string-literal
const jQuery = window['$'];
@NgModule({
  declarations: [
  ],
  imports: [
    BrowserModule
  ],
  providers: [
    {provide: TOASTR_TOKEN, useValue: toastr},
    {provide: JQ_TOKEN, useValue: jQuery}
  ],
  bootstrap: [

  ]
})
export class AppModule { }
