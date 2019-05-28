import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BattleComponent } from './battle.component';
import { battleRoutes } from './battle.routes';
import { BattleHubService } from '../share/battle-hub.service';

@NgModule({
  imports: [
      CommonModule,
      RouterModule.forChild(battleRoutes)
  ],
  declarations: [
    BattleComponent
  ],
  providers: [
  ],
  bootstrap: [
  ]
})
export class BattleModule { }
