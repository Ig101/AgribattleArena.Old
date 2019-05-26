import { NgModule } from '@angular/core';
import { TOASTR_TOKEN, JQ_TOKEN } from '../common/index';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HubComponent } from './hub.component';
import { hubRoutes } from './hub.routes';
import { SideBarComponent } from './side-bar';

@NgModule({
  declarations: [
      HubComponent,
      SideBarComponent
  ],
  imports: [
      CommonModule,
      RouterModule.forChild(hubRoutes)
  ],
  providers: [
  ],
  bootstrap: [
  ]
})
export class HubModule { }
