import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { LoadingService, LoadingComponent } from './loading';
import { ProfileService } from './share/profile.service';
import { HttpClientModule } from '@angular/common/http';
import { BattleHubService } from './share/battle-hub.service';

@NgModule({
  declarations: [
    AppComponent,
    LoadingComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule
  ],
  providers: [
    LoadingService,
    ProfileService,
    BattleHubService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
