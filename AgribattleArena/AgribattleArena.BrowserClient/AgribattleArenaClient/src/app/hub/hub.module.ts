import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HubComponent } from './hub.component';
import { hubRoutes } from './hub.routes';
import { SideBarComponent } from './side-bar';
import { StartPageComponent } from './start-page.component';
import { HubResolver } from './hub.resolver';
import { WrappersModule } from '../share/wrappers/wrappers.module';
import { BattleHubService } from '../share/battle-hub.service';

@NgModule({
  declarations: [
      HubComponent,
      SideBarComponent,
      StartPageComponent
  ],
  imports: [
      CommonModule,
      RouterModule.forChild(hubRoutes),
      WrappersModule
  ],
  providers: [
      HubResolver
  ],
  bootstrap: [
  ]
})
export class HubModule { }
